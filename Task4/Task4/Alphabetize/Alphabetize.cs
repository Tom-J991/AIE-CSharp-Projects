using System;
using System.Collections.Generic;
using System.IO;

namespace Alphabetize
{
    internal class Alphabetize
    {
        // Create a program that will read a plain text file called ‘words.txt’ containing a list of randomized words.
        // Your program must sort these words in alphabetical order and write the ordered list of words to a new file called ‘output.txt’.
        // Record at least 2 tests.
        static void Main()
        {
            Console.WriteLine("- Create a program that will read a plain text file called ‘words.txt’ containing a list of randomized words -");

            int fileLength = 0;
            List<string> words = new List<string>();
            string inPath = System.IO.Directory.GetCurrentDirectory() + @"\words.txt"; // words.txt same folder as executable.
            string outPath = System.IO.Directory.GetCurrentDirectory() + @"\output.txt"; // output.txt same folder as executable.

            // Open File
            if (File.Exists(inPath))
            {
                using (StreamReader sr = File.OpenText(inPath))
                {
                    string s;
                    s = sr.ReadLine();
                    if (!Int32.TryParse(s, out fileLength))
                    {
                        // Error
                        Console.WriteLine("1st Line is not a length."); // 1st line in words.txt is not a number.
                        Console.ReadKey();
                        return;
                    }
                    for (int i = 0; i < fileLength; i++)
                    {
                        s = sr.ReadLine();
                        words.Add(s);
                    }
                }
                Console.WriteLine("Read : " + inPath);
            }
            else
            {
                // Error
                Console.WriteLine(inPath + " : File does not exist!");
                Console.ReadKey();
                return;
            }

            // Alphabetize Words
            List<string> alphaWords = words;
            alphaWords.Sort();

            // Write File
            if (!File.Exists(outPath))
            {
                using (StreamWriter sw = File.CreateText(outPath))
                {
                    foreach (string s in alphaWords)
                    {
                        sw.WriteLine(s);
                    }
                }
                Console.WriteLine("Wrote : " + outPath);
            }
            else
            {
                // Error
                Console.WriteLine(outPath + " : File already exists!"); // Delete output.txt and try again.
                Console.WriteLine("Delete output.txt and try again.");
                Console.ReadKey();
                return;
            }

            Console.ReadKey();
        }
    }
}
