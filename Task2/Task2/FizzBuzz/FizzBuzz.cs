using System;

namespace FizzBuzz
{
    internal class FizzBuzz
    {
        // Write a program that solves the FizzBuzz word game.
        static void FindFizzBuzz()
        {
            Console.WriteLine("- Write a program that solves the FizzBuzz word game -");
            
            string result = "";
            int num = 0;

            // Get Input
            Console.WriteLine("Input Number: ");
            string input = Console.ReadLine();
            if (!Int32.TryParse(input, out num))
            {
                Console.WriteLine("Input is not a number.");
                FindFizzBuzz();
            }

            // FizzBuzz
            bool fizz = false, buzz = false;
            if (num % 3 == 0)
            {
                fizz = true;
                result += "Fizz";
            }
            if (num % 5 == 0)
            {
                buzz = true;
                result += "Buzz";
            }

            if (!fizz && !buzz) // Not a multiple of either 3 or 5.
                result = "" + num;

            // Output
            Console.WriteLine("Result = " + result);
        }

        static void Main()
        {
            // Record at least 2 tests for this problem.
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("(Pass " + (i + 1) + ")");
                FindFizzBuzz();
                Console.ReadKey();
            }
        }
    }
}
