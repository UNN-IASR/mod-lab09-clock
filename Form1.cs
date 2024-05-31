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
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DateTime data = DateTime.Now;

            Brush br = new SolidBrush(Color.Blue);
            Pen div = new Pen(Color.White, 3);

            int w = this.Width;
            int h = this.Height;

            Graphics gr = e.Graphics;
            GraphicsState gstate;
            gr.TranslateTransform(w / 2, h / 2);
            gr.ScaleTransform(w / 300, h / 300);

            gr.FillEllipse(br, -75, -75, 150, 150);

            for (int i = 0; i < 12; ++i)
            {
                gstate = gr.Save();
                gr.RotateTransform(30 * i + 45);
                gr.DrawLine(div, -40, -40, -52, -52);
                gr.Restore(gstate);
            }

            gstate = gr.Save(); 
            gr.RotateTransform(6 * data.Second); 
            gr.DrawLine(new Pen(new SolidBrush(Color.DarkBlue), 2), 0, 0, 0, -75); 
            gr.Restore(gstate);

            gstate = gr.Save();
            gr.RotateTransform(6 * (data.Minute + (float)data.Second / 60));
            gr.DrawLine(new Pen(new SolidBrush(Color.DarkBlue), 3), 0, 0, 0, -55);
            gr.Restore(gstate);

            gstate = gr.Save();
            gr.RotateTransform(6 * data.Hour + (float)data.Minute / 10);
            gr.DrawLine(new Pen(new SolidBrush(Color.DarkBlue), 4), 0, 0, 0, -35);
            gr.Restore(gstate);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
