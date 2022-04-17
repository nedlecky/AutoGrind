using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        string logPrefix;

        public TcpClientSupport(string logPrefix_)
        {
            logPrefix = logPrefix_;
            log.Debug("{0} TcpClientSupport(...)", logPrefix);
        }
        ~TcpClientSupport()
        {
            log.Debug("{0} ~TcpClientSupport(...)", logPrefix);
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

            log.Info("{0} Connect({1}, {2})", logPrefix, myIp, myPort);
            if (client != null) Disconnect();

            IsClientConnected = false;
            try
            {
                Ping ping = new Ping();
                PingReply PR = ping.Send(myIp, 500);
                log.Info("{0} Connect Ping returns {1}", logPrefix, PR.Status);
                if (PR.Status != IPStatus.Success)
                {
                    log.Error("{0} Could not ping {1}: {2}", logPrefix, myIp, PR.Status);
                    return 2;
                }
            }
            catch
            {
                log.Error("{0} Ping {1} failed", logPrefix, myIp);
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
                log.Error("{0} Could not connect", logPrefix);
                return 2;
            }

            log.Info("{0} Connected", logPrefix);
            IsClientConnected = true;
            return 0;
        }
        public int Disconnect()
        {
            log.Info("{0} Disconnect()", logPrefix);

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
                log.Error("{0} Not connected... stream==null", logPrefix);
                ++sendErrorCount;
                if (sendErrorCount > 5)
                {
                    log.Error("{0} Trying to bounce socket 1", logPrefix);
                    Disconnect();
                    Connect(myIp, myPort);
                    sendErrorCount = 0;
                }
                fSendBusy = false;
                return 1;
            }

            log.Debug("{0}==> {1}", logPrefix, request);
            try
            {
                stream.Write(Encoding.ASCII.GetBytes(request + "\r"), 0, request.Length + 1);
            }
            catch
            {
                log.Error("{0} Send(...) failed", logPrefix);
                ++sendErrorCount;
                if (sendErrorCount > 5)
                {
                    log.Error("{0} Trying to bounce socket 2", logPrefix);
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
            if (stream == null) return null;

            int length = 0;
            while (stream.DataAvailable && length < inputBufferLen) inputBuffer[length++] = (byte)stream.ReadByte();

            if (length == 0) return null;

            string input = Encoding.UTF8.GetString(inputBuffer, 0, length).Trim();
            string[] inputLines = input.Split('\n');  // TODO this was \r
            int lineNo = 1;
            foreach (string line in inputLines)
            {
                string cleanLine = line.Trim();
                if (cleanLine.Length > 0)
                {
                    //log.Debug("{0}<== {1} Line {2}", logPrefix, cleanLine, lineNo);
                    //ReceiveCallback?.Invoke(cleanLine); // This is the newer C# "Invoke if not null" syntax
                }
                lineNo++;
            }
            return input.Trim();
        }

        public string InquiryResponse(string inquiry, int timeoutMs = 50)
        {
            // Purge any remaining responses
            string response = Receive();
            if (response != null)
            {
                log.Error("{0} Already had a response waiting: {1}", logPrefix, response);
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();
            Send(inquiry);

            // Wait for awhile for the response!
            while ((response = Receive()) == null && timer.ElapsedMilliseconds < timeoutMs) ;

            if (response == null)
            {
                log.Info("{0} IR({1}) waited {2} mS. Retrying...", logPrefix, inquiry, timeoutMs);

                // Let's just wait a bit more?
                while ((response = Receive()) == null && timer.ElapsedMilliseconds < timeoutMs * 2) ;
                timer.Stop();
                if (response != null)
                {
                    log.Info("{0} IR({1}) Retry succeeded = {2}. [{3} mS]", logPrefix, inquiry, response, timer.ElapsedMilliseconds);
                    return response;
                }
                log.Error("{0} IR({1}) Failed even with retry. [{2} mS]", logPrefix, inquiry, timer.ElapsedMilliseconds);
                return null;
            }
            timer.Stop();

            log.Debug("{0} {1}={2} [{3} mS]", logPrefix, inquiry, response, timer.ElapsedMilliseconds);
            return response;
        }
    }
}
