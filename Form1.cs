using System.Drawing;
using System.Drawing.Drawing2D;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;

namespace clock
{
    public partial class Form1 : Form
    {
        private static int totalHoursOnClock = 12;
        private static int totalMinutesOnClock = 60;
        private static int totalSecondsOnClock = 60;

        private static int degreesForOneHour = 360 / totalHoursOnClock;
        private static int degreesForOneMinute = 360 / totalMinutesOnClock;
        private static int degreesForOneSecond = 360 / totalSecondsOnClock;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            Pen pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.LightGray);

            Graphics g = e.Graphics;

            GraphicsState gs;

            // Круг
            g.TranslateTransform(Width / 2, Height / 2);
            int minDimension = Width < Height ? Width : Height;
            g.ScaleTransform(minDimension / 300, minDimension/300);
            g.DrawEllipse(pen, -120, -120, 240, 240);
            g.FillEllipse(brush, -120, -120, 240, 240);

            // Циферблат
            for (int i = 0; i < totalHoursOnClock; i++)
            {
                gs = g.Save();
                g.RotateTransform(degreesForOneHour * i + 45);
                g.DrawLine(pen, -70, -70, -85, -85);
                g.Restore(gs);
            }
            for (int i = 0; i < totalMinutesOnClock; i++)
            {
                gs = g.Save();
                g.RotateTransform(degreesForOneMinute * i + 45);
                g.DrawLine(pen, -80, -80, -85, -85);
                g.Restore(gs);
            }

            // Секундная стрелка
            gs = g.Save();
            g.RotateTransform(degreesForOneSecond * currentTime.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 4), 0, 0, 0, -80);
            g.Restore(gs);

            // Минутная стрелка
            gs = g.Save();
            g.RotateTransform(degreesForOneMinute * currentTime.Minute + (float)currentTime.Second / 10);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), 0, 0, 0, -80);
            g.Restore(gs);

            // Часовая стрелка
            gs = g.Save();
            g.RotateTransform(degreesForOneHour * currentTime.Hour + (float)currentTime.Minute / 24);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), 0, 0, 0, -40);
            g.Restore(gs);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}