using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace LAB09
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
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

            Pen penSeconds = new Pen(Color.Black, 1);
            Pen penClock = new Pen(Color.Black, 3);
            Pen penHours = new Pen(Color.Black, 7);
            Pen penMinutes = new Pen(Color.Black, 4);


            graphics.DrawEllipse(penClock, -100, -100, 200, 200);

            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);
            graphics.DrawLine(penSeconds, 0, 0, -65, -65);
            graphics.Restore(graphicsState);

            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Minute + dateTime.Second / 5);
            graphics.DrawLine(penMinutes, 0, 0, -55, -55);
            graphics.Restore(graphicsState);

            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Hour + dateTime.Minute / 5);
            graphics.DrawLine(penHours, 0, 0, -40, -40);
            graphics.Restore(graphicsState);

            for (int i = 0; i < 12; ++i)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(30 * i + 45);
                graphics.DrawLine(penClock, -60, -60, -70, -70);
                graphics.Restore(graphicsState);
            }

            for (int i = 0; i < 36; ++i)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(10 * i);
                graphics.DrawLine(penClock, -67, -67, -70, -70);
                graphics.Restore(graphicsState);
            }

        }
    }
}


