using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThreadConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create three threads
            Thread t1 = new Thread(printString);
            Thread t2 = new Thread(printString);
            Thread t3 = new Thread(squareNumber);
            //Start the threads
            t1.Start("Larry");
            t2.Start("Curly");
            t3.Start(12);
            //Wait for the threads to terminate
            t1.Join();
            t2.Join();
            t3.Join();
            // Exit the program
            System.Console.WriteLine("All threads are done.");
        }
        // Thread method
        static void printString(object str)
        {
            Random randy = new Random();
            String s = (String)str;
            for (int k = 0; k <= 12; k++)
            {
                System.Console.Write(" " + s + " ");
                Thread.Sleep(randy.Next(500));
            }
        }
        // Another thread method
        static void squareNumber(object num)
        {
            Random randy = new Random();
            int number = (int)num;
            for (int k = 0; k <= number; k++)
            {
                System.Console.WriteLine(" {0} ", k * k);
                Thread.Sleep(randy.Next(500));
            }
        }
    }
}