using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace clock
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
            Pen cir_pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Black);
            Graphics g = e.Graphics;
            GraphicsState gs;
            DateTime dt = DateTime.Now;

            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.ScaleTransform(this.Width / 200, this.Height / 200);
            g.DrawEllipse(cir_pen, -120, -120, 240, 240);
            
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 4), 0, 0, 0, -60);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Green), 4), 0, 0, 0, -80);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour % 12) + (float)dt.Minute / 2);
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 4), 0, 0, 0, -40);
            g.Restore(gs);

            for (int i = 0; i < 60; i++)
            {
                gs = g.Save();

                g.RotateTransform(i * 6);

                if (i % 5 == 0)
                {
                    g.DrawLine(new Pen(brush, 2), -120, 0, -105, 0);
                }
                else
                {
                    g.DrawLine(new Pen(brush, 1), -120, 0, -110, 0);
                }

                g.Restore(gs);
            }

            for (int i = 1; i <= 12; i++) 
            { 
                gs = g.Save();
                double angle = i * Math.PI / 6 + Math.PI / 2;

                float x = (float)(-90 * Math.Cos(angle) - 6);
                float y = (float)(-90 * Math.Sin(angle) - 6);

                g.DrawString(i.ToString(), this.Font, brush, new PointF(x, y));

                g.Restore(gs);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}