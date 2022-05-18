using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Clock
{
    public partial class Clock : Form
    {
        public Clock()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            GraphicsState grSt;

            int width = this.Width;
            int height = this.Height;

            DateTime dateTime = DateTime.Now;
            gr.TranslateTransform(width / 2, height / 2);

            Pen penGreen = new Pen(Color.Blue, 1);
            Pen penWhite = new Pen(Color.Green, 3);
            Pen penYellow = new Pen(Color.HotPink, 5);
            Pen penRed = new Pen(Color.Red, 3);


            gr.DrawEllipse(penWhite, -100, -100, 200, 200);

            grSt = gr.Save();
            gr.RotateTransform(6 * dateTime.Second);
            gr.DrawLine(penGreen, -3, 0, -65, -65);
            gr.DrawLine(penGreen, 3, 0, -65, -65);
            gr.Restore(grSt);

            grSt = gr.Save();
            gr.RotateTransform(6 * dateTime.Minute + dateTime.Second / 5);
            gr.DrawLine(penRed, 0, 0, -55, -55);
            gr.Restore(grSt);

            grSt = gr.Save();
            gr.RotateTransform(6 * dateTime.Hour + dateTime.Minute / 5);
            gr.DrawLine(penYellow, 0, 0, -43, -43);
            gr.DrawLine(penYellow, -40, -40, -40, -30);
            gr.DrawLine(penYellow, -40, -40, -30, -40);
            gr.Restore(grSt);

            for (int i = 0; i < 12; ++i)
            {
                grSt = gr.Save();
                gr.RotateTransform(30 * i + 45);
                gr.DrawLine(penWhite, -50, -50, -70, -70);
                gr.Restore(grSt);
            }

            for (int i = 0; i < 60; ++i)
            {
                grSt = gr.Save();
                gr.RotateTransform(6 * i);
                gr.DrawLine(penWhite, -63, -63, -70, -70);
                gr.Restore(grSt);
            }

        }
    }
}