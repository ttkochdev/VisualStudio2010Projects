/*
 * Tyler Koch 
 * CSC 569
 * C# lab client
 * 3/3/2013
 * 
 * Client base code credited to: 
 * http://www1.cs.columbia.edu/~lok/csharp/refdocs/System.Net.Sockets/types/TcpClient.html 
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace NetAppClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            String str;
            TcpClient tcpClient = new TcpClient();
            Console.WriteLine("Setting up connection....");
            try
            {

                tcpClient.Connect("localhost", 50001);
                Console.WriteLine("Connected");
                Console.WriteLine("Enter command (ex. 'add 1 2' or 'bye' to close connection): ");

                while (true)
                {
                    str = Console.ReadLine();
                    NetworkStream networkStream = tcpClient.GetStream();

                    if (str != String.Empty && str != "bye")
                    {

                        // Does a simple write.
                        byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes(str);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);

                        // Reads the NetworkStream into a byte buffer.
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                        int i = networkStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);

                        // Returns the data received from the host to the console.
                        string returndata = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: " + returndata);
                        Console.WriteLine("Enter command (ex. 'add 1 2' or 'bye' to close connection): ");

                    }
                    else
                    {
                        if (str == String.Empty)
                        {
                            Console.Write("You didn't enter anything. Please enter a command. Try: 'mult 2 3' or 'bye' to quit ");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                Console.WriteLine("Closing connection.");
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}