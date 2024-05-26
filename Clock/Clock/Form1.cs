using System.Drawing.Drawing2D;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap clock = new Bitmap(ClientSize.Width, ClientSize.Height);
            Graphics g = Graphics.FromImage(clock);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2); 
            g.ScaleTransform((float)ClientSize.Height / 400, (float)ClientSize.Height / 400);
           
            Pen p = new Pen(Color.Black, 5);
            g.DrawEllipse(p, -180, -180, 360, 360);
            g.DrawEllipse(p, -3, -3, 7, 7);
            for (int i = 0; i < 60; i++)
            {
                int a = i % 5 == 0 ? 20 : 10;
                g.DrawLine(p, 0, 180 - a, 0, 180);
                g.RotateTransform(6);
            }
            
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            
            sf.LineAlignment = StringAlignment.Center;
            for (int i = 0; i < 12; i++)
            {
                double a = -i * Math.PI / 6 - Math.PI / 2;
                g.DrawString((12 - i).ToString(), new Font(Font.FontFamily, 20, FontStyle.Bold), new SolidBrush(Color.Gold), 140 * (float)Math.Cos(a), 140 * (float)Math.Sin(a), sf);
            
            }
            BackgroundImage = clock;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DateTime dateTime = DateTime.Now;
            Text = dateTime.ToString();
            g.TranslateTransform(ClientSize.Width / 2, ClientSize.Height / 2);
            g.ScaleTransform((float)ClientSize.Height / 400, (float)ClientSize.Height / 400);
           
            GraphicsState gs = g.Save();
            g.RotateTransform(6 * dateTime.Second + 6 * (float)dateTime.Millisecond / 1000);
            g.DrawLine(new Pen(Color.Red, 2), 0, 0, 0, -130);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dateTime.Minute + dateTime.Second / 10);
            g.DrawLine(new Pen(Color.Blue, 3), 0, 0, 0, -110);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * (dateTime.Hour % 12) + 6 * (float)dateTime.Minute / 12);
            g.DrawLine(new Pen(Color.Green, 5), 0, 0, 0, -90);
            g.Restore(gs);
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
