/*
 * Tyler Koch 
 * CSC 569
 * C# lab server
 * 3/3/2013
 */
using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace TcpListenerSample
{
    public class TcpListenerSample
    {
        static void Main(string[] args)
        {
            try
            {
                // set the TcpListener on port 50001
                int port = 50001;
                TcpListener server = new TcpListener(IPAddress.Any, port);
                server.Start();
                // Buffer for reading data. Instead I am using buffer size. 
                //byte[] bytes = new byte[1024];
                string data;

                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    NetworkStream stream = client.GetStream();
                    // get data from client
                    int i;
                    byte[] bytes = new byte[client.ReceiveBufferSize];
                    i = stream.Read(bytes, 0, (int)client.ReceiveBufferSize);
                    //i = stream.Read(bytes, 0, bytes.Length);

                    while (i != 0)
                    {
                        // convert ASCII
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine(String.Format("Received: {0}", data));
                        //if command was bye break the loop            
                        if (data.Equals("bye"))
                        {
                            break;
                        }
                        else
                        {
                            data = dataProcess(data);
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine(String.Format("Sent: {0}", data));
                            i = stream.Read(bytes, 0, bytes.Length);
                        }
                    }
                    //end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
        public static string dataProcess(String data)
        {
            int total = 0;
            String totalToString = String.Empty;

            string[] dataSplit = data.Split();
            String dataTrim = dataSplit[0].Trim();

            if (dataSplit.Length == 3)
            {
                if (dataTrim.Equals("add"))
                {
                    String string1 = dataSplit[1];
                    String string2 = dataSplit[2];

                    int x = int.Parse(string1.Trim());
                    int y = int.Parse(string2.Trim());

                    total = x + y;
                    totalToString = total.ToString();
                }
                if (dataTrim.Equals("mult"))
                {
                    String string1 = dataSplit[1];
                    String string2 = dataSplit[2];

                    int x = int.Parse(string1.Trim());
                    int y = int.Parse(string2.Trim());

                    total = x * y;
                    totalToString = total.ToString();
                }
            }
            else
            {
                totalToString = "Something went wrong. Try another command.";
            }

            return totalToString;
        }
    }
}