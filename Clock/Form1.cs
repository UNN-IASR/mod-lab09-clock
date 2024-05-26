using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        int R = 100;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            int w = this.Width;
            int h = this.Height;
            Pen cir_pen = new Pen(Color.Black, 2);
            Pen min_pen = new Pen(Color.Red, 2);
            min_pen.EndCap = LineCap.ArrowAnchor;
            Pen sec_pen = new Pen(Color.Black, 1);
            sec_pen.EndCap = LineCap.ArrowAnchor;
            Pen ch_pen = new Pen(Color.Green, 3);
            ch_pen.EndCap = LineCap.ArrowAnchor;
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics g = e.Graphics;
            GraphicsState gs;

            g.TranslateTransform(w / 2, h / 2);
            g.ScaleTransform(w / 400, h / 400);

            Lines(g, cir_pen);
            Numbers(g);
            g.DrawEllipse(cir_pen, -R, -R, 2 * R, 2 * R);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(min_pen, 0, 0, 0, -85);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(sec_pen, 0, h / 10, 0, (float)(-h / 4.5));
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour % 12) + dt.Minute / 2 + dt.Second / 120);
            g.DrawLine(ch_pen, 0, 0, 0, (float)(-h / 6));
            g.Restore(gs);
        }
        void Lines(Graphics g, Pen p)
        {
            Point center = new Point(0, 0);
            int clockRadius = R;
            Point end;
            Point start;
            int angle = 0, length;
            while (angle < 360)
            {
                if (angle % 30 == 0)
                    length = 15;
                else
                    length = 10;
                double radians = angle * Math.PI / 180;

                int xStart = center.X + (int)((clockRadius - length) * Math.Cos(radians));
                int yStart = center.Y + (int)((clockRadius - length) * Math.Sin(radians));
                start = new Point(xStart, yStart);

                int xEnd = center.X + (int)(clockRadius * Math.Cos(radians));
                int yEnd = center.Y + (int)(clockRadius * Math.Sin(radians));
                end = new Point(xEnd, yEnd);

                g.DrawLine(p, start, end);
                angle += 6;
            }
        }

        private void Numbers(Graphics graphics)
        {
            Point center = new Point(0, 0);
            Font font = new Font("Times New Roman", 15, FontStyle.Bold);
            for (int hour = 1; hour <= 12; hour++)
            {
                float angle = hour * 30;
                float x = center.X + (float)((R - 15) * 0.9 * Math.Cos((angle - 90) * Math.PI / 180)) - 9;
                float y = center.Y + (float)((R - 15) * 0.9 * Math.Sin((angle - 90) * Math.PI / 180)) - 12;
                graphics.DrawString(hour.ToString(), font, Brushes.Black, x, y);
            }
        }
    }
}
