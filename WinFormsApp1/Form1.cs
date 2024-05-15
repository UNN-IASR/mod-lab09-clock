using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinFormsApp1
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
            Random rnd = new Random();
            Graphics g = e.Graphics;
            //бэкграунд градиент
            DrawLinearGradient(e.Graphics);
            // смещаю центр и маштабирую
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.ScaleTransform(this.Width / 300, this.Height / 300);
            // основание
            g.DrawEllipse(new Pen(Color.PaleGoldenrod, 1), -120, -120, 240, 240);

            GraphicsState gs;
            DateTime dt = DateTime.Now;
            //вывожу рисочки
            for (int i = 0; i < 60; i++)
            {
                gs = g.Save();
                g.RotateTransform(i * 6);
                if (i % 5 == 0)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.MediumVioletRed), 2), -115, 0, -100, 0);
                }
                else
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.White), 1), -115, 0, -110, 0);
                }
                g.Restore(gs);
            }
            // вывожу цифры
            for (int i = 1; i < 13; i++)
            {
                gs = g.Save();
                PointF point = new PointF((float)(-90 * Math.Cos(i * Math.PI / 6 + Math.PI / 2)-6), 
                                            (float)(-90 * Math.Sin(i * Math.PI / 6 + Math.PI / 2)-6));
                g.DrawString(i.ToString(), Font, new SolidBrush(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))), point);
                g.Restore(gs);
            }
            //секундная стрелка
            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.White), 1), 0, 0, 0, -75);
            g.Restore(gs);
            //минутная стрелка
            gs = g.Save();
            g.RotateTransform((int)(6 * (dt.Minute + (float)dt.Second / 60)));
            g.DrawLine(new Pen(new SolidBrush(Color.Blue), 2), 0, 0, 0, -50);
            g.Restore(gs);
            //часовая стрелка
            gs = g.Save();
            g.RotateTransform((int)(30 * (dt.Hour % 12 + (float)dt.Minute / 60)));
            g.DrawLine(new Pen(new SolidBrush(Color.Red), 3), 0, 0, 0, -25);
            g.Restore(gs);

        }
        private void DrawLinearGradient(Graphics graphics)
        {
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.LightGreen, Color.Black,  LinearGradientMode.Vertical)) 
            {
                graphics.FillRectangle(brush, rect);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}