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

namespace mipis9 {
    public partial class Clock : Form {

        public Clock() {
            InitializeComponent();
        }

        private void Clock_Paint(object sender, PaintEventArgs e) {

            DateTime date = DateTime.Now;

            Brush brush = new SolidBrush(Color.White);

            int w = this.Width - 16;
            int h = this.Height -40;

            Graphics g = e.Graphics;
            GraphicsState gs;
            g.TranslateTransform(w / 2, h / 2);
            g.ScaleTransform(w / 280, h / 280);

            gs = g.Save();
            g.RotateTransform(6 * date.Second);
            g.DrawLine(new Pen(new SolidBrush(Color.Orange), 3), 0, 0, 0, -110);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (date.Minute));
            g.DrawLine(new Pen(new SolidBrush(Color.White), 4), 0, 0, 0, -100);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(30 * (date.Hour % 12) + 6 * (float)date.Minute / 12);
            g.DrawLine(new Pen(new SolidBrush(Color.White), 5), 0, 0, 0, -80);
            g.Restore(gs);
        }

        private void Clock_Load(object sender, EventArgs e) {
            timer1.Start();
        }

        private void timer_Tick(object sender, EventArgs e) {
            this.Invalidate();
        }
    }
}
