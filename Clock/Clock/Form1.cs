using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        private PictureBox pictureBox;
        private Timer timer;

        public Form1()
        {
            InitializeComponents();
            SetupTimer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponents()
        {
            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Paint += PictureBox_Paint;
            Controls.Add(pictureBox);
        }

        private void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Tick;
            timer.Start();
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawClock(e.Graphics);
        }

        private void Tick(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void DrawClock(Graphics graphics)
        {
            int width = pictureBox.Width;
            int height = pictureBox.Height;
            int radius = Math.Min(width, height) / 2;
            int center = width / 2;

            //Циферблат
            Pen pen = new Pen(Brushes.Black, 3);
            graphics.DrawEllipse(pen, center - radius, center - radius, radius * 2, radius * 2);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Font font = new Font("Arial", 16, FontStyle.Bold);
            for (int i = 1; i <= 12; i++)
            {
                double angle = i * 30 * Math.PI / 180;
                int x = center + (int)(radius * 0.8 * Math.Sin(angle));
                int y = center - (int)(radius * 0.8 * Math.Cos(angle));
                if (i % 3 == 0)
                {
                    graphics.DrawString(i.ToString(), font, Brushes.Black, x, y, stringFormat);
                }
                else { graphics.DrawEllipse(pen, x, y, 2, 2); }
            }

            pen.Width = 1;
            for (int i = 0; i < 60; i++)
            {
                double angle = i * 6 * Math.PI / 180;
                if (i % 5 == 0) pen.Width = 3;
                else pen.Width = 1;
                int x1 = center + (int)(radius * 0.99 * Math.Sin(angle));
                int y1 = center - (int)(radius * 0.99 * Math.Cos(angle));
                int x2 = center + (int)(radius * 0.9 * Math.Sin(angle));
                int y2 = center - (int)(radius * 0.9 * Math.Cos(angle));
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            //Часовая стрелка
            DateTime currentTime = DateTime.Now;
            double hoursAngle = (currentTime.Hour % 12 + currentTime.Minute / 60.0) * 30 * Math.PI / 180;
            int hourHandLength = radius / 2;
            pen.Width = 3;
            graphics.DrawLine(pen, center, center, center + (int)(hourHandLength * Math.Sin(hoursAngle)), center - (int)(hourHandLength * Math.Cos(hoursAngle)));

            //Минутная стрелка
            double minutesAngle = (currentTime.Minute + currentTime.Second / 60.0) * 6 * Math.PI / 180;
            int minuteHandLength = radius * 3 / 4;
            graphics.DrawLine(pen, center, center, center + (int)(minuteHandLength * Math.Sin(minutesAngle)), center - (int)(minuteHandLength * Math.Cos(minutesAngle)));

            // Секундная стрелка
            double secondsAngle = currentTime.Second * 6 * Math.PI / 180;
            int secondHandLength = radius * 4 / 5;
            pen.Color = Color.Red;
            pen.Width = 1;
            graphics.DrawLine(pen, center, center, center + (int)(secondHandLength * Math.Sin(secondsAngle)), center - (int)(secondHandLength * Math.Cos(secondsAngle)));
        }


    }
}
