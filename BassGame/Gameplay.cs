using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassGame
{
    class Gameplay
    {
        #region ATTRIBUTES
        public Player player;

        Random r;

        public string selectedPathImage;

        public int turns;
        #endregion

        #region METHODS
        public Gameplay()
        {
            player = new Player();
            r = new Random();
        }

        public void Start()
        {
            this.turns = r.Next(24, 49);
        }

        public string GetPathImageByIdx(int idx)
        {
            int[] appleList = { 2,3,9,15,22};
            if (appleList.Contains(idx)) return "apple.png";
            if (idx == 1 || idx == 13 || idx == 14) return "ambarella.png";
            if (idx == 4 || idx == 5) return "bar.png";
            if (idx == 6 || idx == 8 || idx == 18) return "bell.png";
            if (idx == 7 || idx == 19 || idx == 20) return "orange.png";
            if (idx == 11 || idx == 12) return "star.png";
            if (idx == 16 || idx == 17 ) return "77.png";
            return "watermelon.png";
        }

        public void Finish()
        {
            int idx = turns % 24;
            selectedPathImage = GetPathImageByIdx(idx);
            if (idx == 0)
            {
                player.bonusWin += player.watermelonPoint * 20;
            }
            if(idx == 1)
            {
                player.bonusWin += player.ambarellaPoint * 10;
            }
            if (idx == 2)
            {
                player.bonusWin += player.applePoint * 3;
            }
            if (idx == 3)
            { 
                player.bonusWin += player.applePoint * 10;
            }
            if (idx == 4)
            {
                player.bonusWin += player.barPoint * 100;
            }
            if (idx == 5)
            {
                player.bonusWin += player.barPoint * 50;
            }
            if (idx == 6)
            {
                player.bonusWin += player.bellPoint * 3;
            }
            if (idx == 7)
            {
                player.bonusWin += player.orangePoint * 10;
            }
            if (idx == 8)
            {
                player.bonusWin += player.bellPoint * 3;
            }
            if (idx == 9)
            {
                player.bonusWin += player.applePoint * 10;
            }           
            if (idx == 11)
            {
                player.bonusWin += player.starPoint * 6;
            }
            if (idx == 12)
            {
                player.bonusWin += player.starPoint * 20;
            }
            if (idx == 13)
            {
                player.bonusWin += player.ambarellaPoint * 10;
            }
            if (idx == 14)
            {
                player.bonusWin += player.ambarellaPoint * 3;
            }
            if (idx == 15)
            {
                player.bonusWin += player.applePoint * 10;
            }
            if (idx == 16)
            {
                player.bonusWin += player.sevenSevenPoint * 20;
            }
            if (idx == 17)
            {
                player.bonusWin += player.sevenSevenPoint * 6;
            }
            if (idx == 18)
            {
                player.bonusWin += player.bellPoint * 10;
            }
            if (idx == 19)
            {
                player.bonusWin += player.orangePoint * 10;
            }
            if (idx == 20)
            {
                player.bonusWin += player.orangePoint * 3;
            }
            if (idx == 21)
            {
                player.bonusWin += player.applePoint * 10;
            }           
            if (idx == 23)
            {
                player.bonusWin += player.watermelonPoint * 6;
            }           
        }

        public bool IsBig()
        {
            int diceNumber1 = r.Next(1, 7);
            int diceNumber2 = r.Next(1, 7);
            int diceNumber3 = r.Next(1, 7);

            int total = diceNumber1 + diceNumber2 + diceNumber3;

            return total > 10;
        }             

        public void Roulette(String mode) {
            if(mode == "Tài")
            {
                if (IsBig()) player.bonusWin *= 2;
                else player.bonusWin = 0;
            }
            else if(mode == "Xỉu")
            {
                if (!IsBig()) player.bonusWin *= 2;
                else player.bonusWin = 0;
            }
            
        }

        public void MinusPointsBet()
        {
            int total = player.barPoint + player.sevenSevenPoint + player.starPoint + player.watermelonPoint
                    + player.bellPoint + player.ambarellaPoint + player.orangePoint + player.applePoint;
            player.coin -= total;           
        }

        public bool IsEnoughCoinBets()
        {
            int total = player.barPoint + player.sevenSevenPoint + player.starPoint + player.watermelonPoint
                    + player.bellPoint + player.ambarellaPoint + player.orangePoint + player.applePoint;
            return total <= player.coin;
        }

        public List<int> HowlMode()
        {
            int mode = r.Next(1, 4);
            List<int> results = new List<int>();
            //Hú 3 điểm
            if(mode == 1)
            {
                int idx1, idx2, idx3;
                do
                {
                    idx1 = r.Next(1, 25);
                    idx2 = r.Next(1, 25);
                    idx3 = r.Next(1, 25);
                } while (idx1 == idx2 || idx2 == idx3 || idx1 == idx3 );
                results.Add(idx1); results.Add(idx2); results.Add(idx3);            
            }            
            //Hú 1 điểm lớn
            else if(mode == 2)
            {
                List<int> ds =new List<int>()
                {
                    1,3,4,6,7,9,12,13,15,16,18,19,21,24
                };
                
                int idx1;
                do
                {
                    idx1 = r.Next(1, 25);
                } while (!ds.Contains(idx1));
                results.Add(idx1);
            }
            //Hú chết
            else if(mode == 3)
            {
            }
            return results;
        }
        #endregion
    }
}
