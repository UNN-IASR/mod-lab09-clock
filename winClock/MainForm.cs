using System;
using System.Drawing;
using System.Windows.Forms;

namespace winClock
{
    public partial class MainForm : Form
    {
        private Size clockSize;
        private Bitmap back;
        private Bitmap secArrow;
        private Bitmap minArrow;
        private Bitmap hourArrow;
        public MainForm()
        {
            InitializeComponent();
            clockSize = new Size(this.ClientSize.Width - 100, this.ClientSize.Height - 100);
            back = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            secArrow = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            minArrow = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            hourArrow = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using Graphics g = Graphics.FromImage(back);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.TranslateTransform(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            Pen cpen = new Pen(Color.Black, 4);
            g.DrawEllipse(cpen, -clockSize.Width / 2, -clockSize.Height / 2, clockSize.Width, clockSize.Height);

            StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            float r = clockSize.Height / 2;
            for (int i = 5; i < 65; i++)
            {
                double angle = i * Math.PI / 30 - Math.PI / 2;
                g.DrawLine(
                    cpen,
                    new PointF(r * (float)Math.Cos(angle), r * (float)Math.Sin(angle)),
                    new PointF((r - 10) * (float)Math.Cos(angle), (r - 10) * (float)Math.Sin(angle))
                );
                if (i % 5 == 0)
                {
                    g.DrawLine(
                        cpen,
                        new PointF(r * (float)Math.Cos(angle), r * (float)Math.Sin(angle)),
                        new PointF((r - 25) * (float)Math.Cos(angle), (r - 25) * (float)Math.Sin(angle))
                    );
                    float tx = (r - 45) * (float)Math.Cos(angle);
                    float ty = (r - 45) * (float)Math.Sin(angle);
                    g.DrawString($"{i / 5}", new Font(this.Font.FontFamily, 20), Brushes.Black, new PointF(tx, ty), sf);
                }

            }

            this.BackgroundImage = back;

            using Graphics s = Graphics.FromImage(secArrow);
            CreateArrow(s, Color.Gray, 200);

            using Graphics m = Graphics.FromImage(minArrow);
            CreateArrow(m, Color.Blue, 150);

            using Graphics h = Graphics.FromImage(hourArrow);
            CreateArrow(h, Color.Red, 100);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DateTime now = DateTime.Now;
            Console.WriteLine(now.Millisecond);
            g.DrawImage(Rotated(secArrow, now.Second * 6 + (float)now.Millisecond / 1000 * 6), 0, 0);
            g.DrawImage(Rotated(minArrow, now.Minute * 6 + (float)now.Second / 10), 0, 0);
            g.DrawImage(Rotated(hourArrow, now.Hour * 30), 0, 0);
        }

        private void CreateArrow(Graphics g, Color color, int radius)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TranslateTransform(secArrow.Width / 2, secArrow.Height / 2);
            Pen pen = new Pen(color, 3);
            g.DrawLine(pen, new Point(-5, 20), new Point(0, -radius));
            g.DrawLine(pen, new Point(5, 20), new Point(0, -radius));
            g.DrawLine(pen, new Point(-5, 20), new Point(0, 0));
            g.DrawLine(pen, new Point(5, 20), new Point(0, 0));
        }

        private Bitmap Rotated(Bitmap source, float angle)
        {
            Bitmap result = new Bitmap(source.Width, source.Height);
            Graphics g = Graphics.FromImage(result);
            g.TranslateTransform((float)source.Width / 2, (float)source.Width / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-(float)source.Width / 2, -(float)source.Width / 2);
            g.DrawImage(source, new Point(0, 0));
            return result;
        }
    }
}
