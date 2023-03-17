using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PK_04_Arkanoid
{
    public partial class FormArkanoid : Form
    {
        Graphics g;

        Rectangle Ball;
        Point BallDirection;
        Rectangle Player;
        Int32 PlayerSpeed;
        Keys PlayerDirection;
        List<Rectangle> Bricks;
        Int32 Score;

        public FormArkanoid()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //poruszanie graczem
            if (PlayerDirection == Keys.Left)
            {
                Player.X -= PlayerSpeed;
            }
            else if (PlayerDirection == Keys.Right)
            {
                Player.X += PlayerSpeed;
            }

            //kolizje piłki
            if (Ball.X < 0 || Ball.X + Ball.Width > pictureBoxBoard.Width)
            {
                BallDirection.X = -BallDirection.X;
            }
            else if (Ball.Y < 0)
            {
                BallDirection.Y = -BallDirection.Y;
            }
            else if (Ball.IntersectsWith(Player))
            {
                BallDirection.Y = -BallDirection.Y;
            }
            else
            {
                Rectangle brickToRemove = Rectangle.Empty;
                foreach (Rectangle brick in Bricks)
                {
                    if (Ball.IntersectsWith(brick))
                    {
                        BallDirection.Y = -BallDirection.Y;
                        brickToRemove = brick;
                        Score++;
                        break;
                    }
                }
                Bricks.Remove(brickToRemove);
            }

            Ball.X += BallDirection.X;
            Ball.Y += BallDirection.Y;



            //odrysowanie gry
            g.Clear(Color.Black);

            foreach (Rectangle brick in Bricks)
            {
                g.FillRectangle(new SolidBrush(Color.Yellow), brick);
                g.DrawRectangle(new Pen(Color.Red, 5), brick);
            }
            g.FillEllipse(new SolidBrush(Color.Yellow), Ball);
            g.DrawEllipse(new Pen(Color.Red, 5), Ball);

            g.FillRectangle(new SolidBrush(Color.Yellow), Player);
            g.DrawRectangle(new Pen(Color.Red, 5), Player);

            String s;
            //s = "Punkty: " + Score.ToString();
            //s = "Punkty: " + Score;
            //s = String.Format("{0}: {1}", "Punkty", Score);
            s = $"Punkty: {Score}";
            g.DrawString(s,
                         new Font("Courier New",
                                  40F,
                                  ((FontStyle)((FontStyle.Bold | FontStyle.Italic)))),
                         new SolidBrush(Color.Green),
                         10, 10);

            String info = "";
            if (Ball.Y + Ball.Height > pictureBoxBoard.Height)
            {
                timer.Stop();
                info = "PRZEGRANA\nCzy chcesz jeszcze raz? (T/N)";
            }
            else if (Bricks.Count == 0)
            {
                timer.Stop();
                info = "WYGRANA\nCzy chcesz jeszcze raz? (T/N)";
            }
            g.DrawString(info,
                        new Font("Courier New",
                                 40F,
                                 ((FontStyle)((FontStyle.Bold | FontStyle.Italic)))),
                        new SolidBrush(Color.Green),
                        50, 200);
            pictureBoxBoard.Refresh();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pictureBoxBoard.Dock = System.Windows.Forms.DockStyle.Fill;

            pictureBoxBoard.Image = new Bitmap(pictureBoxBoard.Width, pictureBoxBoard.Height);
            g = Graphics.FromImage(pictureBoxBoard.Image);

            prepareNewGame();
        }

        private void prepareNewGame()
        {
            Ball = new Rectangle((int)(pictureBoxBoard.Width * 0.3),
                                   (int)(pictureBoxBoard.Height * 0.3),
                                   (int)(pictureBoxBoard.Width * 0.05),
                                   (int)(pictureBoxBoard.Width * 0.05));
            BallDirection = new Point(15, 20);

            Player = new Rectangle((int)(pictureBoxBoard.Width * 0.4),
                                   (int)(pictureBoxBoard.Height * 0.95),
                                   (int)(pictureBoxBoard.Width * 0.2),
                                   (int)(pictureBoxBoard.Height * 0.03));
            PlayerSpeed = 15;

            Bricks = new List<Rectangle>();
            {
                int xMax = 2;
                int yMax = 2;
                for (int x = 0; x < xMax; x++)
                {
                    for (int y = 0; y < yMax; y++)
                    {
                        Bricks.Add(new Rectangle(pictureBoxBoard.Width / xMax * x,
                                                 (int)(pictureBoxBoard.Height * 0.08) * y,
                                                 pictureBoxBoard.Width / xMax,
                                                 (int)(pictureBoxBoard.Height * 0.08)
                                                ));
                    }
                }
            }
            Score = 0;

            timer.Start();
        }

        private void FormArkanoid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                PlayerDirection = Keys.Left;
            }
            else if (e.KeyCode == Keys.Right)
            {
                PlayerDirection = Keys.Right;
            }
            else if (!timer.Enabled)
            {
                if (e.KeyCode == Keys.T)
                {
                    prepareNewGame();
                }
                else if (e.KeyCode == Keys.N)
                {
                    this.Close();
                }
            }
        }
        private void FormArkanoid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && PlayerDirection == Keys.Left)
            {
                PlayerDirection = Keys.None;
            }
            else if (e.KeyCode == Keys.Right && PlayerDirection == Keys.Right)
            {
                PlayerDirection = Keys.None;
            }
        }
    }
}
