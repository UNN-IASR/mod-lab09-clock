using System.Drawing.Drawing2D;

namespace mod_lab09_clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int r = Math.Min(this.ClientSize.Width, this.ClientSize.Height) / 2 - 25;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            Pen pen = new Pen(Color.Black, 3);
            g.DrawEllipse(pen, new Rectangle(25, 25, 2*r, 2*r));
            g.TranslateTransform(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            for (int i = 0; i < 60; i++)
            {
                g.DrawLine(pen, 0, r - 10, 0, r);
                g.RotateTransform(6);
            }
            for (int i = 1; i <= 12; i++)
            {
                g.RotateTransform(30);
                g.DrawLine(pen, 0, r - 30, 0, r);
                StringFormat sf = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                GraphicsState gs = g.Save();
                g.TranslateTransform(0, -r + 50);
                g.RotateTransform(-i * 30);
                g.DrawString(i.ToString(), new Font(this.Font.FontFamily, 16, FontStyle.Bold), new SolidBrush(Color.Indigo), 0, 0, sf);
                g.Restore(gs);
            }

            DateTime dt = DateTime.Now;
            float s = dt.Second * 6;
            float m = dt.Minute * 6 + dt.Second / 10 - s;
            float h = dt.Hour * 30 + dt.Minute / 2 - m - s;
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.ArrowAnchor;
            g.RotateTransform(s);
            g.DrawLine(pen, 0, 0, 0, -r + 60);
            pen.Width = 4;
            pen.Color = Color.Blue;
            g.RotateTransform(m);
            g.DrawLine(pen, 0, 0, 0, -r + 90);
            pen.Width = 5;
            pen.Color = Color.Red;
            g.RotateTransform(h);
            g.DrawLine(pen, 0, 0, 0, -r + 120);
            g.RotateTransform(360-s-m-h);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
