using System.Drawing.Drawing2D;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitializeClock()
        {
            Bitmap clock = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(clock);
            g.SmoothingMode = SmoothingMode.AntiAlias; // Добавляем сглаживание для более плавных краев

            // Фон циферблата
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.FillEllipse(brush, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            }

            // Циферблат
            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawEllipse(pen, 10, 10, ClientSize.Width - 21, ClientSize.Height - 21);
            }

            // Часовые метки
            using (Pen pen = new Pen(Color.Black, 2))
            {
                for (int i = 0; i < 12; i++)
                {
                    double angle = -i * Math.PI / 6 + Math.PI / 2;
                    float x1 = (float)(ClientSize.Width / 2 + (ClientSize.Width / 2 - 30) * Math.Cos(angle));
                    float y1 = (float)(ClientSize.Height / 2 - (ClientSize.Height / 2 - 30) * Math.Sin(angle));
                    float x2 = (float)(ClientSize.Width / 2 + (ClientSize.Width / 2 - 50) * Math.Cos(angle));
                    float y2 = (float)(ClientSize.Height / 2 - (ClientSize.Height / 2 - 50) * Math.Sin(angle));
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }

            BackgroundImage = clock;
        }

        private void DrawClock(Graphics g)
        {
            DateTime dateTime = DateTime.Now;
            Text = dateTime.ToString();
            g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            g.ScaleTransform((float)ClientSize.Height / 400, (float)ClientSize.Height / 400);

            DrawHand(g, Color.DarkRed, 6 * dateTime.Second + 6 * (float)dateTime.Millisecond / 1000, 130, 2);
            DrawHand(g, Color.Black, 6 * dateTime.Minute + dateTime.Second / 10, 110, 3);
            DrawHand(g, Color.Black, 30 * (dateTime.Hour % 12) + 6 * (float)dateTime.Minute / 12, 90, 5);
        }

        private void DrawHand(Graphics g, Color color, float angle, int length, int thickness)
        {
            // Сохраняем текущее состояние графики
            GraphicsState initialState = g.Save();

            // Поворачиваем систему координат на заданный угол
            g.RotateTransform(angle);

            // Рисуем стрелку с заданными параметрами
            using (Pen pen = new Pen(color, thickness))
            {
                g.DrawLine(pen, 0, 0, 0, -length);
            }

            // Восстанавливаем изначальное состояние графики
            g.Restore(initialState);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeClock();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawClock(g);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }
    }
}