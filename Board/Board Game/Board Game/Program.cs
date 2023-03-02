using System;
using System.Threading;
using System.Threading.Tasks;

namespace Board_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            int gridSize = 8;
            int tileWidth = 5;
            int tileHeight = 3;
            Tile[,] grid = new Tile[gridSize, gridSize];

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    grid[x, y] = new Tile();
                }
            }


            bool winner = false;
            int player = 1;
            int playerTotal = 2;

            //Game Loop
            while (!winner)
            {
                //Draw tiles
                for (int y = 0; y < gridSize; y++)
                {
                    //Draw outter labels (1-6 and A-E)
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(0, (y + 1) * tileHeight);
                    Console.Write(y + 1);
                    Console.SetCursorPosition((y + 1) * tileWidth, 0);
                    Console.Write(Convert.ToChar('A' + y));

                    //Draw colored tiles
                    bool white = y % 2 == 0;
                    for (int x = 0; x < gridSize; x++)
                    {
                        grid[x, y].Draw((x * tileWidth) + 5, (y * tileHeight) + 3, white);
                        white = !white;
                    }
                }

                //Instructions
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, gridSize * tileHeight + 3);
                Console.WriteLine("Player " + player + ": Select tile to place your token (eg. F3)");

                //Read input
                string input = Console.ReadLine().ToUpper();
                //-65 and -49 refer to unicode. They can take a char like 'A' and convert it to the number appropriate for the grid
                int first = input[0] - 65;
                int second = input[1] - 49;

                //Set token
                bool validInput = GameLogic(grid, player, first, second);
                winner = WinLogic(grid, player, first, second);

                if (validInput && !winner)
                {
                    //Next player
                    player += 1;
                    if (player > playerTotal)
                        player = 1;
                }

                Console.Clear();
            }
        }

        //Make something happen once player has chosen their spot on the grid.
        public static bool GameLogic(Tile[,] grid, int player, int x, int y)
        {
            grid[x, y].CurrentToken = new Token(player);

            //return true if the player is allowed to make that move
            return true;
        }


        //Check if the current player has won
        public static bool WinLogic(Tile[,] grid, int player, int x, int y)
        {

            //return true if player wins
            return false;
        }
    }
}
