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
namespace MIPIS_clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int k1 = 700;
        int k2 = 240;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            DoubleBuffered = true;
            Width = k1;
            Height = k1;
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            Pen cir_pen = new Pen(Color.Black, 10);
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics g = e.Graphics;
            GraphicsState gs;
            g.TranslateTransform(Width / 2, Height / 2);
            g.ScaleTransform(500 / 200, 500 / 200);
            g.DrawEllipse(cir_pen, -k2 / 2, -k2 / 2, k2, k2);
            gs = g.Save();
            for (int i = 0; i < 12; i++)
            {
                g.RotateTransform(30 * i);
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 6), 0, -k2 / 2, 0, -k1 / 7);
            }
            g.RotateTransform(-120);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 6), 0, -k2 / 2, 0, -k1 / 7);
            g.RotateTransform(90);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 6), 0, -k2 / 2, 0, -k1 / 7);
            g.RotateTransform(90);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 6), 0, -k2 / 2, 0, -k1 / 7);
            g.RotateTransform(90);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 6), 0, -k2 / 2, 0, -k1 / 7);
            for (int i = 0; i < 60; i++)
            {
                g.RotateTransform(6 * i);
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, -k2 / 2, 0, -k2 / 2 + 10);
                g.Restore(gs);
                gs = g.Save();
            }
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.GreenYellow), 3), 0, 0, 0, -75);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(6 * (float)dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.GreenYellow), 2), 0, 0, 0, -100);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour + (float)dt.Minute / 60 + (float)dt.Second / 3600));
            g.DrawLine(new Pen(new SolidBrush(Color.GreenYellow), 4), 0, 0, 0, -50);
            g.Restore(gs);
            gs = g.Save();
        }
    }
}