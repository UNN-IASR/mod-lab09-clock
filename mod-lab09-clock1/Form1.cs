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

namespace mod_lab09_clock1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            Graphics g = e.Graphics;
            GraphicsState gs;

            int width = this.Width;
            int height = this.Height;

            g.TranslateTransform(width / 2, height / 2);

            SolidBrush wBrush = new SolidBrush(Color.White);
            SolidBrush bBrush = new SolidBrush(Color.Black);

            g.DrawEllipse(new Pen(Color.Black, 2f), -110, -110, 220, 220);
            g.DrawEllipse(new Pen(Color.Black, 2f), -135, -135, 270, 270);

            g.FillEllipse(wBrush, -133, -133, 266, 266);
            g.FillEllipse(bBrush, -110, -110, 220, 220);
            g.FillEllipse(wBrush, -100, -100, 200, 200);

            gs = g.Save();
            g.RotateTransform(6 * (dateTime.Minute + (float)dateTime.Second / 60)+40);
            g.DrawLine(new Pen(Color.Black, 2f), 3, 0, -45, -45);
            g.DrawLine(new Pen(Color.Black, 2f), -3, 0, -45, -45);
            g.DrawLine(new Pen(Color.Black, 2f), 0, 0, -45, -45);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * (dateTime.Hour + (float)dateTime.Minute / 60 + (float)dateTime.Second/3600)+45);
            g.DrawLine(new Pen(Color.Black, 3f), 4, 0, -25, -25);
            g.DrawLine(new Pen(Color.Black, 3f), -4, 0, -25, -25);
            g.DrawLine(new Pen(Color.Black, 3f), 0, 0, -25, -25);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dateTime.Second+46);
            g.DrawLine(new Pen(Color.Red, 2f), 15, 15, -60, -60);
            g.Restore(gs);

            gs = g.Save();
            g.DrawEllipse(new Pen(Color.Red, 4f), -2, -2, 5, 5);
            g.Restore(gs);

            for (int i = 0; i < 60; ++i)
            {
                gs = g.Save();
                g.RotateTransform(6 * i + 3);
                g.DrawLine(new Pen(Color.Black, 2f), -65, -65, -68, -68);
                g.Restore(gs);
            }

            for (int i = 0; i < 12; ++i)
            {
                gs = g.Save();
                g.RotateTransform(30 * i + 45);
                g.DrawEllipse(new Pen(Color.Black, 4f), -66, -66, -4, -4);
                g.Restore(gs);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
