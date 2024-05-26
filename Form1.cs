using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;//1 sekunda
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            Random rnd = new Random();
            this.BackColor = Color.White;

            Pen cir_pen = new Pen(Color.Black, 5);
            Pen pen = new Pen(Color.Black, 2);
            Pen pen1 = new Pen(Color.Brown, 1);
            Brush brush = new SolidBrush(Color.Indigo);

            Graphics g = e.Graphics;

            GraphicsState gs;

            g.TranslateTransform(this.Width / 2, this.Height / 2);
            float zoom = 2f;
            g.ScaleTransform(zoom, zoom);

            g.DrawEllipse(cir_pen, -120, -120, 240, 240); 
            
            // Рисование часовой стрелки
            gs = g.Save();
            g.RotateTransform(30 * dt.Hour + dt.Minute / 2);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, 0, 0, -25);
            g.Restore(gs);

            // Рисование минутной стрелки
            gs = g.Save();
            g.RotateTransform(6 * dt.Minute + (float)dt.Second / 10);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 2), 0, 0, 0, -50);
            g.Restore(gs);

            // Рисование секундной стрелки
            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 1), 0, 0, 0, -70);
            g.Restore(gs);

            for (int i = 0; i < 60; i++)
            {
                gs = g.Save();
                g.RotateTransform(i * 6);
                g.DrawLine(pen1, 0, -100, 0, -110);
                g.Restore(gs);
            }
            for (int i = 0; i < 12; i++)
            {
                gs = g.Save();
                g.RotateTransform(i * 30);
                g.DrawLine(pen, 0, -100, 0, -110);
                g.Restore(gs);
            }
            // Рисование цифр часов
            int radius = 85; // радиус окружности, на которой будут расположены цифры
            int centerX = 0; // координаты центра окружности
            int centerY = 0;
            int step = 30; // шаг угла между цифрами
            string[] numbers = { "12", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
            for (int i = 0; i < 12; i++)
            {
                double a = (i - 3) * step * Math.PI / 180; //значение угла
                int x = (int)(centerX + radius * Math.Cos(a)) - 5; //координаты точки на окружности
                int y = (int)(centerY + radius * Math.Sin(a)) - 5;

                g.DrawString(numbers[i], this.Font, brush, new PointF(x, y)); // цифра в найденной точке
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
