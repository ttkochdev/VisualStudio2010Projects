/**
 * Tyler Koch 3/10/2013
 * Lab 2 Client for assignment in CSC 569
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreadedTCPClient
{
    class MultiThreadedTCPClient
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient tcpclient = new TcpClient();
                Console.WriteLine("Connecting...");

                tcpclient.Connect("localhost", 50001);

                Console.WriteLine("Connected!!!");
                Console.Write("Please enter a command. (ex. fib 5): ");


                NetworkStream stream = (NetworkStream)tcpclient.GetStream();
                StreamReader netReader = new StreamReader(stream);
                StreamWriter netWriter = new StreamWriter(stream);
                netWriter.AutoFlush = true;
                Boolean whilecondition = true;

                while (whilecondition)
                {
                    String userinput = Console.ReadLine();
                    String response;
                    int integerToSend = 0;
                    String[] stringArray = userinput.Split(' ');
                    if (stringArray[0].Equals("fact") || stringArray[0].Equals("fib"))
                    {

                        integerToSend = int.Parse(stringArray[1]);
                        netWriter.WriteLine(userinput);

                        for (int i = 0; i <= integerToSend; i++)
                        {
                            response = netReader.ReadLine();
                            Console.WriteLine(response);
                        }

                    }
                    else
                    {
                        if (userinput == String.Empty)
                        {
                            Console.WriteLine("You didn't enter anything. ");
                        }
                        else if (stringArray[0].Equals("bye"))
                        {
                            netWriter.WriteLine("bye");
                            whilecondition = false;
                        }
                        else if (userinput != String.Empty)
                        {
                            Console.WriteLine("You entered a bad command! ");
                        }
                        else
                        {
                            whilecondition = false;
                        }
                    }
                    Console.Write("Please enter a command. (ex. fact 4): ");
                }
                Console.WriteLine("bye");
                tcpclient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Maybe it exploded?: " + e.StackTrace);
            }

        }
    }
}
