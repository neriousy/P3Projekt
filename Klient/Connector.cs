/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
namespace Dziennik_Szkolny
{
    public class ClientState
    {
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];

        public StringBuilder sb = new StringBuilder();

        public Socket workSocket = null;
    }
    public class Connector
    {
        private const int port = 7777;

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        private static String response = String.Empty;
        public static void StartClient()
        {
            try
            {
                Socket client = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);
                //IPHostEntry ipHostInfo = Dns.GetHostEntry("10.10.244.165");
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint server = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                
                client.BeginConnect(server, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                Send(client, "This iasdasds a test<EOF>");
                sendDone.WaitOne();
            }
            catch(Exception e)
            {
                Trace.WriteLine("Polaczenie" + e);
            }
        }

        public static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                connectDone.Set();
            }
            catch(Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        public static void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);

        }

        public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;

                int bytesSent = client.EndSend(ar);
                string bytes = bytesSent.ToString();
                Trace.WriteLine("Sent {0} bytes to server.", bytes);

                sendDone.Set();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                ClientState stan = new ClientState();
                stan.workSocket = client;

                client.BeginReceive(stan.buffer, 0, ClientState.BufferSize, 0, new AsyncCallback(ReceiveCallBack), stan);

            }
            catch(Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                ClientState stan = (ClientState)ar.AsyncState;
                Socket client = stan.workSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    stan.sb.Append(Encoding.ASCII.GetString(stan.buffer, 0, bytesRead));

                    client.BeginReceive(stan.buffer, 0, ClientState.BufferSize, 0, new AsyncCallback(ReceiveCallBack), stan);
                }
                else
                {
                    if (stan.sb.Length > 1)
                    {
                        response = stan.sb.ToString();
                        receiveDone.Set();

                    }
                }
            }
            catch(Exception e)
            {
                Trace.WriteLine(e.ToString());
            }
        }


    }
}
*/