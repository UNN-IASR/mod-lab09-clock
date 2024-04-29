using System.Drawing.Drawing2D;

namespace CLOCK_9_lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            DateTime dt = DateTime.Now;

            //Для сверки записываем время в название формы
            this.Text = dt.ToString();

            Pen cir_pen_big = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics g = e.Graphics;
            GraphicsState gs;

            g.TranslateTransform(w / 2, h / 2);
            g.ScaleTransform(w / 250, h / 250);

            g.DrawEllipse(cir_pen_big, -120, -120, 240, 240);

            //Рисуем штрихи для минут и часов
            for (int i = 0; i < 60; i++)
            {
                gs = g.Save();
                g.RotateTransform(i * 6);
                if (i % 5 == 0)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.Pink), 2), -119, 0, -100, 0);
                }
                else
                {
                    g.DrawLine(new Pen(brush, 1), -119, 0, -110, 0);
                }
                g.Restore(gs);
            }

            //Циферблат
            for (int i = 1; i < 13; i++)
            {
                gs = g.Save();
                double x = -90 * Math.Cos(30 * i * Math.PI / 180 + 30 * 3 * Math.PI / 180);
                double y = -90 * Math.Sin(30 * i * Math.PI / 180 + 30 * 3 * Math.PI / 180);
                g.DrawString(i.ToString(), new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.BlueViolet), (float)x - 10, (float)y - 10);
                g.Restore(gs);
            }

            //Часы
            gs = g.Save();
            int hourAngle = (int)(30 * (dt.Hour % 12 + (float)dt.Minute / 60));
            g.RotateTransform(hourAngle);
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 5), 0, 0, 0, -65);
            g.Restore(gs);

            //Минуты
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 3), 0, 0, 0, -110);
            g.Restore(gs);

            //Секунды
            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Green), 2), 0, 0, 0, -115);
            g.Restore(gs);
            
            //Если оставить Refresh() то таймер игнорируется
            //this.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}