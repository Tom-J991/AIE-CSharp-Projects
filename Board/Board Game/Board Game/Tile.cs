using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Game
{
    class Tile
    {
        public Token CurrentToken;

        public void Draw(int x, int y, bool white)
        {
            //Set background color
            if (white)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Gray;
            }

            //Note: Each "Tile" is made of 3 lines
            //First line
            Console.SetCursorPosition(x - 1, y - 1);
            Console.WriteLine("     ");

            //Second line
            Console.SetCursorPosition(x - 1, y);
            Console.Write("  ");
            if (CurrentToken != null)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(CurrentToken.Name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write(" ");
            }
            Console.Write("  ");

            //Third line
            Console.SetCursorPosition(x - 1, y + 1);
            Console.WriteLine("     ");

        }
    }
}
