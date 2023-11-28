using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS_Game_Core
{
    public class GameLogic
    {
        
        public void turnWin(int p1, int p2, WinCounter wincounter)
        {
            int winner;
            if(p1 == 1)
            {
                switch (p2)
                {
                    case 1:
                        winner = 0; break;
                    case 2:
                        winner = 2; break;
                    case 3:
                        winner = 1; break;
                    default:
                        winner = 1; break;
                }
            }
            else if (p1 == 2)
            {
                switch (p2)
                {
                    case 1:
                        winner = 1; break;
                    case 2:
                        winner = 0; break;
                    case 3:
                        winner = 2; break;
                    default:
                        winner = 1; break;
                }
            }
            else if (p1 == 3)
            {
                switch (p2)
                {
                    case 1:
                        winner = 2; break;
                    case 2:
                        winner = 1; break;
                    case 3:
                        winner = 0; break;
                    default:
                        winner = 1; break;
                }
            }
            else
            {
                winner = 0;
            }
            wincounter.addWin(winner);
        }

    }
}
