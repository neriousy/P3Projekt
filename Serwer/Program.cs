using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Windows;

namespace Serwer
{
    class Program
    {

        public static int Main(string[] args)
        {
            Listener.StartListening();
            return 0;
        }

    }
}

