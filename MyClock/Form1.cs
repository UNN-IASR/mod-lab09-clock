using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyClock
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;

            Pen pen_cir = new Pen(Color.Black, 2);
            Pen pen_12 = new Pen(Color.MidnightBlue, 5);
            Pen pen_60 = new Pen(Color.Black, 3);
            Brush brush = new SolidBrush(Color.Indigo);

            Graphics g = e.Graphics;
            GraphicsState gs;

            g.TranslateTransform(171, 159);


            g.DrawEllipse(pen_12, 0, 0, 1, 1);
            g.DrawEllipse(pen_cir, -150, -150, 300, 300);
            g.DrawEllipse(pen_cir, -95, -95, 190, 190);
            gs = g.Save();

            g.DrawString(Convert.ToString(3), new Font("Times New Roman", 11F), new SolidBrush(Color.Black), 97, -5);
            g.DrawString(Convert.ToString(9), new Font("Times New Roman", 11F), new SolidBrush(Color.Black), -107, -5);
            g.DrawString(Convert.ToString(6), new Font("Times New Roman", 11F), new SolidBrush(Color.Black), -5, 95);
            g.DrawString(Convert.ToString(12), new Font("Times New Roman", 11F), new SolidBrush(Color.Black), -7, -110);


            for (int i = 0; i <= 60; i++) {
                g.RotateTransform(6 * i);
                g.DrawLine(pen_60, 0, -120, 0, -110);
                g.Restore(gs);
                gs = g.Save();
            }
            
            for (int i = 0; i < 12; i++) {

                g.RotateTransform(30 * i);
                g.DrawLine(pen_12, 0, 80, 0, 95);
                g.Restore(gs);
                gs = g.Save();
            }

            g.RotateTransform(6 * (float)dt.Second);
            Pen pens = new Pen(new SolidBrush(Color.Black), 2);
            g.DrawLine(pens, 0, 0, 0, -100);
            g.Restore(gs);
            gs = g.Save();

            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            Pen penm = new Pen(new SolidBrush(Color.Brown), 3);
            g.DrawLine(penm, 0, 0, 0, -75);
            g.Restore(gs);
            gs = g.Save();

            g.RotateTransform(30 * (dt.Hour + (float)dt.Minute / 60 + (float)dt.Second / 3600));
            Pen penh = new Pen(new SolidBrush(Color.MidnightBlue), 4);
            g.DrawLine(penh, 0, 0, 0, -50);
            g.Restore(gs);
            gs = g.Save();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            
        }
    }
}