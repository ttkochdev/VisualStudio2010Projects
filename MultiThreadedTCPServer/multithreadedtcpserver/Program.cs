/**
 * Tyler Koch 3/10/2013
 * Lab 2 Server for assignment in CSC 569
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadedTCPServer
{
    public class MultiThreadedTCPServer
    {

        private TcpListener server;
        private Thread serverThread;

        public MultiThreadedTCPServer()
        {
            this.server = new TcpListener(IPAddress.Any, 50001);
            this.serverThread = new Thread(new ThreadStart(run));
            this.serverThread.Start();
        }
        private void run()
        {
            this.server.Start();
            Console.WriteLine("Waiting to be connected...");
            while (true)
            {
                TcpClient client = this.server.AcceptTcpClient();
                Console.WriteLine("Connected!!!");
                Thread clientT = new Thread(new ParameterizedThreadStart(ClientLogic));
                clientT.Start(client);
            }
        }
        //This does not seem like a very fast fib sequence....
        //But then again we are sleeping on threads...
        static long fib(long n)
        {
            if (n <= 1)
            {
                return 1;
            }
            else
            {
                return fib(n - 1) + fib(n - 2);
            }
        }
        static long fact(long n)
        {
            if (n <= 1) return 1; else return n * fact(n - 1);
        }

        private void ClientLogic(object client)
        {
            Boolean whilecondition = true;
            while (whilecondition)
            {
                try
                {
                    TcpClient tcpClient = (TcpClient)client;
                    NetworkStream networkStream = (NetworkStream)tcpClient.GetStream();
                    StreamReader netReader = new StreamReader(networkStream);
                    StreamWriter netWriter = new StreamWriter(networkStream);
                    netWriter.AutoFlush = true;

                    String userinput = netReader.ReadLine();
                    String[] inputArray = userinput.Split(' ');
                    long inputNumber = 0;
                    //I thought about doing a switch here but for lack of familiarity in c#
                    //I went with something simple. 
                    if (userinput.Equals("bye"))
                    {
                        whilecondition = false;
                        Console.WriteLine("bye");
                    }
                    else
                    {
                        if (inputArray[0].Equals("fact"))
                        {
                            inputNumber = long.Parse(inputArray[1]);
                            for (long i = 0; i <= inputNumber; i++)
                            {
                                Thread.Sleep(1000); //sleep then write. 
                                netWriter.WriteLine(fact(i));
                            }
                        }
                        else
                        {
                            if (inputArray[0].Equals("fib"))
                            {
                                inputNumber = long.Parse(inputArray[1]);
                                for (long i = 0; i <= inputNumber; i++)
                                {
                                    Thread.Sleep(1000); //sleep then write. 
                                    netWriter.WriteLine(fib(i));
                                }
                            }
                            else
                            {
                                netWriter.WriteLine("Try another command:");
                            }
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            MultiThreadedTCPServer TCPServer = new MultiThreadedTCPServer();
        }

    }
}