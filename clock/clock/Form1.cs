using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace clock
{
    public partial class Form1 : Form
    {
        private Color clockFaceColor = Color.White;
        private Color hourHandColor = Color.Black;
        private Color minuteHandColor = Color.Black;
        private Color secondHandColor = Color.DeepSkyBlue;
        private Bitmap clockBitmap;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateClockFace();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void UpdateClockFace()
        {
            int width = ClientSize.Width;
            int height = ClientSize.Height;
            clockBitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(clockBitmap))
            {
                g.TranslateTransform(width / 2, height / 2);
                g.ScaleTransform((float)height / 400, (float)height / 400);
                g.Clear(clockFaceColor);
                Pen p = new Pen(Color.Black, 3);
                g.DrawEllipse(p, -180, -180, 360, 360);
                g.DrawEllipse(p, -5, -5, 10, 10);

                for (int i = 0; i < 60; i++)
                {
                    int a = i % 5 == 0 ? 20 : 10;
                    g.DrawLine(p, 0, 180 - a, 0, 180);
                    g.RotateTransform(6);
                }

                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                for (int i = 0; i < 12; i++)
                {
                    double angle = -i * Math.PI / 6 - Math.PI / 2;
                    g.DrawString((12 - i).ToString(), new Font(Font.FontFamily, 20, FontStyle.Italic), new SolidBrush(Color.DeepSkyBlue), 140 * (float)Math.Cos(angle), 140 * (float)Math.Sin(angle), sf);
                }
            }

            BackgroundImage = clockBitmap;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DateTime dateTime = DateTime.Now;
            Text = dateTime.ToString();

            g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            g.ScaleTransform((float)ClientSize.Height / 400, (float)ClientSize.Height / 400);

            GraphicsState gs = g.Save();
            g.RotateTransform(6 * dateTime.Second + 6 * (float)dateTime.Millisecond / 1000);
            g.DrawLine(new Pen(secondHandColor, 2), 0, 0, 0, -130);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dateTime.Minute + dateTime.Second / 10);
            g.DrawLine(new Pen(minuteHandColor, 3), 0, 0, 0, -110);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * (dateTime.Hour % 12) + 6 * (float)dateTime.Minute / 12);
            g.DrawLine(new Pen(hourHandColor, 5), 0, 0, 0, -90);
            g.Restore(gs);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateClockFace();
        }
    }
}