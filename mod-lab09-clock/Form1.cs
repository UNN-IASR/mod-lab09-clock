using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace mod_lab09_clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            int minSize = Math.Min(ClientSize.Width, ClientSize.Height);
            int radius = minSize / 2 - 20;

            Pen cir_pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics g = e.Graphics;
            GraphicsState gs;

            g.TranslateTransform(minSize / 2, minSize / 2);
            g.ScaleTransform(radius / 120f, radius / 120f);

            g.DrawEllipse(cir_pen, -radius, -radius, 2 * radius, 2 * radius);

            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour % 12 + (float)dt.Minute / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 0.02f * radius), 0, 0, 0, -0.4f * radius);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Green), 0.02f * radius), 0, 0, 0, -0.6f * radius);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 0.015f * radius), 0, 0, 0, -0.8f * radius);
            g.Restore(gs);

            for (int i = 1; i <= 12; i++)
            {
                double angle = -Math.PI / 2 + 2 * Math.PI / 12 * i;
                float x = (float)(Math.Cos(angle) * 0.9f * radius);
                float y = (float)(Math.Sin(angle) * 0.9f * radius);
                g.DrawString(i.ToString(), Font, Brushes.Black, x - 5, y - 5);
            }
            for (int i = 1; i < 60; i++)
            {
                if (i % 5 == 0) continue;
                double angle = -Math.PI / 2 + 2 * Math.PI / 60 * i;
                float x = (float)(Math.Cos(angle) * 0.95f * radius);
                float y = (float)(Math.Sin(angle) * 0.95f * radius);
                g.FillEllipse(Brushes.Black, x - 2, y - 2, 4, 4);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
