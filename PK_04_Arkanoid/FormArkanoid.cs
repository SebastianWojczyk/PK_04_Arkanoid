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
        Point Direction;
        public FormArkanoid()
        {
            InitializeComponent();
        }

        private void FormArkanoid_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
            }
            else if (e.KeyCode == Keys.Right)
            {
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            

            if (Ball.X < 0 || Ball.X + Ball.Width > pictureBoxBoard.Width)
            {
                Direction.X = -Direction.X;
            }
            if (Ball.Y < 0)
            {
                Direction.Y = -Direction.Y;
            }

            Ball.X += Direction.X;
            Ball.Y += Direction.Y;

            g.Clear(Color.Black);
            
            g.FillEllipse(new SolidBrush(Color.Yellow), Ball);
            g.DrawEllipse(new Pen(Color.Red, 5), Ball);
            
            pictureBoxBoard.Refresh();

            if(Ball.Y + Ball.Height > pictureBoxBoard.Height)
            {
                timer.Stop();
                MessageBox.Show("PRZEGRANA");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pictureBoxBoard.Dock = System.Windows.Forms.DockStyle.Fill;

            pictureBoxBoard.Image = new Bitmap(pictureBoxBoard.Width, pictureBoxBoard.Height);
            g = Graphics.FromImage(pictureBoxBoard.Image);

            Ball = new Rectangle(300, 300, 100, 100);
            Direction = new Point(15, 20);
        }
    }
}
