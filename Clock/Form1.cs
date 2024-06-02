using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using static System.Windows.Forms.AxHost;
using System.Threading;

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
            Thread.Sleep(925);
            this.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int w = 800;
            int h = 600;

            DateTime dt = DateTime.Now;

            Pen cir_pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Indigo);

            Graphics g = e.Graphics;

            GraphicsState gs;

            g.TranslateTransform(w / 2, h / 2);
            g.ScaleTransform(w / 400, h / 300);

            g.DrawEllipse(cir_pen, -75, -75, 150, 150);

            for (int i = 0; i < 12; ++i)
            {
                gs = g.Save();
                g.RotateTransform(30 * i + 45);
                g.DrawLine(cir_pen, -40, -40, -52, -52);
                g.Restore(gs);
            }

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 2), 0, 0, 0, -75);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Green), 4), 0, 0, 0, -75);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Hour + (float)dt.Minute / 10);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 4), 0, 0, 0, -35);
            g.Restore(gs);
        }
    }
}
