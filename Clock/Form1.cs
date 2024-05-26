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
        private Timer timer;
        public Form1()
        {
            this.Size = new Size(400, 400);
            this.DoubleBuffered = true;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawClock(e.Graphics);
        }
        private void DrawClock(Graphics g)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            Point center = new Point(width / 2, height / 2);
            int radius = Math.Min(width, height) / 2 - 10;
            g.DrawEllipse(Pens.Black, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            for (int i = 1; i <= 12; i++)
            {
                DrawHourTick(g, center, radius, i);
                DrawHourNumber(g, center, radius, i, i.ToString());
            }
            DateTime now = DateTime.Now;
            float hour = now.Hour % 12 + now.Minute / 60f;
            float minute = now.Minute + now.Second / 60f;
            float second = now.Second;
            DrawHand(g, center, hour * 30, radius * 0.5f, 6, Brushes.Black);
            DrawHand(g, center, minute * 6, radius * 0.75f, 4, Brushes.Black);
            DrawHand(g, center, second * 6, radius * 0.9f, 2, Brushes.Red);
        }
        private void DrawHourNumber(Graphics g, Point center, int radius, int hour, string number)
        {
            double angle = (hour % 12) * 30 * Math.PI / 180;
            int x = center.X + (int)(radius * 0.7 * Math.Sin(angle));
            int y = center.Y - (int)(radius * 0.7 * Math.Cos(angle));
            SizeF size = g.MeasureString(number, this.Font);
            x -= (int)(size.Width / 2);
            y -= (int)(size.Height / 2);
            g.DrawString(number, this.Font, Brushes.Black, new PointF(x, y));
        }
        private void DrawHourTick(Graphics g, Point center, int radius, int hour)
        {
            double angle = (hour % 12) * 30 * Math.PI / 180;
            int X = center.X + (int)(radius * 0.8 * Math.Sin(angle));
            int Y = center.Y - (int)(radius * 0.8 * Math.Cos(angle));
            int X2 = center.X + (int)(radius * 0.9 * Math.Sin(angle));
            int Y2 = center.Y - (int)(radius * 0.9 * Math.Cos(angle));
            using (Pen pen = new Pen(Brushes.Black, 3))
            {
                g.DrawLine(pen, X, Y, X2, Y2);
            }
        }
        private void DrawHand(Graphics g, Point center, float angle, float length, int width, Brush brush)
        {
            double radian = angle * Math.PI / 180;
            Point end = new Point(center.X + (int)(length * Math.Sin(radian)), center.Y - (int)(length * Math.Cos(radian)));
            using (Pen pen = new Pen(brush, width))
            {
                g.DrawLine(pen, center, end);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
