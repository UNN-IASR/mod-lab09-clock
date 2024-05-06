using System.Drawing.Drawing2D;
using System.Reflection;

namespace Clock
{
    public partial class Form1 : Form
    {
        int x0, y0;
        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
           | BindingFlags.Instance | BindingFlags.NonPublic, null,
           panel1, new object[] { true });
            //InitializeComponent();
            y0 = panel1.ClientSize.Height / 2;
            x0 = panel1.ClientSize.Width / 2;
            
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics g = e.Graphics;
            GraphicsState gs;
            DateTime dt = DateTime.Now;
            g.TranslateTransform(x0, y0);
            g.ScaleTransform(x0/100, y0/100);

            g.DrawEllipse(pen, -120, -120, 240, 240);
            gs = g.Save();
           
            g.Restore(gs);
            for (int i = 0; i < 60; i++)
            {
                gs = g.Save();
                g.RotateTransform(i * 6);
                if (i % 5 == 0)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), -119, 0, -100, 0);
                }
                else
                {
                    g.DrawLine(new Pen(brush, 1), -119, 0, -110, 0);
                }
                g.Restore(gs);
            }
            for (int i = 1; i < 13; i++)
            {
                gs = g.Save();
                double x = -90 * Math.Cos(30 * i * Math.PI / 180 + 30 * 3 * Math.PI / 180);
                double y = -90 * Math.Sin(30 * i * Math.PI / 180 + 30 * 3 * Math.PI / 180);
                g.DrawString(i.ToString(), new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.Blue), (float)x - 10, (float)y - 10);
                g.Restore(gs);
            }
            
            //Часы
            gs = g.Save();
            int hourAngle = (int)(30 * (dt.Hour % 12 + (float)dt.Minute / 60));
            g.RotateTransform(hourAngle);
            Pen pen_strelka = new Pen(new SolidBrush(Color.Black), 5);
            pen_strelka.CustomEndCap = new AdjustableArrowCap(5, 5);
            g.DrawLine(pen_strelka, 0, 0, 0, -65);
            g.Restore(gs);

            //Минуты
            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            pen_strelka.Width = 3;
            
            g.DrawLine(pen_strelka, 0, 0, 0, -110);
            g.Restore(gs);

            //Секунды
            gs = g.Save();
            g.RotateTransform(6 * dt.Second);
            pen_strelka.Color = Color.Red;
            pen_strelka.Width = 2;
            g.DrawLine(pen_strelka, 0, 0, 0, -115);
            g.Restore(gs);
            pen.Dispose();
            this.Refresh();

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            y0 = panel1.ClientSize.Height / 2;
            x0 = panel1.ClientSize.Width / 2;
            panel1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
