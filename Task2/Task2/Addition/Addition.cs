using System;

namespace Addition
{
    internal class Addition
    {
        // Create a function called AddNumbers that accepts two parameters of type float, and returns an integer.
        static int AddNumbers(float a, float b)
        {
            int result = 0;
            float sum = a + b;
            result = (int)(Math.Floor(sum));

            return result;
        }

        // Write a program to determine the integer floor of the sum of two floating point numbers.
        static void IntegerFloor()
        {
            Console.WriteLine("- Integer floor of the sum of two floating point numbers - ");

            int result = 0;
            float num_a, num_b = 0;

            // Get Inputs
            Console.WriteLine("First Input: ");
            string input = Console.ReadLine();
            if (!float.TryParse(input, out num_a))
            {
                Console.WriteLine("Input is not a Floating Point Number.");
            }

            Console.WriteLine("Second Input: ");
            input = Console.ReadLine();
            if (!float.TryParse(input, out num_b))
            {
                Console.WriteLine("Input is not a Floating Point Number.");
            }

            // Calculate
            result = AddNumbers(num_a, num_b);

            // Output
            Console.WriteLine("Inputs = " + num_a + " + " + num_b + " = " + (num_a + num_b));
            Console.WriteLine("Result = " + result);
        }

        static void Main()
        {
            // Record at least 2 tests for this problem.
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("(Pass " + (i+1) + ")");
                IntegerFloor();
                Console.ReadKey();
            }
        }
    }
}
