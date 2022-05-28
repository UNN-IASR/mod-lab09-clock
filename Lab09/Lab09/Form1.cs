using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Lab09
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBoxCLOCK_Paint(object sender, PaintEventArgs e)
        {
            pictureBoxCLOCK.BackColor = Color.FromArgb(15, 0, 76, 154);
            Graphics graphics = e.Graphics;
            GraphicsState gs;

            int width = this.Width;
            int height = this.Height;

            DateTime dateTime = DateTime.Now;
            graphics.TranslateTransform(width / 2, height / 2);

            graphics.RotateTransform(225);

            gs = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);

            graphics.DrawLine(new Pen(Color.Black, 1), 0, 0, 50, 50);
            graphics.FillEllipse(new SolidBrush(Color.Red), 50, 50, 8, 8);

            graphics.Restore(gs);

            gs = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Minute + dateTime.Second / 10);

            PointF[] MinutePts =
            {
                new PointF(8.0F, 0.0F),
                new PointF(55.0F, 55.0F),
                new PointF(-8.0F, 0.0F),
                new PointF(5.0F, 5.0F)
            };

            graphics.FillPolygon(new SolidBrush(Color.Black), MinutePts);
            graphics.Restore(gs);

            gs = graphics.Save();
            graphics.RotateTransform(30 * dateTime.Hour + (float)dateTime.Minute / 2);

            PointF[] HourPts =
            {
                new PointF(5.0F, -2.0F),
                new PointF(50.0F, 50.0F),
                new PointF(-5.0F, 2.0F),
                new PointF(7F, 7F)
            };

            graphics.FillPolygon(new SolidBrush(Color.Black), HourPts);
            graphics.Restore(gs);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            GraphicsState graphicsState;
            int width = this.Width;
            int height = this.Height;

            graphics.TranslateTransform(width / 2, height / 2);
            Pen black_pen = new Pen(Color.Black, 3);
            Pen red_pen = new Pen(Color.Red, 3);

            graphics.FillEllipse(new SolidBrush(Color.White), -105, -105, 210, 210);
            graphics.DrawEllipse(black_pen, -105, -105, 210, 210);

            for (int i = 0; i < 12; i++)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(30 * i + 45);
                graphics.DrawLine(red_pen, -57, -57, -70, -70);
                graphics.Restore(graphicsState);
            }

            for (int i = 0; i < 60; i++)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(6 * i + 45);
                graphics.DrawLine(black_pen, -63, -63, -70, -70);
                graphics.Restore(graphicsState);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
            this.Invalidate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pictureBoxCLOCK.Invalidate();
        }
    }
}
