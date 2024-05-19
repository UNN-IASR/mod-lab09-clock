using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalogClockApp
{
    public partial class Form1 : Form
    {
        private Pen _circlePen = new Pen(Brushes.Black, 2);
        private int _circleDiameter = 300;
        private Pen _dashPen = new Pen(Brushes.Gray, 1);
        private Pen _hourDashPen = new Pen(Brushes.DarkRed, 1);
        private int _hourDashLength = 12;

        private Brush _hourTextBrush = Brushes.DarkRed;

        private Pen _secondsPen = new Pen(Brushes.Gray, 1.5F);
        private Pen _minutesPen = new Pen(Brushes.Black, 2);
        private Pen _hoursPen = new Pen(Brushes.Black, 2);

        private int _secondsLength = 140;
        private int _minutesLength = 120;
        private int _hoursLength = 100;

        public Form1()
        {
            DoubleBuffered = true;
            InitializeComponent();
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            
            DrawFace(g);

            DrawHours(g);
            DrawMinutes(g);
            DrawSeconds(g);
        }

        private void DrawSeconds(Graphics g)
        {
            var time = DateTime.Now;
            var angle = 2*Math.PI / 60F * time.Second;
            float x0 = 0 - 10 * (float)Math.Sin(angle);
            float y0 = 0 - 10 * (float)Math.Cos(angle);
            float x1 = _secondsLength * (float)Math.Sin(angle); 
            float y1 = _secondsLength * (float)Math.Cos(angle);

            g.DrawLine(_secondsPen, x0, -y0, x1, -y1);
        }

        private void DrawMinutes(Graphics g)
        {
            var time = DateTime.Now;
            var angle = 2 * Math.PI / 60F * (time.Second / 60F + time.Minute);
            float x0 = 0 - 10 * (float)Math.Sin(angle);
            float y0 = 0 - 10 * (float)Math.Cos(angle);
            float x1 = _minutesLength * (float)Math.Sin(angle);
            float y1 = _minutesLength * (float)Math.Cos(angle);

            g.DrawLine(_minutesPen, x0, -y0, x1, -y1);
        }

        private void DrawHours(Graphics g)
        {
            var time = DateTime.Now;
            var angle = 2 * Math.PI / 12F * (time.Second / 3600F + time.Minute / 60F + time.Hour);
            float x0 = 0 - 10 * (float)Math.Sin(angle);
            float y0 = 0 - 10 * (float)Math.Cos(angle);
            float x1 = _hoursLength * (float)Math.Sin(angle);
            float y1 = _hoursLength * (float)Math.Cos(angle);

            g.DrawLine(_hoursPen, x0, -y0, x1, -y1);
        }

        private void DrawFace(Graphics g)
        {
            var width = (float)Width;
            var height = (float)Height;
            g.TranslateTransform(width / 2, height / 2);

            float dangle = 2 * (float)Math.PI / 12;
            float angle = 0;
            int hour = 12;
            for (int i = 1; i <= 12; i++)
            {
                float x1 = (_circleDiameter / 2F - 20) * (float)Math.Sin(angle);
                float y1 = (_circleDiameter / 2F - 20) * (float)Math.Cos(angle);
                float x1_1 = (_circleDiameter / 2F - _hourDashLength) * (float)Math.Sin(angle);
                float y1_1 = (_circleDiameter / 2F - _hourDashLength) * (float)Math.Cos(angle);
                float x0 = (_circleDiameter / 2F) * (float)Math.Sin(angle);
                float y0 = (_circleDiameter / 2F) * (float)Math.Cos(angle);

                if (hour % 3 == 0)
                {
                    g.DrawString($"{hour}", Font, _hourTextBrush, x1 - 5, -y1 - 5);
                }
                g.DrawLine(_hourDashPen, x0, -y0, x1_1, -y1_1);
                hour = (hour + 1) % 12;
                angle += dangle;
            }

            angle = 0;
            dangle = 2 * (float)Math.PI / 60;
            for (int i = 1; i <= 60; i++)
            {
                float x1_1 = (_circleDiameter / 2F - _hourDashLength / 2F) * (float)Math.Sin(angle);
                float y1_1 = (_circleDiameter / 2F - _hourDashLength / 2F) * (float)Math.Cos(angle);
                float x0 = (_circleDiameter / 2F - _hourDashLength / 3F) * (float)Math.Sin(angle);
                float y0 = (_circleDiameter / 2F - _hourDashLength / 3F) * (float)Math.Cos(angle);
                g.DrawLine(_dashPen, x0, -y0, x1_1, -y1_1);
                angle += dangle;
            }

            g.DrawEllipse(
                _circlePen,
                -_circleDiameter / 2,
                -_circleDiameter / 2,
                _circleDiameter,
                _circleDiameter);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
