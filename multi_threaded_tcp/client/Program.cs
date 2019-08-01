using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Multi-Threaded TCP Server Demo");
            Console.WriteLine("Provide IP:");
            String ip = Console.ReadLine();

            Console.WriteLine("Provide Port:");
            int port = Int32.Parse(Console.ReadLine());

            ClientDemo client = new ClientDemo(ip, port);
        }
    }
}
