using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacingGame
{
    public partial class Form1 : Form
    {
       

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        int roadSpeed;
        int trafficSpeed;
        int playerSpeed = 12;
        int score;
        int carImage;


        Random rand = new Random();
        Random carPosition = new Random();

        bool goleft, goright;


        public Form1()
{
    InitializeComponent();
    gameTimer.Stop(); // Timer başlangıçta durdurulur
    ResetGame();      // ResetGame çağırılır ama timer başlatılmaz
    this.KeyDown += new KeyEventHandler(keyisdown);
    this.KeyUp += new KeyEventHandler(keyisup);
    this.KeyPreview = true;
}




        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
        }
       
        private void changeAIcars(PictureBox tempCar)
        {

            carImage = rand.Next(1, 9);

            switch (carImage)
            {

                case 1:
                    tempCar.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_174928_removebg_preview;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_162404_removebg_preview;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_162418_removebg_preview;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_162349_removebg_preview;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 9:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
            }


            tempCar.Top = carPosition.Next(100, 400) * -1;


            if ((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(5, 200);
            }
            if ((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(245, 422);
            }
        }

        private void gameOver()
        {
            gameTimer.Stop(); // Timer durdurulur
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            award.Visible = true;
            award.BringToFront();

            btnStart.Enabled = true; // Buton tekrar aktif edilir
        }


        private void ResetGame()
        {
            score = 0; // Skor sıfırlanır
            txtScore.Text = "SCORE: 0"; // Skor ekranı güncellenir
        
            explosion.Visible = false;
            award.Visible = false;

            goleft = false;
            goright = false;

           
            award.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_194530;

            roadSpeed = 12;
            trafficSpeed = 15;

            AI1.Top = carPosition.Next(200, 500) * -1;
            AI1.Left = carPosition.Next(5, 200);

            AI2.Top = carPosition.Next(200, 500) * -1;
            AI2.Left = carPosition.Next(245, 422);

            player.Left = 200; // Oyuncunun başlangıç pozisyonu
            player.Top = 423;

           
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ResetGame();       // Oyunu sıfırlar
            gameTimer.Start(); // Timer başlatılır
            btnStart.Enabled = false; // Buton devre dışı bırakılır
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Skor güncelleme
            score++;
            txtScore.Text = "SCORE: " + score;

            // Ödül resmini güncelleme
            if (score > 40 && score < 500)
            {
                award.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_194530;
            }
            else if (score >= 500 && score < 1000)
            {
                award.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_174815;
                roadSpeed = 19;
                trafficSpeed = 20;
            }
            else if (score >= 1000)
            {
                award.Image = Properties.Resources.Ekran_görüntüsü_2024_12_02_174551;
                roadSpeed = 20;
                trafficSpeed = 21;
            }

            // Yol hareketi
            roadTrack1.Top += roadSpeed;
            roadTrack2.Top += roadSpeed;

            if (roadTrack1.Top > 519)
            {
                roadTrack1.Top = -519;
            }

            if (roadTrack2.Top > 519)
            {
                roadTrack2.Top = -519;
            }

            // Arabaların hareketi
            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            // Eğer araba ekran dışına çıkarsa yeniden pozisyon ayarla
            if (AI1.Top > 530)
            {
                changeAIcars(AI1);
            }

            if (AI2.Top > 530)
            {
                changeAIcars(AI2);
            }

            // Oyuncunun hareketi
            if (goleft && player.Left > 10)
            {
                player.Left -= playerSpeed;
            }

            if (goright && player.Left < 415)
            {
                player.Left += playerSpeed;
            }

            // Çarpışma kontrolü
            if (player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameOver();
            }
        }


        private void restartGame(object sender, EventArgs e)
        {
            ResetGame();
        }

        
    }
}
