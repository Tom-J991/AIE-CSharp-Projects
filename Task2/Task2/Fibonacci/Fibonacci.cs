using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Fibonacci
{
    internal class Fibonacci
    {
        // Write a program that outputs a Fibonacci number sequence.
        static void FindFibonacci()
        {
            Console.WriteLine("- Write a program that outputs a Fibonacci number sequence -");

            int length = 0;
            List<int> series = new List<int>();

            // Get Input Length
            Console.WriteLine("Input Length: ");
            string input = Console.ReadLine();
            if (!Int32.TryParse(input, out length))
            {
                Console.WriteLine("Input is not a number.");
                FindFibonacci();
            }

            // Calculate Series
            int prev_a = 0;
            int prev_b = 1;
            series.Add(prev_a); // Add initials to list.
            series.Add(prev_b);
            for (int i = 2; i <= length; i++) // Calculate rest
            {
                int result = prev_a + prev_b;

                series.Add(result);

                prev_a = prev_b;
                prev_b = result;
            }

            // Output
            foreach (int i in series)
            {
                if (i == series.Last<int>())
                {
                    Console.Write(i + "\n");
                }
                else
                    Console.Write(i + ", ");
            }
        }

        static void Main()
        {
            // Record at least 2 tests for this problem.
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("(Pass " + (i + 1) + ")");
                FindFibonacci();
                Console.ReadKey();
            }
        }
    }
}
