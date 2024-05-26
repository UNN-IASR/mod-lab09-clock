using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer1;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Включаем двойную буферизацию для предотвращения мерцания
            this.Paint += new PaintEventHandler(this.Form1_Paint);
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 1000; // Устанавливаем интервал таймера на 1 секунду
            timer1.Tick += new EventHandler(this.timer1_Tick);
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now; // Получаем текущее время

            Pen cirPen = new Pen(Color.Black, 2); // Создаем перо для рисования окружности
            Brush brush = new SolidBrush(Color.Indigo); // Создаем кисть для рисования цифр

            Graphics g = e.Graphics;
            GraphicsState gs;

            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            // Перемещаем начало координат в центр и масштабируем рисунок
            g.TranslateTransform(w / 2, h / 2);
            g.ScaleTransform(w / 200, h / 200);

            // Рисуем циферблат
            g.DrawEllipse(cirPen, -120, -120, 240, 240);

            // Рисуем цифры на циферблате
            for (int i = 0; i < 12; i++)
            {
                int angle = i * 30;
                double rad = angle * Math.PI / 180;
                int x = (int)(100 * Math.Sin(rad));
                int y = (int)(-100 * Math.Cos(rad));
                g.DrawString(i == 0 ? "12" : i.ToString(), this.Font, brush, new PointF(x - 10, y - 10));
            }

            // Рисуем секундную стрелку
            gs = g.Save();
            g.RotateTransform(6 * (dt.Second + dt.Millisecond / 1000.0f));
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 2), 0, 0, 0, -90);
            g.Restore(gs);

            // Рисуем минутную стрелку
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + dt.Second / 60.0f));
            g.DrawLine(new Pen(new SolidBrush(Color.Brown), 4), 0, 0, 0, -70);
            g.Restore(gs);

            // Рисуем часовую стрелку
            gs = g.Save();
            g.RotateTransform(30 * (dt.Hour % 12 + dt.Minute / 60.0f));
            g.DrawLine(new Pen(new SolidBrush(Color.Green), 6), 0, 0, 0, -50);
            g.Restore(gs);
        }

        // Обработчик события таймера, вызывающий перерисовку формы
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
