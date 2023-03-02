using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board_Game
{
    class Token
    {
        public string Name = "X";
        public int Player = 0;

        public Token(int player)
        {
            Player = player;
            if (player == 1)
                Name = "X";
            else if (player == 2)
                Name = "O";
        }
    }
}
