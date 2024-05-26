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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DateTime dt = DateTime.Now;
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Start();
        }

        //функция-обработчки события
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Form form1 = new Form();
            int width = form1.Size.Width;
            int height = form1.Size.Height;

            DateTime dt = DateTime.Now;
            //перья
            Pen circle_pen = new Pen(Color.Black, 5);
            Pen pen = new Pen(Color.Black, 5);
            Brush brush = new SolidBrush(Color.Indigo);

            Graphics g = e.Graphics;

            GraphicsState gs;

            //смещение системы координат в центр циферблата
            g.TranslateTransform(width / 2, height / 2);
            g.ScaleTransform(width / 200, height / 200);

            //рисуем циферблат
            g.DrawEllipse(circle_pen, -120, -120, 240, 240);

            string[] str = new string[] { "12", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
            int j = 0;
            FontFamily fontFamily = new FontFamily("Algerian");
            Font font = new Font( fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                {
                    g.DrawLine(pen, 0, -105, 0, -120);
                    g.DrawString(str[j], font, brush, -10, -100);
                    j++;
                }
                else
                {
                    g.DrawLine(pen, 0, -112, 0, -120);

                }
                g.RotateTransform(6);
            }

            gs = g.Save();
            int hourAngle = (int)(30 * (dt.Hour % 12 + (float)dt.Minute / 60));
            g.RotateTransform(hourAngle);
            g.DrawLine(new Pen(new SolidBrush(Color.DarkCyan), 4), 0, 0, 0, -70);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.DarkMagenta), 3), 0, 0, 0, -100);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.DarkTurquoise), 2), 0, 0, 0, -100);
            g.Restore(gs);



        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
