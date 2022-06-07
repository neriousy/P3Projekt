using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Serwer
{
    public class ClientState
    {
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];

        public StringBuilder sb = new StringBuilder();

        public Socket workSocket = null;
    }

    public class Listener
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false); // powiadamia czekajace watki o zdarzeniu

        public Listener()
        {

        }

        public static void StartListening()
        {
            Socket serverSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            Console.WriteLine("{0}", ipAddress);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 7777);

            try
            {
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);
                while (true)
                {
                    allDone.Reset(); // blokuje watek 
                    Console.WriteLine("Oczekiwanie na polaczenie...");
                    serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);

                    allDone.WaitOne(); // blokuje dopoki handler nie otrzyma sygnalu
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("Server.listen error: " + e);
            }
        }

        public static void AcceptCallback(IAsyncResult ar) // asyncresult = stan operacji asynchronicznej
        {
            allDone.Set(); // odblokowywuje watek

            Socket serverSocket = (Socket)ar.AsyncState;
            Socket handler = serverSocket.EndAccept(ar); // akceptuje asynchronicznie polaczenie

            ClientState stan = new ClientState(); // po przylaczeniu polaczenia tworzymy obiekt gdzie przechowujemy stan polaczenia 
            stan.workSocket = handler;
            handler.BeginReceive(stan.buffer, 0, ClientState.BufferSize, 0, new AsyncCallback(ReadCallback), stan);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            ClientState stan = (ClientState)ar.AsyncState;
            Socket handler = stan.workSocket;

            int bytesRead = handler.EndReceive(ar);

            if(bytesRead > 0)
            {
                stan.sb.Append(Encoding.ASCII.GetString(stan.buffer, 0, bytesRead));

                content = stan.sb.ToString();

                if(content.IndexOf("<EOF>") > -1)
                {
                    Console.WriteLine("Przeczytano {0} bajtow. \nDane: {0}", content.Length, content);
                }
                else
                {
                    handler.BeginReceive(stan.buffer, 0, ClientState.BufferSize, 0, new AsyncCallback(ReadCallback), stan); // rekurencyjne wywolanie dopoki nie przeczytamy calej wiadomosci
                }
            }

        }
    }
}
