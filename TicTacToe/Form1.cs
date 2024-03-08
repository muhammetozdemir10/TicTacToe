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
                CheckPlayerIsWin(bluePlayer);
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
            List<List<string>> patterns = new List<List<string>>()
            {
                // Yatay Butonlar
                new List<string>() { "btn1", "btn2", "btn3" },
                new List<string>() { "btn4", "btn5", "btn6" },
                new List<string>() { "btn7", "btn8", "btn9" },

                // Dikey Butonlar
                new List<string>() { "btn1", "btn4", "btn7" },
                new List<string>() { "btn2", "btn5", "btn8" },
                new List<string>() { "btn3", "btn6", "btn9" },

                // Capraz Buttonlar
                new List<string>() { "btn1", "btn5", "btn9" },
                new List<string>() { "btn3", "btn5", "btn7" },
            };

            foreach (List<string> pattern in patterns)
            {
                int findedButtons = 0;
                foreach (string buttonName in pattern)
                {
                    int buttonIndex = player.SelectedButtons.FindIndex((x) => x == buttonName);
                    if (buttonIndex > -1)
                    {
                        findedButtons++;
                    }
                }

                if (findedButtons == 3)
                {
                    switch (player.Id)
                    {
                        // Kirmizi oyuncu
                        case 1:
                            MessageBox.Show("RED Kazandi");
                            player.Score++;
                            RedScoreLabel.Text = $"Score: {player.Score}";
                            break;

                        // Mavi oyuncu
                        case 2:
                            MessageBox.Show("BLUE Kazandi");
                            player.Score++;
                            BlueScoreLabel.Text = $"Score: {player.Score}";
                           
                            break;

                        default:
                            break;
                    }

                    NewRound();
                    return;
                }
            }

            if (redPlayer.SelectedButtons.Count + bluePlayer.SelectedButtons.Count == 9)
            {
                MessageBox.Show("Berabere kaldiniz");
                NewRound();
            }
        }

        void NewRound()
        {
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

            redPlayer.SelectedButtons.Clear();
            bluePlayer.SelectedButtons.Clear();
        }
    }
}
