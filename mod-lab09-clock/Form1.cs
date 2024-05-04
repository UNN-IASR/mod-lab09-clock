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

            g.DrawEllipse(new Pen(Color.White, 3f), -135, -135, 270, 270);

            g.FillEllipse(bBrush, -133, -133, 266, 266);

            gs = g.Save();
            g.RotateTransform(6 * (dateTime.Minute + (float)dateTime.Second / 60)+40);
            g.DrawLine(new Pen(Color.White, 2f), -1, 0, -50, -50);
            g.DrawLine(new Pen(Color.White, 2f), 1, 0, -50, -50);
            g.DrawLine(new Pen(Color.White, 2f), 0, 0, -50, -50);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * (dateTime.Hour + (float)dateTime.Minute / 60 + (float)dateTime.Second/3600)+45);
            g.DrawLine(new Pen(Color.White, 3f), -1, 0, -25, -25);
            g.DrawLine(new Pen(Color.White, 3f), 1, 0, -25, -25);
            g.DrawLine(new Pen(Color.White, 3f), 0, 0, -25, -25);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dateTime.Second+46);
            g.DrawLine(new Pen(Color.Blue, 2f), 15, 15, -60, -60);
            g.Restore(gs);

            gs = g.Save();
            g.DrawEllipse(new Pen(Color.Blue, 4f), -2, -2, 4, 4);
            g.Restore(gs);

            for (int i = 0; i < 60; ++i)
            {
                gs = g.Save();
                g.RotateTransform(6 * i + 3);
                g.DrawLine(new Pen(Color.White, 2f), -95, -95, -85, -85);
                g.Restore(gs);
            }

            for (int i = 0; i < 4; ++i)
            {
                gs = g.Save();
                g.RotateTransform(90 * i + 45);
                g.DrawLine(new Pen(Color.White, 2f), -95, -95, -80, -80);
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
