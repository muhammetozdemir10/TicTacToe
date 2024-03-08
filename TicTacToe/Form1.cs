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
        /*
         * Tic Tac Toe Oyunu
         * Hakkinda: Bu projede tic tac toe oyunu iki oyuncu 
         * Red ve Blue oyuncular olmak uzere iki kisilik bir oyundur. 
         */


        // Kirmizi oyuncunun degiskeni
        Player redPlayer;

        // Mavi oyuncunun degiskeni
        Player bluePlayer;

        // Sirasi olan oyuncunun Id'si
        int currentPlayerId = 0;

        public Form1()
        {
            // Bu fonksiyon Windows Form bilesenleri calismaya hazirlar.
            InitializeComponent();
        }


        /// <summary>
        /// Bu fonksiyonda oyunun icerisindeki 9 Button'unlarin Click eyleminde cagrilmaktadir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {
            // sender'i Button olarak unbox ettik
            var button = sender as Button;

            // Secilen bir butonun yerini farkli bir oyuncunun secmesini engeller.
            if (redPlayer.SelectedButtons.Contains(button.Name) || bluePlayer.SelectedButtons.Contains(button.Name))
            {
                // Fonksiyondan cikis yapilir.
                return;
            }

            // Su an ki oyuncu kirmizi oyuncu olup olmadigini kontrol eder.
            if (currentPlayerId == redPlayer.Id)
            {
                // Secilen butonun arka plan rengi kirmizi yapar.
                button.BackColor = Color.Red;

                // Su an ki oyuncunun hamlesi diger oyuncuya atanir. 
                currentPlayerId = bluePlayer.Id;
                CurrentPlayerLabel.Text = "Current: BLUE";
                CurrentPlayerLabel.ForeColor = Color.Blue;

                // Secilen butonun adini oyuncunun secilen butonlarin listesine ekler.
                redPlayer.SelectedButtons.Add(button.Name);

                // CheckPlayerIsWin Fonksiyonu Kazanan oyuncuyu kontrol eder
                CheckPlayerIsWin(redPlayer);

                // Fonksiyondan cikis yapilir.
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

        /// <summary>
        /// Start Game botununa tiklandiginda cagrilan fonksiyon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGameButton_Click(object sender, EventArgs e)
        {
            // Kirmizi oyuncunun degiskenine yeni bir Player nesnesi atanir.
            redPlayer = new Player(1);

            // Mavi oyuncunun degiskenine yeni bir Player nesnesi atanir.
            bluePlayer = new Player(2);

            /* 
             * Kirmizi oyuncunun RadioButton'u veya
             * mavi oyuncunun RadioButton'u secili olup olmadigini kontrol eder
            */
            if (RedPlayerRadioButton.Checked == true || BluePlayerRadioButton.Checked == true)
            {

                // Kirmizi oyuncunun RadioButton secili oldugunu kontrol eder
                if (RedPlayerRadioButton.Checked == true)
                {
                    // Suanki oyuncuun sirasi gosteren Label'in yazisi degistirilir.
                    CurrentPlayerLabel.Text = "Current: RED";

                    // Suanki oyuncuun sirasi gosteren Label'in rengi degistirilir.
                    CurrentPlayerLabel.ForeColor = Color.Red;

                    // Suanki oyuncuun Id'si currentPlayerId degiskenine atanir.
                    currentPlayerId = redPlayer.Id;
                }

                // Mavi oyuncunun RadioButton secili oldugunu kontrol eder
                if (BluePlayerRadioButton.Checked == true)
                {
                    // Suanki oyuncuun sirasi gosteren Label'in yazisi degistirilir.
                    CurrentPlayerLabel.Text = "Current: BLUE";

                    // Suanki oyuncuun sirasi gosteren Label'in rengi degistirilir.
                    CurrentPlayerLabel.ForeColor = Color.Blue;

                    // Suanki oyuncuun Id'si currentPlayerId degiskenine atanir.
                    currentPlayerId = bluePlayer.Id;
                }


                // Oyunun Button'lari aktif edilir (Oyunun alanin buttonlari sadece)
                btn1.Enabled = true;
                btn2.Enabled = true;
                btn3.Enabled = true;
                btn4.Enabled = true;
                btn5.Enabled = true;
                btn6.Enabled = true;
                btn7.Enabled = true;
                btn8.Enabled = true;
                btn9.Enabled = true;

                // StartGameButton'u gizler
                StartGameButton.Visible = false;
            }
            else
            {
                // Herhangi bir secim yapilmazsa uyari mesaji verilir.
                MessageBox.Show("Kirmizi veya Mavi oyunculardan biri secilmelidir");
            }
        }


        // ResetButton'un Click eyleminde cagrilan fonksiyon
        private void ResetButton_Click(object sender, EventArgs e)
        {
            // Burada secenekli bir MessageBox olusturulur. Sonucu result degiskenine atanir.
            DialogResult result = MessageBox.Show(
                "Oyunu resetlemek istediginizden eminmisiniz?",
                "UYARI",
                MessageBoxButtons.OKCancel
                );

            // Kullanici OK butona tikladigini kontrol eder
            if (result == DialogResult.OK)
            {
                // Kirmizi oyuncunun degiskenine null degeri atanir.
                redPlayer = null;

                // Mavi oyuncunun degiskenine null degeri atanir.
                bluePlayer = null;

                // Suanki sirasi olan oyuncunun Id'si sifir olarak atanir.
                currentPlayerId = 0;

                #region Oyunun butonlari sifirlar

                // Butonun deaktif eder.
                btn1.Enabled = false;

                // Butonun rengi degistirir.
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

                #endregion

                // Suanki oyuncunun sirasi gosteren Label yazisi degistirilir.
                CurrentPlayerLabel.Text = "Current:";

                // Suanki oyuncunun sirasi gosteren Label yazi rengi siyah olarak tanimlanir.
                CurrentPlayerLabel.ForeColor = Color.Black;

                // Kirmizi oyuncunun RadioButton tusu secimini kaldirir.
                RedPlayerRadioButton.Checked = false;

                // Mavi oyuncunun RadioButton tusu secimini kaldirir.
                BluePlayerRadioButton.Checked = false;

                // StartGameButton'u gizlenmesi kaldirir.
                StartGameButton.Visible = true;

                // Mavi oyuncunun skor Label'in yazisi degistirir.
                BlueScoreLabel.Text = "Score:";

                // Kirmizi oyuncunun skor Label'in yazisi degistirir.
                RedScoreLabel.Text = "Score:";
            }
        }

        /// <summary>
        /// Kazanan oyuncuyu kontrol eder
        /// </summary>
        /// <param name="player">Kontrol edilecek oyuncunun nesnesi</param>
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
            // ------------------------------------------
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
