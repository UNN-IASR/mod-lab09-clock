using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace mipis
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

            Graphics graphics = e.Graphics;
            GraphicsState gState;

            int width = this.Width;
            int height = this.Height;

            graphics.TranslateTransform(width / 2, height / 2);

            Pen whitePen = new Pen(Color.White, 2);
            Pen purplePen = new Pen(Color.MediumPurple, 2);
            Pen bluePen = new Pen(Color.BlueViolet, 2);
            Pen pinkPen = new Pen(Color.DeepPink, 2);

            SolidBrush myBrush = new SolidBrush(Color.Black);

            graphics.FillEllipse(myBrush, -100, -100, 200, 200);

            graphics.DrawEllipse(whitePen, -100, -100, 200, 200);
            graphics.DrawEllipse(whitePen, -105, -105, 210, 210);

            gState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);
            graphics.DrawLine(purplePen, 3, 0, -60, -60);
            graphics.DrawLine(purplePen, -3, 0, -60, -60);
            graphics.DrawLine(purplePen, 0, 0, -60, -60);
            graphics.Restore(gState);

            gState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Minute + (float)dateTime.Second / 10);
            graphics.DrawLine(pinkPen, 3, 0, -45, -45);
            graphics.DrawLine(pinkPen, -3, 0, -45, -45);
            graphics.DrawLine(pinkPen, 0, 0, -45, -45);

            graphics.Restore(gState);

            gState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Hour + (float)dateTime.Minute / 10);
            graphics.DrawLine(bluePen, 2, 0, -30, -30);
            graphics.DrawLine(bluePen, -2, 0, -30, -30);
            graphics.DrawLine(bluePen, 0, 0, -30, -30);
            graphics.Restore(gState);

            for (int i = 0; i < 12; ++i)
            {
                gState = graphics.Save();
                graphics.RotateTransform(30 * i + 45);
                graphics.DrawLine(whitePen, -60, -60, -70, -70);
                graphics.Restore(gState);
            }

            for (int i = 0; i < 36; ++i)
            {
                gState = graphics.Save();
                graphics.RotateTransform(10 * i);
                graphics.DrawLine(whitePen, -67, -67, -70, -70);
                graphics.Restore(gState);
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
