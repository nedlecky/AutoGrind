using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoGrind
{
    public class TcpServerSupport
    {
        TcpListener server;
        TcpClient client;
        NetworkStream stream;
        string myIp;
        string myPort;
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        const int inputBufferLen = 128000;
        byte[] inputBuffer = new byte[inputBufferLen];
        public int nGetStatusRequests = 0;
        public int nGetStatusResponses = 0;
        public int nBadCommLenErrors = 0;
        public Action<string> ReceiveCallback { get; set; }
        public bool IsClientConnected { get; set; } = false;

        public TcpServerSupport()
        {
            log.Debug("TcpServer(...)");
        }

        ~TcpServerSupport()
        {
            log.Debug("~TcpServer()");
        }

        public long IPAddressToLong(IPAddress address)
        {
            byte[] byteIP = address.GetAddressBytes();

            long ip = (long)byteIP[3] << 24;
            ip += (long)byteIP[2] << 16;
            ip += (long)byteIP[1] << 8;
            ip += (long)byteIP[0];
            return ip;
        }

        public int Connect(string IPport)
        {
            try
            {
                string[] s = IPport.Split(':');
                return Connect(s[0], s[1]);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return 1;
            }
        }
        public int Connect(string IP, string port)
        {
            myIp = IP;
            myPort = port;

            log.Info("UR Connect({0},{1})", IP, port);
            if (server != null) Disconnect();
            IsClientConnected = false;

            try
            {
                IPAddress ipAddress = IPAddress.Parse(IP);
                IPEndPoint remoteEP = new IPEndPoint(IPAddressToLong(ipAddress), Int32.Parse(port));
                server = new TcpListener(remoteEP);
                server.Start();
                server.BeginAcceptTcpClient(ClientConnected, server);
            }
            catch
            {
                log.Error("Couldn't start UR server");
                return 1;
            }
            log.Info("Server: Waiting for UR Client...");
            return 0;
        }

        public int Disconnect()
        {
            log.Info("UR Disconnect()");
            CloseConnection();
            if (server != null)
            {
                server.Stop();
                server = null;
            }
            return 0;
        }

        void ClientConnected(IAsyncResult result)
        {
            IsClientConnected = false;
            try
            {
                TcpListener server = (TcpListener)result.AsyncState;
                if (server != null)
                {
                    try
                    {
                        client = server.EndAcceptTcpClient(result);
                        stream = client.GetStream();
                        log.Info("ClientConnected(): UR Client connected");
                        IsClientConnected = true;

                        Send("(10)");        // Query position
                    }
                    catch { }
                }
            }
            catch
            {
            }
        }

        public bool IsConnected()
        {
            if (server == null) return false;
            try
            {
                return !(server.Server.Poll(1, SelectMode.SelectRead) && (server.Server.Available == 0));
            }
            catch (SocketException) { return false; }
        }

        void CloseConnection()
        {
            log.Debug("UR CloseConnection()");

            if (stream != null)
            {
                stream.Close();
                stream = null;
            }

            if (client != null)
            {
                client.Close();
                client = null;
            }
            IsClientConnected = false;
        }

        bool fSendBusy = false;
        public int Send(string response)
        {
            if (stream == null) return 1;
            while (fSendBusy)
                Thread.Sleep(10);
            fSendBusy = true;

            log.Debug("UR==> {0}", response);
            try
            {
                stream.Write(Encoding.ASCII.GetBytes(response + "\r"), 0, response.Length + 1);
            }
            catch
            {
                log.Error("Send(...) could not write to UR socket");
            }
            fSendBusy = false;
            return 0;
        }

        int addingAt = 0;
        private Queue<string> inputQueue = new Queue<string>();
        public string Receive()
        {
            if (stream == null) return "";
            if (!IsConnected())
            {
                log.Error("Lost UR connection");
                Disconnect();
                Connect(myIp, myPort);
                return "";
            }

            // Read any available characters and \n delimit them as strings into the queue
            // If a string is started but has no \n, it will be completed and queued in a later call!
            int totalChars = 0;
            while (stream.DataAvailable)
            {
                int c = stream.ReadByte();
                totalChars++;
                if (c == 10)
                {
                    inputQueue.Enqueue(Encoding.UTF8.GetString(inputBuffer, 0, addingAt));
                    addingAt = 0;
                }
                else
                    inputBuffer[addingAt++] = (byte)c;
            }
            // This can be used to see how frequently incomplete lines are recived... about once an hour in normal testing
            if (addingAt > 0)
                log.Debug("UR<== incomplete line received (will get rest later) totalChars={0} addingAt={1} [{2}]", totalChars,addingAt, Encoding.UTF8.GetString(inputBuffer, 0, addingAt-1));

            // No execute any completed lines that have been received
            int lineNo = 1;
            int nLines = inputQueue.Count;
            while (inputQueue.Count > 0)
            {
                string line = inputQueue.Dequeue();
                if (line.Length > 0)
                {
                    if (nLines > 1)
                        log.Debug("UR<== {0} Line {1} of {2}", line, lineNo, nLines);
                    else
                        log.Debug("UR<== {0}", line);
                    if (ReceiveCallback == null)
                        return line;
                    else
                        ReceiveCallback?.Invoke(line);
                }
                lineNo++;
            }
            return "";
        }
    }
}
