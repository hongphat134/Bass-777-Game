using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BassGame
{
    class Player
    {
        #region ATTRIBUTES
        public int barPoint = 0;
        public int starPoint = 0;
        public int watermelonPoint = 0;
        public int sevenSevenPoint = 0;
        public int ambarellaPoint = 0;
        public int orangePoint = 0;
        public int applePoint = 0;
        public int bellPoint = 0;

        public int coin = 500;

        public int bonusWin = 0;
        #endregion

        #region METHODS
        public void ResetBets()
        {
            this.barPoint = 0;
            this.starPoint = 0;
            this.watermelonPoint = 0;
            this.sevenSevenPoint = 0;
            this.ambarellaPoint = 0;
            this.orangePoint = 0;
            this.applePoint = 0;
            this.bellPoint = 0;
        }
        public void Bets(int barPoint, int sevenSevenPoint,int starPoint,int watermelonPoint,
                    int bellPoint,int ambarellaPoint,int orangePoint,int applePoint)
        {
            this.barPoint = barPoint;
            this.starPoint = starPoint;
            this.watermelonPoint = watermelonPoint;
            this.sevenSevenPoint = sevenSevenPoint;
            this.ambarellaPoint = ambarellaPoint;
            this.orangePoint = orangePoint;
            this.applePoint = applePoint;
            this.bellPoint = bellPoint;
        }

        #endregion
    }
}
