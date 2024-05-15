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

namespace clock
{
    public partial class Form1 : Form
    {
        private Timer timer;
        public Form1()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Interval = 1000;//1 sekunda
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();

        }
        
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            //делаем фон прозрачный, чтобы только картинка была видна
            this.BackColor = Color.AliceBlue;
            //this.TransparencyKey = this.BackColor;

            DateTime dt = DateTime.Now;

            Pen cir_pen = new Pen(Color.Blue, 10);
            Pen pen = new Pen(Color.Black, 2);
            Pen pen1 = new Pen(Color.DarkGray, 1);
            Brush brush = new SolidBrush(Color.Indigo);

            Graphics g = e.Graphics;

            GraphicsState gs;

            g.TranslateTransform(this.Width / 2, this.Height / 2);
            float zoom = 2f;
            g.ScaleTransform(zoom, zoom);

            
            g.DrawEllipse(cir_pen, -120, -120, 240, 240);


            //рисуем стрелки 
            /*
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Brown), 3), 0, 0, 0, -80);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Pink), 2), 0, 0, 0, -100);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * dt.Hour % 360 + (float)(dt.Minute) / 2);
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 3), 0, 0, 0, -60);
            g.Restore(gs);*/


            // Создание массива точек для формирования стрелки
            Point[] hourPoints = { new Point(0, 0), new Point(20, -20), new Point(0, -65) };
            Point[] minutePoints = { new Point(0, 0), new Point(5, -8), new Point(0, -80)};
            Point[] secondPoints = { new Point(0, 0), new Point(20, -50), new Point(10, -50), new Point(0, -90) };

            // Рисование часовой стрелки
            gs = g.Save();
            g.RotateTransform(30 * dt.Hour + dt.Minute / 2);
            g.DrawPolygon(new Pen(new SolidBrush(Color.Red), 3), hourPoints);
            g.Restore(gs);

            // Рисование минутной стрелки
            gs = g.Save();
            g.RotateTransform(6 * dt.Minute + (float)dt.Second / 10);
            g.DrawPolygon(new Pen(new SolidBrush(Color.Orange), 2), minutePoints);
            g.Restore(gs);

            // Рисование секундной стрелки
            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawPolygon(new Pen(new SolidBrush(Color.Pink), 2), secondPoints);
            g.Restore(gs);


            // деления часов
            for (int i=0; i < 60; i++)
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
            string[] digits = { "12", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
            for (int i = 0; i < 12; i++)
            {
                /*gs = g.Save();
                //g.TranslateTransform(0, -100); // Move to the center of the hour markers
                g.RotateTransform(i * 30); // Rotate around the center, starting from 12 o'clock
                g.TranslateTransform(-5, -5); // Move to the top-left corner of the clock
                g.DrawString(digits[i], this.Font, brush, new PointF(0, -90));
                g.Restore(gs);
                double angle = i * angleStep * Math.PI / 180;
                int x = (int)(centerX + radius * Math.Cos(angle))- 5; // вычисляем координаты точки на окружности
                int y = (int)(centerY + radius * Math.Sin(angle)) - 5;

                g.DrawString(digits[i], this.Font, brush, new PointF(x, y)); // рисуем цифру в найденной точке*/
                double a = (i - 3) * step * Math.PI / 180; //значение угла
                int x = (int)(centerX + radius * Math.Cos(a)) - 5; //координаты точки на окружности
                int y = (int)(centerY + radius * Math.Sin(a)) - 5;

                g.DrawString(digits[i], this.Font, brush, new PointF(x, y)); // цифра в найденной точке
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
