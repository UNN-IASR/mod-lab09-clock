using System.Drawing.Drawing2D;

namespace Clock
{
    public partial class ClockForm : Form
    {
        Pen second_pen = new Pen(Color.Red, 4);
        Pen minute_pen = new Pen(Color.Black, 6);
        Pen hour_pen = new Pen(Color.Black, 8);

        public ClockForm()
        {
            InitializeComponent();

            this.Size = BackgroundImage.Size + new Size(18, 46);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


            timer1.Enabled = true;
            timer1.Start();
        }

        private void ClockForm_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            Graphics g = e.Graphics;
            GraphicsState gs;

            float w = this.BackgroundImage.Size.Width;
            float h = this.BackgroundImage.Size.Height;

            g.TranslateTransform(w / 2, h / 2);

            gs = g.Save();

            g.RotateTransform(6 * (float)dt.Second);
            g.DrawLine(second_pen, 0, 0, 0, -100);

            g.Restore(gs);
            gs = g.Save();

            g.RotateTransform(6 * (float)dt.Minute);
            g.DrawLine(minute_pen, 0, 0, 0, -100);

            g.Restore(gs);
            gs = g.Save();

            g.RotateTransform(30 * (float)dt.Hour);
            g.DrawLine(hour_pen, 0, 0, 0, -80);

            g.Restore(gs);
            gs = g.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}