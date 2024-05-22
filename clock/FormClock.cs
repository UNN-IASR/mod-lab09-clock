using System.Drawing.Drawing2D;

namespace Clock
{
    public partial class FormClock : Form
    {
        public FormClock()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void FormClock_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            DateTime dateTime = DateTime.Now;
            this.Text = dateTime.ToString();

            graphics.TranslateTransform(this.Width / 2, this.Height / 2);
            graphics.ScaleTransform(this.Width / 250, this.Height / 250);

            DrawingDial(graphics);
            DrawingClockHand(graphics, dateTime);
            DrawingMinuteHand(graphics, dateTime);
            DrawingSecondHand(graphics, dateTime);
        }

        private void DrawingDial(Graphics graphics)
        {
            for (int i = 0; i < 12; i++)
            {
                GraphicsState graphicsState = graphics.Save();
                graphics.RotateTransform(i * 30);
                graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 5), -120, 0, -80, 0);
                graphics.Restore(graphicsState);
            }
        }

        private void DrawingClockHand(Graphics graphics, DateTime dateTime)
        {
            GraphicsState graphicsState = graphics.Save();
            int hourAngle = (int)(30 * (dateTime.Hour % 12 + (float)dateTime.Minute / 60));
            graphics.RotateTransform(hourAngle);
            graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 4), 0, 10, 0, -50);
            graphics.Restore(graphicsState);
        }

        private void DrawingMinuteHand(Graphics graphics, DateTime dateTime)
        {
            GraphicsState graphicsState = graphics.Save();
            graphics.RotateTransform(6 * (dateTime.Minute + (float)dateTime.Second / 60));
            graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 2), 0, 10, 0, -70);
            graphics.Restore(graphicsState);
        }

        private void DrawingSecondHand(Graphics graphics, DateTime dateTime)
        {
            GraphicsState graphicsState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);
            graphics.DrawLine(new Pen(new SolidBrush(Color.Red), 1), 0, 20, 0, -75);
            graphics.Restore(graphicsState);
        }

        private void FormClock_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
