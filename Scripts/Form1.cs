using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Часы
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
            timer.Interval = 1000; // Обновление каждую секунду
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            pictureBox.Invalidate(); // Перерисовать PictureBox
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawClock(e.Graphics);
        }

        private void DrawClock(Graphics graphics)
        {
            int width = pictureBox.Width;
            int height = pictureBox.Height;
            int radius = Math.Min(width, height) / 2;
            int centerX = width / 2;
            int centerY = height / 2;

            // Рисуем циферблат
            graphics.DrawEllipse(Pens.Black, centerX - radius, centerY - radius, radius * 2, radius * 2);

            // Рисуем цифры на циферблате
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            Font font = new Font("Arial", 16, FontStyle.Bold);
            for (int i = 1; i <= 12; i++)
            {
                double angle = i * 30 * Math.PI / 180;
                int x = centerX + (int)(radius * 0.7 * Math.Sin(angle));
                int y = centerY - (int)(radius * 0.7 * Math.Cos(angle));
                graphics.DrawString(i.ToString(), font, Brushes.Black, x, y, stringFormat);
            }

            // Рисуем деления на циферблате
            Pen pen = new Pen(Brushes.Black, 2);
            for (int i = 0; i < 60; i++)
            {
                double angle = i * 6 * Math.PI / 180;
                int x1 = centerX + (int)(radius * 0.9 * Math.Sin(angle));
                int y1 = centerY - (int)(radius * 0.9 * Math.Cos(angle));
                int x2 = centerX + (int)(radius * 0.8 * Math.Sin(angle));
                int y2 = centerY - (int)(radius * 0.8 * Math.Cos(angle));
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            // Рисуем часовую стрелку
            DateTime currentTime = DateTime.Now;
            double hoursAngle = (currentTime.Hour % 12 + currentTime.Minute / 60.0) * 30 * Math.PI / 180;
            int hourHandLength = radius / 2;
            graphics.DrawLine(Pens.Black, centerX, centerY, centerX + (int)(hourHandLength * Math.Sin(hoursAngle)), centerY - (int)(hourHandLength * Math.Cos(hoursAngle)));

            // Рисуем минутную стрелку
            double minutesAngle = (currentTime.Minute + currentTime.Second / 60.0) * 6 * Math.PI / 180;
            int minuteHandLength = radius * 3 / 4;
            graphics.DrawLine(Pens.Black, centerX, centerY, centerX + (int)(minuteHandLength * Math.Sin(minutesAngle)), centerY - (int)(minuteHandLength * Math.Cos(minutesAngle)));

            // Рисуем секундную стрелку
            double secondsAngle = currentTime.Second * 6 * Math.PI / 180;
            int secondHandLength = radius * 4 / 5;
            graphics.DrawLine(Pens.Red, centerX, centerY, centerX + (int)(secondHandLength * Math.Sin(secondsAngle)), centerY - (int)(secondHandLength * Math.Cos(secondsAngle)));
        }
    }
}
