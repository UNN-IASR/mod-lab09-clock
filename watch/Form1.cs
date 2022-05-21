using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Watch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int Width = 700;
        int Height = 300;
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Paint += Form1_Paint;
            DoubleBuffered = true;
            Width = 700;
            Height = 300;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            Pen cir_pen = new Pen(Color.Blue, 2);
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics g = e.Graphics;
            GraphicsState gs;
            g.TranslateTransform(Width / 2, Height / 2);
            g.ScaleTransform(300 / 200, 300 / 200);
            g.DrawEllipse(cir_pen, -Height / 2, -Height / 2, Height, Height);
            gs = g.Save();
            for (int i = 0; i < 12; i++)
            {
                g.RotateTransform(30 * i);
                g.DrawLine(new Pen(new SolidBrush(Color.Blue), 6), 0, -Height / 2, 0, -Width / 7);
            }
            g.RotateTransform(-120);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 6), 0, -Height / 2, 0, -Width / 7);
            g.RotateTransform(90);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 6), 0, -Height / 2, 0, -Width / 7);
            g.RotateTransform(90);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 6), 0, -Height / 2, 0, -Width / 7);
            g.RotateTransform(90);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 6), 0, -Height / 2, 0, -Width / 7);
            for (int i = 0; i < 60; i++)
            {
                g.RotateTransform(6 * i);
                g.DrawLine(new Pen(new SolidBrush(Color.Blue), 3), 0, -Height / 2, 0, -Height / 2 + 10);
                g.Restore(gs);
                gs = g.Save();
            }
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 3), 0, 0, 0, -75);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(6 * (float)dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Gold), 2), 0, 0, 0, -100);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour + (float)dt.Minute / 60 + (float)dt.Second / 3600));
            g.DrawLine(new Pen(new SolidBrush(Color.Green), 4), 0, 0, 0, -50);
            g.Restore(gs);
            gs = g.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
