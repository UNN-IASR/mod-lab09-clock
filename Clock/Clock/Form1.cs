namespace Clock
{
    public partial class Form1 : Form
    {
        DateTime dateTime;
        int min;
        int hour;
        int sec;
        int msec;
        DateTime datetime;
        Pen cir_pen = new Pen(Color.Black, 2);
        Brush brush = new SolidBrush(Color.Indigo);
        Brush black = new SolidBrush(Color.Black);
        Font font = new Font("Arial", 10);
        Pen dial_pen = new Pen(Color.Black, 2);
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            SetTime();
            Invalidate();
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float w = this.ClientSize.Width;
            float h = this.ClientSize.Height;
            g.TranslateTransform(w / 2, h / 2);
            //g.DrawLine(cir_pen, 0, -h / 2, 0, h / 2);
            //g.DrawLine(cir_pen, -w/2, 0, w/2, 0);
            //g.DrawRectangle(cir_pen, -5, -5, 10, 10);
            float r1 = (float)0.45 * w, r2 = (float)0.45 * h;
            var scale = Math.Sqrt(r1 * r2) / 100;
            Pen dial_pen = new Pen(Color.Black, (float)scale);
            //g.DrawRectangle(cir_pen, -r1, -r2, 2 * r1, 2 * r2);
            DrawTheDial(g, dial_pen, r1, r2);
            DrawArrows(g, r1, r2);
        }
        private void DrawArrows(Graphics g, float r1, float r2)
        {
            var pi = Math.PI;
            double angle_sec = 2 * pi * (sec+msec/ (double)1000) / 60;
            double angle_min = (2 * pi / 60) *min + angle_sec/60;
            double angle_hour = (2*pi/60)*hour + angle_min/60;
            angle_sec -= pi / 2; angle_min -= pi / 2; //angle_hour -= pi / 2;
            var scale = Math.Sqrt(r1 * r2)/100;
            Pen sec_pen = new Pen(Color.Red, (float)scale);
            Pen min_pen = new Pen(Color.Chocolate, (float)scale*2);
            Pen hour_pen = new Pen(Color.BlueViolet, (float)scale * 3);
            g.DrawLine(sec_pen, new PointF(0, 0),
                                new PointF((float)(0.95*r1*Math.Cos(angle_sec)),(float)(0.95*r2 * Math.Sin(angle_sec))));
            g.DrawLine(min_pen, new PointF(0, 0),
                                new PointF((float)(0.85*r1 * Math.Cos(angle_min)), (float)(0.85*r2 * Math.Sin(angle_min))));
            g.DrawLine(hour_pen, new PointF(0, 0),
                                new PointF((float)(0.75 * r1 * Math.Cos(angle_hour)), (float)(0.75 * r2 * Math.Sin(angle_hour))));
        }
        private void DrawTheDial(Graphics g, Pen pen, float r1, float r2)
        {
            var pi = Math.PI;
            double angle;
            g.DrawEllipse(cir_pen, (float)-r1, (float)-r2, (float)2*r1, (float)2*r2);
            g.FillEllipse(black, (float)-0.05 * r1, (float)-0.05 * r2, (float)0.05 * 2 * r1, (float)0.05 * 2 * r2);
            for (int i = 1; i <= 60; i++)
            {
                angle = 2 * pi * i / 60;
                if (i % 5 != 0)
                {
                    g.DrawLine(pen, new PointF((float)(r1 * Math.Cos(angle)),
                                               (float)(r2 * Math.Sin(angle))),
                                    new PointF((float)(0.97 * r1 * Math.Cos(angle)),
                                               (float)(0.97 * r2 * Math.Sin(angle))));
                }
                else
                    g.DrawLine(pen, new PointF((float)(r1 * Math.Cos(angle)),
                                               (float)(r2 * Math.Sin(angle))),
                                    new PointF((float)(0.90 * r1 * Math.Cos(angle)),
                                               (float)(0.90 * r2 * Math.Sin(angle))));
            }
            Font font = new Font("Arial", Math.Max(r1, r2)/30);
            
            for (int i = 0; i < 12; i++)
            {
                angle = 2 * pi * i / 12;
                int num = (i + 2) % 12 + 1;
                g.DrawString(num.ToString(), font, black,
                    new PointF(((float)(0.9*r1*Math.Cos(angle))),
                                (float)(0.9*r2*Math.Sin(angle))));
            }
        }

        private void SetTime()
        {
            datetime = DateTime.Now;
            msec = datetime.Millisecond;
            sec = datetime.Second;
            min = datetime.Minute;
            hour = datetime.Hour;
        }

        private void UpdateTime()
        {
            datetime.AddMilliseconds(timer1.Interval);
            msec = datetime.Millisecond;
            sec = datetime.Second;
            min = datetime.Minute;
            hour = datetime.Hour;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetTime();
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Invalidate();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
