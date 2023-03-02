using System;
using System.Collections.Generic;
using System.Linq;

namespace SumOfArray
{
    internal class SumOfArray
    {
        static int SumNumbers(int[] numbers)
        {
            int result = 0;

            // Calculate Sum
            foreach (int i in numbers)
                result += i;

            return result;
        }

        //Implement and test a function that accepts an integer array and returns the sum of the array.
        static void SumArrays()
        {
            Console.WriteLine("- Implement and test a function that accepts an integer array and returns the sum of the array -");

            int sum = 0;
            List<int> numArray = new List<int>();

            // Get Input
            bool getInput = true;
            while (getInput)
            {
                Console.WriteLine("Input Number (Q when done): ");
                string input = Console.ReadLine();
                int num = 0;
                if (input == "Q" || input == "q")
                    getInput = false;
                else
                {
                    if (Int32.TryParse(input, out num))
                    {
                        numArray.Add(num);
                    }
                    else
                    {
                        Console.WriteLine("Input is not a Number (or Q).");
                    }
                }
            }

            // Get Sum
            sum = SumNumbers(numArray.ToArray());

            // Output
            if (numArray.Count == 0)
                return;

            Console.Write("Array = { ");
            foreach (int i in numArray)
            {
                if (i == numArray.Last<int>())
                    Console.Write(i + " }\n");
                else
                {
                    Console.Write(i + ", ");
                }
            }
            Console.WriteLine("Sum of Array = " + sum);
        }

        //Record at least 2 tests for this problem.
        static void Main()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("(Pass " + (i+1) + ")");
                SumArrays();
                Console.ReadKey();
            }
        }
    }
}
