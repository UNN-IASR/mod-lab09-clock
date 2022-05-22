using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Clocks
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
            this.Invalidate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            GraphicsState graphicsState;
            int width = this.Width;
            int height = this.Height;
            graphics.TranslateTransform(width / 2, height / 2);
            Pen brownPen = new Pen(Color.DeepPink, 3);
            graphics.DrawEllipse(brownPen, -100, -100, 200, 200);
            graphics.FillEllipse(new SolidBrush(Color.AliceBlue), -98, -98, 196, 196);
            for (int i = 0; i < 12; i++)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(30 * i + 45);
                graphics.DrawLine(brownPen, -57, -57, -70, -70);
                graphics.Restore(graphicsState);
            }

            for (int i = 0; i < 60; i++)
            {
                graphicsState = graphics.Save();
                graphics.RotateTransform(6 * i + 45);
                graphics.DrawLine(brownPen, -63, -63, -70, -70);
                graphics.Restore(graphicsState);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb(0, 0, 0, 0);
            Graphics graphics = e.Graphics;
            GraphicsState graphicsState;
            int width = this.Width;
            int height = this.Height;
            DateTime dateTime = DateTime.Now;
            graphics.TranslateTransform(width / 2, height / 2);
            graphics.RotateTransform(-135);
            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);
            graphics.DrawLine(new Pen(Color.Red, 1), 0, 0, 65, 65);
            graphics.Restore(graphicsState);
            graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Minute + dateTime.Second / 10);
            PointF[] pointsForMinute =
                {
                    new PointF(4.0F, 0.0F),
                    new PointF(60.0F, 60.0F),
                    new PointF(-4.0F, 0.0F)
                };
            graphics.FillPolygon(new SolidBrush(Color.Aquamarine), pointsForMinute);
            graphics.Restore(graphicsState);

            graphicsState = graphics.Save();
            graphics.RotateTransform(30 * dateTime.Hour + (float)dateTime.Minute / 2);
            PointF[] pointsForHour =
                {
                    new PointF(5.0F, -2.0F),
                    new PointF(50.0F, 50.0F),
                    new PointF(-5.0F, -2.0F)
                };
            graphics.FillPolygon(new SolidBrush(Color.Aqua), pointsForHour);
            graphics.Restore(graphicsState);
        }
    }
}