using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices; // Подключаем новое пространство имён
using System.Drawing;
using System.Drawing.Drawing2D;


namespace lab9
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
            int w = form1.Size.Width;
            int h = form1.Size.Height;

            DateTime dt = DateTime.Now;
            //перья
            Pen cir_pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Indigo);

            Graphics g = e.Graphics;

            GraphicsState gs;

            //смещение системы координат в центр циферблата
            g.TranslateTransform(w / 2, h / 2);
            g.ScaleTransform(w / 200, h / 200);

            //рисуем циферблат
            g.DrawEllipse(cir_pen, -120, -120, 240, 240);

            string[] str = new string[] { "12", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
            int j = 0;
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               16,
               FontStyle.Regular,
               GraphicsUnit.Pixel);
            for (int i = 0; i < 60; i++)
            {
                if (i%5 == 0)
                {
                    g.DrawLine(cir_pen, 0, -105, 0, -120);
                    g.DrawString(str[j], font, brush, -10, -100);
                    j++;
                }
                else
                {
                    g.DrawLine(cir_pen, 0, -115, 0, -120);
                   
                }
                g.RotateTransform(6);
            }

            gs = g.Save();
            Point[] pointsSec = new Point[] { new Point(0, 0), new Point(10, -20), new Point(-10, -50), new Point(0, -70) };
            Point[] pointsMin = new Point[] { new Point(0, 0), new Point(10, -10), new Point(-10, -30),  new Point(0, -65) };
            g.RotateTransform(6 * (dt.Hour + (float)(dt.Minute + (float)dt.Second / 60) / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), 0, 0, 0, -50);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.DarkGoldenrod), 1), 0, 0, 0, -60);
            g.DrawPolygon(new Pen(new SolidBrush(Color.DarkGoldenrod), 1), pointsMin);
            //Brush brush1 = new SolidBrush(Color.DarkGoldenrod);
           // g.FillClosedCurve(brush1, pointsMin);

            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (float)dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.DarkCyan), 1), 0, 0, 0, -70);
            g.DrawCurve(new Pen(new SolidBrush(Color.DarkCyan), 1), pointsSec);
            Brush brush1 = new SolidBrush(Color.DarkCyan);
            g.FillClosedCurve(brush1, pointsSec);
            g.Restore(gs);



        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
