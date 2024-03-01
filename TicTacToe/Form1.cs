using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Models;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Player redPlayer;
        Player bluePlayer;
        int currentPlayerId = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            // sender'i Button olarak unbox ettik
            var button = sender as Button;

            // Secilen bir butonun yerini farkli bir oyuncunun secmesini engeller.
            if (redPlayer.SelectedButtons.Contains(button.Name) || bluePlayer.SelectedButtons.Contains(button.Name))
            {
                return;
            }

            // Su an ki oyuncu kirmizi oyuncu olup olmadigini kontrol eder.
            if (currentPlayerId == redPlayer.Id)
            {
                // Secilen butonun arka olan rengi kirmizi yapar.
                button.BackColor = Color.Red;

                // Su an ki oyuncunun hamlesi diger oyuncuya atanir. 
                currentPlayerId = bluePlayer.Id;
                CurrentPlayerLabel.Text = "Current: BLUE";
                CurrentPlayerLabel.ForeColor = Color.Blue;

                // Secilen butonun adini oyuncunun secilen butonlarin listesine ekler.
                redPlayer.SelectedButtons.Add(button.Name);
                CheckPlayerIsWin(redPlayer);

                return;
            }

            // Su an ki oyuncu mavi oyuncu olup olmadigini kontrol eder.
            if (currentPlayerId == bluePlayer.Id)
            {
                // Secilen butonun arka olan rengi mavi yapar.
                button.BackColor = Color.Blue;

                // Su an ki oyuncunun hamlesi diger oyuncuya atanir. 
                currentPlayerId = redPlayer.Id;
                CurrentPlayerLabel.Text = "Current: RED";
                CurrentPlayerLabel.ForeColor = Color.Red;

                // Secilen butonun adini oyuncunun secilen butonlarin listesine ekler.
                bluePlayer.SelectedButtons.Add(button.Name);
                return;
            }
        }

        private void StartGameButton_Click(object sender, EventArgs e)
        {
            redPlayer = new Player(1);
            bluePlayer = new Player(2);

            if (RedPlayerRadioButton.Checked == true || BluePlayerRadioButton.Checked == true)
            {
                if (RedPlayerRadioButton.Checked == true)
                {
                    CurrentPlayerLabel.Text = "Current: RED";
                    CurrentPlayerLabel.ForeColor = Color.Red;
                    currentPlayerId = redPlayer.Id;
                }

                if (BluePlayerRadioButton.Checked == true)
                {
                    CurrentPlayerLabel.Text = "Current: BLUE";
                    CurrentPlayerLabel.ForeColor = Color.Blue;
                    currentPlayerId = bluePlayer.Id;
                }

                btn1.Enabled = true;
                btn2.Enabled = true;
                btn3.Enabled = true;
                btn4.Enabled = true;
                btn5.Enabled = true;
                btn6.Enabled = true;
                btn7.Enabled = true;
                btn8.Enabled = true;
                btn9.Enabled = true;

                StartGameButton.Visible = false;
            }
            else
            {
                MessageBox.Show("Kirmizi veya Mavi oyunculardan biri secilmelidir");
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Oyunu resetlemek istediginizden eminmisiniz?",
                "UYARI",
                MessageBoxButtons.OKCancel
                );

            if (result == DialogResult.OK)
            {
                redPlayer = null;
                bluePlayer = null;

                currentPlayerId = 0;

                btn1.Enabled = false;
                btn1.BackColor = SystemColors.Control;

                btn2.Enabled = false;
                btn2.BackColor = SystemColors.Control; 

                btn3.Enabled = false;
                btn3.BackColor = SystemColors.Control;

                btn4.Enabled = false;
                btn4.BackColor = SystemColors.Control;

                btn5.Enabled = false;
                btn5.BackColor = SystemColors.Control;

                btn6.Enabled = false;
                btn6.BackColor = SystemColors.Control;

                btn7.Enabled = false;
                btn7.BackColor = SystemColors.Control;

                btn8.Enabled = false;
                btn8.BackColor = SystemColors.Control;

                btn9.Enabled = false;
                btn9.BackColor = SystemColors.Control;


                CurrentPlayerLabel.Text = "Current:";
                CurrentPlayerLabel.ForeColor = Color.Black;

                RedPlayerRadioButton.Checked = false;
                BluePlayerRadioButton.Checked = false;

                StartGameButton.Visible = true;

                BlueScoreLabel.Text = "Score:";
                RedScoreLabel.Text = "Score:";
            }
        }

        void CheckPlayerIsWin(Player player)
        {
            // Yatay Butonlar
            List<string> pattern1 = new List<string>() { "btn1", "btn2", "btn3" };
            List<string> pattern2 = new List<string>() { "btn4", "btn5", "btn6" };
            List<string> pattern3 = new List<string>() { "btn7", "btn8", "btn9" };

            // Dikey Butonlar
            List<string> pattern4 = new List<string>() { "btn1", "btn4", "btn7" };
            List<string> pattern5 = new List<string>() { "btn2", "btn5", "btn8" };
            List<string> pattern6 = new List<string>() { "btn3", "btn6", "btn9" };

            // Capraz Buttonlar
            List<string> pattern7 = new List<string>() { "btn1", "btn5", "btn9" };
            List<string> pattern8 = new List<string>() { "btn3", "btn5", "btn7" };


            bool pattern1Result = player.SelectedButtons.Any((x) => pattern1.Any((y) => x == y));
            MessageBox.Show(pattern1Result.ToString());
        }

        /*
         * TODO: Kazanma hamleleri algoritmasi yazilacak ve kazanan oyuncuya skora puan yazilacak.
         * TODO: Oyun bittiginde oyunu sifirdan baslasin yada yeni bir tur secenegi yapilacak.
         */
    }
}
