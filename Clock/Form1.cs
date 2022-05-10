using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
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
            Graphics graphics = e.Graphics;
            GraphicsState graphicsState;

            int width = this.Width;
            int height = this.Height;

            DateTime dateTime = DateTime.Now;
            graphics.TranslateTransform(width / 2, height / 2);

            Pen penGreen = new Pen(Color.LightGreen, 1);
            Pen penWhite = new Pen(Color.White, 3);
            Pen penYellow = new Pen(Color.Yellow, 5);
            Pen penRed = new Pen(Color.Red, 3);


            graphics.DrawEllipse(penWhite, -100, -100, 200, 200);

            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);
            graphics.DrawLine(penGreen, -3, 0, -65, -65);
            graphics.DrawLine(penGreen, 3, 0, -65, -65);
            graphics.Restore(graphicsState);

            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Minute + dateTime.Second / 5);
            graphics.DrawLine(penRed, 0, 0, -55, -55);
            graphics.Restore(graphicsState);

            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Hour + dateTime.Minute / 5);
            graphics.DrawLine(penYellow, 0, 0, -43, -43);
            graphics.DrawLine(penYellow, -40, -40, -40, -30);
            graphics.DrawLine(penYellow, -40, -40, -30, -40);
            graphics.Restore(graphicsState);

            for (int i = 0; i < 12; ++i)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(30 * i + 45);
                graphics.DrawLine(penWhite, -50, -50, -70, -70);
                graphics.Restore(graphicsState);
            }

            for (int i = 0; i < 60; ++i)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(6 * i);
                graphics.DrawLine(penWhite, -63, -63, -70, -70);
                graphics.Restore(graphicsState);
            }

        }
    }
}