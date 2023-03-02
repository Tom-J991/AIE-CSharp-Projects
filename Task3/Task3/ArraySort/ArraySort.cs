using System;
using System.Collections.Generic;
using System.Linq;

namespace ArraySort
{
    internal class ArraySort
    {
        static void SortDescending(List<int>? array, out int[] sorted)
        {
            sorted = array.OrderByDescending(c => c).ToArray();
        }

        // Implement and test a function that sorts the elements in descending order.
        static void SortArrays()
        {
            Console.WriteLine("- Implement and test a function that sorts the elements in descending order -");

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

            // Sort Array
            int[] sortedArray;
            SortDescending(numArray, out sortedArray);

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

            Console.Write("Sorted Array = { ");
            foreach (int i in sortedArray)
            {
                if (i == sortedArray.Last<int>())
                    Console.Write(i + " }\n");
                else
                {
                    Console.Write(i + ", ");
                }
            }
        }

        //Record at least 2 tests for this problem.
        static void Main()
        {
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("(Pass " + (i + 1) + ")");
                SortArrays();
                Console.ReadKey();
            }
        }
    }
}
