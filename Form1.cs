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

namespace clock
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            int w = this.Width;
            int h = this.Height;

            Graphics graphics = e.Graphics;
            GraphicsState gState;

            graphics.TranslateTransform(w / 2, h / 2);

            SolidBrush brush = new SolidBrush(Color.White);
            graphics.FillEllipse(brush, -100, -100, 200, 200);

            Pen grayPen = new Pen(Color.Gray, 2);
            graphics.DrawEllipse(grayPen, -100, -100, 200, 200);

            Pen aquamarinePen = new Pen(Color.Aquamarine, 2);
            Pen turquoisePen = new Pen(Color.Turquoise, 2);
            Pen bluePen = new Pen(Color.SkyBlue, 2);

            gState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);
            graphics.DrawLine(aquamarinePen, 0, 0, -60, -60);
            graphics.Restore(gState);

            gState = graphics.Save();
            graphics.RotateTransform(6 * (dateTime.Minute + (float)dateTime.Second / 60));
            graphics.DrawLine(bluePen, 3, 0, -45, -45);
            graphics.DrawLine(bluePen, -3, 0, -45, -45);
            graphics.DrawLine(bluePen, 0, 0, -45, -45);

            graphics.Restore(gState);

            gState = graphics.Save();
            graphics.RotateTransform(30 * (dateTime.Hour + (float)dateTime.Minute / 60 + (float)dateTime.Second / 3600));
            graphics.DrawLine(turquoisePen, 2, 0, -30, -30);
            graphics.DrawLine(turquoisePen, -2, 0, -30, -30);
            graphics.DrawLine(turquoisePen, 0, 0, -30, -30);
            graphics.Restore(gState);

            for (int i = 0; i < 12; i++)
            {
                gState = graphics.Save();
                graphics.RotateTransform(30 * i + 45);
                graphics.DrawLine(grayPen, -60, -60, -70, -70);
                graphics.Restore(gState);
            }

            for (int i = 0; i < 72; i++)
            {
                gState = graphics.Save();
                graphics.RotateTransform(6 * i + 3);
                graphics.DrawLine(grayPen, -67, -67, -70, -70);
                graphics.Restore(gState);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
