using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClockCSharp {
    public partial class ClockForm : Form {
        public ClockForm() {
            InitializeComponent();
        }
        private void ClockForm_Load(object sender, EventArgs e) {
            this.DoubleBuffered = true;
            Width = 500;
            Height = 500;
        }
        private void ClockForm_Paint(object sender, PaintEventArgs e) {
            DateTime dt = DateTime.Now;
            Pen silver_pen = new Pen(Color.Silver, 2);
            Graphics g = e.Graphics;
            GraphicsState gs;

            g.TranslateTransform(Width / 2, Height / 2);
            int Form_Width = Width;
            int Form_Height = Height;
            int circle = Form_Width >= Form_Height ? Form_Width : Form_Height;
            g.DrawEllipse(silver_pen, - circle / 4, - circle / 4, circle / 2, circle / 2);
            for(int i = 0; i < 12; i++) {
                if(i == 10 || i == 11)
                    g.DrawRectangle(silver_pen, circle / 4 * (float)Math.Sin(i * Math.PI / 6) - circle / 60, -circle / 4 * (float)Math.Cos(i * Math.PI / 6) - circle / 50, circle / 20, circle / 20);
                else
                    g.DrawRectangle(silver_pen, circle / 4 * (float)Math.Sin(i * Math.PI / 6) - circle / 40, -circle / 4 * (float)Math.Cos(i * Math.PI / 6) - circle / 50, circle / 20, circle / 20);
                g.DrawString(i.ToString(), new Font("Arial", circle / 30), new SolidBrush(Color.Black), circle / 4 * (float)Math.Sin(i * Math.PI / 6) - circle / 50, -circle / 4 * (float)Math.Cos(i * Math.PI / 6) - circle / 50);
            }
            g.DrawLine(new Pen(Color.Red, 4), 0, 0, (int)(Math.Sin(Math.PI / 30 * dt.Second) * circle / 5), - (int)(Math.Cos(Math.PI / 30 * dt.Second) * circle / 5));
            g.DrawLine(new Pen(Color.Green, 4), 0, 0, (int)(Math.Sin(Math.PI / 30 * (dt.Minute + (float)dt.Second / 60)) * circle / 7), -(int)(Math.Cos(Math.PI / 30 * (dt.Minute + (float)dt.Second / 60)) * circle / 7));
            g.DrawLine(new Pen(Color.Blue, 4), 0, 0, (int)(Math.Sin(Math.PI / 6 * (dt.Hour + (float)dt.Minute / 60) + (float)dt.Second / 3600) * circle / 9), -(int)(Math.Cos(Math.PI / 6 * (dt.Hour + (float)dt.Minute / 60) + (float)dt.Second / 3600) * circle / 9));
            gs = g.Save();
            g.Restore(gs);
        }
        private void timer_Tick(object sender, EventArgs e) {
            int h = DateTime.Now.Hour;
            int m = DateTime.Now.Minute;
            int s = DateTime.Now.Second;

            string time = "";

            if (h < 10) {
                time += "0" + h;
            } else {
                time += h;
            }
            time += ":";
            if (m < 10)
            {
                time += "0" + m;
            } else {
                time += m;
            }
            time += ":";
            if (s < 10)
            {
                time += "0" + s;
            } else {
                time += s;
            }
            label.Text = time;
            Controls.Add(label);
            this.Invalidate();
        }
    }
}
