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
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            GraphicsState gs;

            int w = Width;
            int h = Height;

            DateTime dt = DateTime.Now;

            g.TranslateTransform(w / 2, h / 2);

            Pen clock_pen = new Pen(Color.White, 3);
            Pen p1 = new Pen(Color.Red, 4);
            Pen p2 = new Pen(Color.Orange, 4);
            Pen p3 = new Pen(Color.OrangeRed, 4);
            Brush b1 = new SolidBrush(Color.OrangeRed);
            Brush b2 = new SolidBrush(Color.Red);

            g.DrawEllipse(clock_pen, -120, -120, 240, 240);


            for (int i = 0; i < 12; i++)
            {
                gs = g.Save();
                g.RotateTransform(30 * i + 45);
                g.DrawLine(clock_pen, -70, -70, -85, -85);
                g.Restore(gs);
            }

            for (int i = 0; i < 60; i++)
            {
                gs = g.Save();
                g.RotateTransform(6 * i + 45);
                g.DrawLine(clock_pen, -80, -80, -85, -85);
                g.Restore(gs);
            }

            gs = g.Save();
            g.RotateTransform(6 * (dt.Hour + (float)dt.Minute / 60));
            g.DrawLine(p3, 0, 0, 0, -40);
            Point point1 = new Point(10, -40);
            Point point2 = new Point(-10, -40);
            Point point3 = new Point(0, -60);
            Point[] points1 = { point1, point2, point3 };
            g.FillPolygon(b1, points1);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(p2, 0, 0, 0, -110);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(p1, 0, 0, 0, -50);
            Point point4 = new Point(5, -50);
            Point point5 = new Point(-5, -50);
            Point point6 = new Point(0, -80);
            PointF[] points2 = { point4, point5, point6 };
            g.FillPolygon(b2, points2);
            g.Restore(gs);
        }
    }
}
