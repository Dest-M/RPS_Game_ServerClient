using System.Numerics;
using System.Text;

namespace RPS_Game_Core
{
    public class WinCounter
    {
        public int p1Wins = 0;
        public int p2Wins = 0;
        public string status = "AA";
        public int rounds = 0;

        public void addWin(int wins)
        {
            rounds++;
            if (wins == 1)
            {
                p1Wins++;
                changeStatus("Player 1 wins this round.");

            }
            else if (wins == 2)
            {
                p2Wins++;
                changeStatus("Player 2 wins this round.");


            }
            else
            {
                changeStatus("Draw");
            }
        }



        void changeStatus(string str)
        {
            status = str.ToString() + "\n______________________\nPlayer1: " + p1Wins.ToString() + "\nPlayer2: " + p2Wins.ToString() + "\nRounds: " + rounds.ToString() + "/5\n\n";
        }
    }
    
}