using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ClockRound
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(Form1_Paint);
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            this.Size = new Size(500, 500);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;

            Font font = new Font("Arial", 14, FontStyle.Underline);
            Brush numberBrush = new SolidBrush(Color.LimeGreen);

            Graphics g = e.Graphics;
            GraphicsState gs;

            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            g.TranslateTransform(w / 2, h / 2);
            g.ScaleTransform(w / 400f, h / 400f);

            // circle
            g.DrawEllipse(new Pen(Color.Black, 2), -180, -180, 360, 360);

            //hour points
            for (int i = 0; i < 12; i++)
            {
                gs = g.Save();
                g.RotateTransform(30 * i);
                g.DrawLine(new Pen(Color.Black, 3), 0, -170, 0, -180);
                g.Restore(gs);
            }

            // min points
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 != 0)
                {
                    gs = g.Save();
                    g.RotateTransform(6 * i);
                    g.DrawLine(new Pen(Color.Black, 2), 0, -175, 0, -180);
                    g.Restore(gs);
                }
            }

            // Numbers
            for (int i = 1; i <= 12; i++)
            {
                int angle = 30 * i;
                double rad = Math.PI * angle / 180;
                float x = (float)(150 * Math.Sin(rad));
                float y = (float)(-150 * Math.Cos(rad));
                string number = i.ToString();
                SizeF numberSize = g.MeasureString(number, font);
                g.DrawString(number, font, numberBrush, x - numberSize.Width / 2, y - numberSize.Height / 2);
            }

            // hour
            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour % 12 + (float)dt.Minute / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 6), 0, 0, 0, -50);
            g.Restore(gs);

            // min
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.DarkGreen), 4), 0, 0, 0, -80);
            g.Restore(gs);

            // sec
            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.GreenYellow), 2), 0, 0, 0, -90);
            g.Restore(gs);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // Redraw
        }
    }
}
