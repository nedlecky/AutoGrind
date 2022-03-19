using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoGrind
{
    public class TcpClientSupport
    {
        TcpClient client;
        NetworkStream stream;
        string myIp;
        string myPort;
        const int inputBufferLen = 128000;
        byte[] inputBuffer = new byte[inputBufferLen];
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public Action<string, string> receiveCallback { get; set; }
        public Action<string> ReceiveCallback { get; set; }

        public bool IsClientConnected { get; set; } = false;

        public TcpClientSupport()
        {
            log.Debug("TcpClientSupport(...)");
        }
        ~TcpClientSupport()
        {
            log.Debug("~TcpClientSupport(...)");
        }
        public int Connect(string IPport)
        {
            string[] s = IPport.Split(':');
            return Connect(s[0], s[1]);
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

        public int Connect(string IP, string port)
        {
            myIp = IP;
            myPort = port;

            log.Info("URD Connect({0}, {1})", myIp, myPort);
            if (client != null) Disconnect();

            IsClientConnected = false;
            try
            {
                Ping ping = new Ping();
                PingReply PR = ping.Send(myIp,500);
                log.Info("URD Connect Ping returns {0}", PR.Status);
                if (PR.Status != IPStatus.Success)
                {
                    log.Error("UR Could not ping {0}: {1}", myIp, PR.Status);
                    return 2;
                }
            }
            catch
            {
                log.Error("URD Ping {0} failed", myIp);
                return 1;
            }

            IPAddress ipAddress = IPAddress.Parse(myIp);
            IPEndPoint remoteEP = new IPEndPoint(IPAddressToLong(ipAddress), Int32.Parse(myPort));

            try
            {
                client = new TcpClient();
                client.Connect(remoteEP);
                stream = client.GetStream();
            }
            catch
            {
                log.Error("URD Could not connect");
                return 2;
            }

            log.Info("URD Connected");
            IsClientConnected = true;
            return 0;
        }
        public int Disconnect()
        {
            log.Info("URD Disconnect()");

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
            return 0;
        }

        int sendErrorCount = 0;
        bool fSendBusy = false;
        public int Send(string request)
        {
            while (fSendBusy)
                Thread.Sleep(10);

            fSendBusy = true;
            if (stream == null)
            {
                log.Error("URD Not connected... stream==null");
                ++sendErrorCount;
                if (sendErrorCount > 5)
                {
                    log.Error("URD Trying to bounce socket 1");
                    Disconnect();
                    Connect(myIp, myPort);
                    sendErrorCount = 0;
                }
                fSendBusy = false;
                return 1;
            }

            log.Info("URD==> {0}", request);
            try
            {
                stream.Write(Encoding.ASCII.GetBytes(request + "\r"), 0, request.Length + 1);
            }
            catch
            {
                log.Error("URD Send(...) failed");
                ++sendErrorCount;
                if (sendErrorCount > 5)
                {
                    log.Error("URD Trying to bounce socket 2");
                    Disconnect();
                    Connect(myIp, myPort);
                    sendErrorCount = 0;
                }
            }
            fSendBusy = false;
            return 0;
        }
        public string Receive()
        {
            if (stream != null)
            {
                int length = 0;
                while (stream.DataAvailable && length < inputBufferLen) inputBuffer[length++] = (byte)stream.ReadByte();

                if (length > 0)
                {
                    string input = Encoding.UTF8.GetString(inputBuffer, 0, length);
                    string[] inputLines = input.Split('\n');  // TODO this was \r
                    int lineNo = 1;
                    foreach (string line in inputLines)
                    {
                        string cleanLine = line.Trim('\r');  // TODO this was \n
                        if (cleanLine.Length > 0)
                        {
                            log.Debug("URD<== {0} Line {1}", cleanLine, lineNo);
                            ReceiveCallback?.Invoke(cleanLine); // This is the newer C# "Invoke if not null" syntax
                        }
                        lineNo++;
                    }
                }

                // TODO: I think all these returns are ignored
                return "";
            }
            return "";
        }
    }
}
