using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Windows;

namespace Serwer
{
    class Program
    {

        Formatter fm = new Formatter();
        const int port = 7777;
        int id = 0;
        char type = 's';
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);

            EndPoint serverAddress = new IPEndPoint(IPAddress.Any, 7777);
            serverSocket.Bind(serverAddress);

            var context = new MyContext();
            var studenci = context.Studenci.ToArray();
            Console.WriteLine(studenci[1].name);

            serverSocket.Listen(10);
            
        }

        async void connectionHandler(Socket serverSocket)
        {
            Socket connected =
                await Task<Socket>.Factory.StartNew(serverSocket.Accept);

            if (connected.Connected)
            {
                NetworkStream cStream = new NetworkStream(connected);
                string data = await fm.DeserializeAsync<string>(connected);
                await loginHandler(data);

                //await fm.SerializeAsync<int>(cStream, id);
                //await fm.SerializeAsync<char>(cStream, type);

                connected.Close();

            }
        }
        public (int, char) login(string email, string password)
        {
            var context = new MyContext();
            var studenci = context.Studenci.ToArray();
            var nauczyciele = context.Nauczyciele.ToArray();
            var rodzice = context.Rodzice.ToArray();
            int id = 0;
            char type = 's';
            foreach (var student in studenci)
            {
                if (student.email == email)
                {
                    if (student.passwd == password)
                    {
                        id = student.student_id;
                        type = 's';
                        break;
                    }
                }
            }
            if (id == 0)
            {
                foreach (var nauczyciel in nauczyciele)
                {
                    if (nauczyciel.email == email)
                    {
                        if (nauczyciel.passwd == password)
                        {
                            id = nauczyciel.teacher_id;
                            type = 't';
                            break;
                        }
                    }
                }
            }

            if (id == 0)
            {
                foreach (var rodzic in rodzice)
                {
                    if (rodzic.email == email)
                    {
                        if (rodzic.passwd == password)
                        {
                            id = rodzic.parent_id;
                            type = 'p';
                            break;
                        }
                    }
                }
            }
            (int, char) tupla = (id, type);
            return tupla;
        }

        Task<(int, char)> loginHandler(string data)
        {
            string[] dataFormatted = data.Split(';');
            string email = dataFormatted[0];
            string password = dataFormatted[1];

            Task<(int, char)> t1 = new Task<(int, char)>(() => login(email, password));

            return t1;

        }

        
     }
}

