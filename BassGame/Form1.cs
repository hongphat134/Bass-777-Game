﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BassGame
{
    public partial class FormBassGame : Form
    {
        
        #region ATTRIBUTES
        Gameplay gameplay;
        public int turns = 999;
        public List<int> howlList;
        public int ptrbIdxHList = 0;
        public int flashLuckCount;
        public bool flashLuckflag;
  
        Color DEFAULT_LIGHT_COLOR = Color.Black;
        Color LIGHT_ON_COLOR = Color.Red;
        #endregion
        public FormBassGame()
        {
            InitializeComponent();
            
            gameplay = new Gameplay();
            howlList = new List<int>();

            lblCredit.Text = gameplay.player.coin.ToString();
            lblBonusWin.Text = gameplay.player.bonusWin.ToString();
            timerDownPtr.Start();
        }

        public void SetPictureForDown(String imageLink)
        {
            ptrbPtrDown.Location = new Point(7, -45);
            ptrbPtrDown.Image = Image.FromFile("../" + imageLink);
            timerDownPtr.Start();
        }

        private void timerCircular_Tick(object sender, EventArgs e)
        {
            int idx = turns % 24;
            if (turns == gameplay.turns)
            {                
                timerCircular.Stop();
                if( (turns % 24 == 10) || (turns % 24 == 22))
                {
                    flashLuckCount = 19;
                    flashLuckflag = false;
                    timerFlashLuck.Start();
                }
                else
                {
                    btnStart.Click += btnStart_Click;
                    btnBig.Click += btnBig_Click;
                    btnSmall.Click += btnSmall_Click;
                    btnSend.Click += btnSend_Click;
                    
                    gameplay.Finish();
                    SetPictureForDown(gameplay.selectedPathImage);
                    gameplay.player.ResetBets();
                    lblBonusWin.Text = gameplay.player.bonusWin.ToString();
                }               
            }
            
            
            if (idx == 1 && turns >= 24)
            {                
                this.Controls["pnlGameBoard"].Controls["ptrbLight1"].BackColor = LIGHT_ON_COLOR;
                this.Controls["pnlGameBoard"].Controls["ptrbLight24"].BackColor = DEFAULT_LIGHT_COLOR;                
            }
            else
            {

                if (idx == 0)
                {
                    if (turns >= 24) this.Controls["pnlGameBoard"].Controls["ptrbLight24"].BackColor = LIGHT_ON_COLOR;
                    else this.Controls["pnlGameBoard"].Controls["ptrbLight1"].BackColor = LIGHT_ON_COLOR;                        
                    
                    this.Controls["pnlGameBoard"].Controls["ptrbLight23"].BackColor = DEFAULT_LIGHT_COLOR;
                }
                else
                {
                    this.Controls["pnlGameBoard"].Controls["ptrbLight" + idx].BackColor = LIGHT_ON_COLOR;
                    if (idx != 1) this.Controls["pnlGameBoard"].Controls["ptrbLight" + (idx - 1)].BackColor = DEFAULT_LIGHT_COLOR;
                }
            }
            turns++;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            gameplay.Start();            
           
            int barPoint =Int32.Parse(txtBar.Text);
            int sevenSevenPoint = Int32.Parse(txtSevenSeven.Text);
            int starPoint = Int32.Parse(txtStar.Text);
            int watermelonPoint = Int32.Parse(txtWatermelon.Text);
            int bellPoint = Int32.Parse(txtBell.Text);
            int ambarellaPoint = Int32.Parse(txtAmbarella.Text);
            int orangePoint = Int32.Parse(txtOrange.Text);
            int applePoint =Int32.Parse(txtApple.Text);
            gameplay.player.Bets(barPoint,
                            sevenSevenPoint,
                            starPoint,
                            watermelonPoint,
                            bellPoint,
                            ambarellaPoint,
                            orangePoint,
                            applePoint);
            if (gameplay.IsEnoughCoinBets())
            {
                timerCircular.Start();

                gameplay.MinusPointsBet();
                lblCredit.Text = gameplay.player.coin.ToString();
                btnStart.Click -= btnStart_Click;
                btnBig.Click -= btnBig_Click;
                btnSmall.Click -= btnSmall_Click;
                btnSend.Click -= btnSend_Click;

                //Reset tất cả đèn về DEFAULT
                if (turns != 999)
                {
                    if (turns % 24 == 0) this.Controls["pnlGameBoard"].Controls["ptrbLight23"].BackColor = DEFAULT_LIGHT_COLOR;
                    else if (turns % 24 == 1) this.Controls["pnlGameBoard"].Controls["ptrbLight24"].BackColor = DEFAULT_LIGHT_COLOR;
                    else this.Controls["pnlGameBoard"].Controls["ptrbLight" + ((turns % 24) - 1)].BackColor = DEFAULT_LIGHT_COLOR;

                    foreach (int ptrbIndex in howlList)
                    {
                        this.Controls["pnlGameBoard"].Controls["ptrbLight" + ptrbIndex].BackColor = DEFAULT_LIGHT_COLOR;
                    }
                    howlList.Clear();
                    ptrbIdxHList = 0;
                    ptrbPtrDown.Location = new Point(7, -45);
                }
                turns = 1;
            }
            else MessageBox.Show("Bạn ko đủ xu để đặt!");
            
        }

        private void btnBig_Click(object sender, EventArgs e)
        {
            gameplay.Roulette("Tài");
            lblBonusWin.Text = gameplay.player.bonusWin.ToString();
        }

        private void btnSmall_Click(object sender, EventArgs e)
        {
            gameplay.Roulette("Xỉu");
            lblBonusWin.Text = gameplay.player.bonusWin.ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            gameplay.player.coin += gameplay.player.bonusWin;
            gameplay.player.bonusWin = 0;
            lblBonusWin.Text = gameplay.player.bonusWin.ToString();
            lblCredit.Text = gameplay.player.coin.ToString();
        }

        private void timerHowl_Tick(object sender, EventArgs e)
        {
            if (ptrbIdxHList == howlList.Count) {
                timerHowl.Stop();                
                btnStart.Click += btnStart_Click;
                btnBig.Click += btnBig_Click;
                btnSmall.Click += btnSmall_Click;
                btnSend.Click += btnSend_Click;
            }
            else
            {
                gameplay.turns = howlList[ptrbIdxHList];
                gameplay.Finish();
                SetPictureForDown(gameplay.selectedPathImage);
                lblBonusWin.Text = gameplay.player.bonusWin.ToString();
                this.Controls["pnlGameBoard"].Controls["ptrbLight" + howlList[ptrbIdxHList]].BackColor = LIGHT_ON_COLOR;
                ptrbIdxHList++;
            }
        }

        private void timerDownPtr_Tick(object sender, EventArgs e)
        {
            if (ptrbPtrDown.Location.Y == 0) timerDownPtr.Stop();
            ptrbPtrDown.Location = new Point(ptrbPtrDown.Location.X, ptrbPtrDown.Location.Y + 5);
        }

        private void timerFlashLuck_Tick(object sender, EventArgs e)
        {
            if (flashLuckCount == 0)
            {
                timerFlashLuck.Stop();
                timerHowl.Start();
                howlList = gameplay.HowlMode();
            }
            else
            {
                int idx = (turns % 24) - 1;
                if (!flashLuckflag)
                {
                    this.Controls["pnlGameBoard"].Controls["ptrbLight" + idx].BackColor = LIGHT_ON_COLOR;
                    flashLuckflag = true;
                }
                else
                {
                    this.Controls["pnlGameBoard"].Controls["ptrbLight" + idx].BackColor = DEFAULT_LIGHT_COLOR;
                    flashLuckflag = false;
                }
                    flashLuckCount--;
            }

            
        }

        private void btnBar_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtBar.Text);
            if(oldNumber != 99) txtBar.Text = (oldNumber + 1).ToString();
        }

        private void btnSevenSeven_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtSevenSeven.Text);
            if (oldNumber != 99) txtSevenSeven.Text = (oldNumber + 1).ToString();
        }

        private void btnStar_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtStar.Text);
            if (oldNumber != 99) txtStar.Text = (oldNumber + 1).ToString();
        }

        private void btnWatermelon_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtWatermelon.Text);
            if (oldNumber != 99) txtWatermelon.Text = (oldNumber + 1).ToString();
        }

        private void btnBell_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtBell.Text);
            if (oldNumber != 99) txtBell.Text = (oldNumber + 1).ToString();
        }

        private void btnAmbarella_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtAmbarella.Text);
            if (oldNumber != 99) txtAmbarella.Text = (oldNumber + 1).ToString();            
        }

        private void btnOrange_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtOrange.Text);
            if (oldNumber != 99) txtOrange.Text = (oldNumber + 1).ToString();
        }

        private void btnApple_Click(object sender, EventArgs e)
        {
            int oldNumber = Int32.Parse(txtApple.Text);
            if (oldNumber != 99) txtApple.Text = (oldNumber + 1).ToString();
        }      
      
    }
}