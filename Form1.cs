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

namespace clock_9_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;

            Pen cir_pen = new Pen(Color.Red, 4);
            Brush brush1 = new SolidBrush(Color.Pink);
            Brush brush2 = new SolidBrush(Color.Purple);
            Pen sep = new Pen(Color.Red, 3);

            int width = this.Width;
            int height = this.Height;

            Graphics g = e.Graphics;
            GraphicsState gs;
            g.TranslateTransform(width / 2, height / 2);
            g.ScaleTransform(width / 300, height / 300);

            g.DrawEllipse(cir_pen, -80, -80, 160, 160);
            g.FillEllipse(brush2, -80, -80, 160, 160);
            g.FillEllipse(brush1, -75, -75, 150, 150);

            for (int i = 0; i < 4; ++i)
            {
                gs = g.Save();
                g.RotateTransform(90 * i + 45);
                g.DrawLine(sep, -40, -40, -52, -52);
                g.Restore(gs);
            }

            gs = g.Save(); 
            g.RotateTransform(6 * dt.Second); 
            g.DrawLine(new Pen(new SolidBrush(Color.White), 2), 0, 0, 0, -75); 
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            g.DrawLine(new Pen(new SolidBrush(Color.Gray), 3), 0, 0, 0, -55);
            g.Restore(gs);

            gs = g.Save();
            g.RotateTransform(6 * dt.Hour + (float)dt.Minute / 10);
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), 0, 0, 0, -35);
            g.Restore(gs);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
