/**
 * Developed By: Jason Kryst
 * IDE: Visual Studio 2012
 * Language: C#
 * Date: 2/28/2013
 * Educational: For CSC 569/Prof. Muganda
 * About: A TCP based data submission client written in C# that will send
 * to specific commands.  Upon submission of a "bye" the port is closed.
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient tcpclient = new TcpClient();
                Console.WriteLine("Connecting...");

                tcpclient.Connect("127.0.0.1", 50001);

                Console.WriteLine("Connected");
                Console.Write("Enter the string to be transmitted: ");


                NetworkStream stream = (NetworkStream)tcpclient.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;
                Boolean flag = true;

                while (flag)
                {
                    String str = Console.ReadLine();
                    String response;
                    int intInput = 0;
                    String[] stringArray = str.Split(' ');
                    if (stringArray[0].Equals("fib") || stringArray[0].Equals("fact"))
                    {

                        intInput = int.Parse(stringArray[1]);
                        writer.WriteLine(str);

                        for (int i = 0; i <= intInput; i++)
                        {
                            response = reader.ReadLine();
                            Console.WriteLine(response);
                        }
                        //str = "";
                    }
                    else
                    {
                        if (str == String.Empty)
                        {
                            Console.Write("Invalid entry, enter a new string: ");
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    Console.Write("Enter the string to be transmitted: ");
                }
                Console.WriteLine("You have been disconnected");
                tcpclient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
            }

        }
    }
}
