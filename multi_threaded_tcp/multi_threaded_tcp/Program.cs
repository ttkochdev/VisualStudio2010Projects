using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Multi_Threaded_TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Multi-Threaded TCP Server Demo");
            TcpServer server = new TcpServer(5555);
        }
    }
}
