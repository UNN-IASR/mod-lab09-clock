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

namespace App
{
    public partial class Form1 : Form
    {
        int width = 400;
        int height = 400;
        int eps = 5;
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(tick);
            timer.Start();
        }
        void tick(object sender, EventArgs e){
            this.Invalidate();
        }
        void paint_clock(object sender, PaintEventArgs e){
            Graphics g = e.Graphics;
            g.TranslateTransform(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            g.ScaleTransform((float)this.ClientSize.Width / width, (float)this.ClientSize.Height / height);
            paint_clock_face(sender, e);
            paint_clock_date(sender, e);
            paint_clock_arrows(sender, e);
        }

        void paint_clock_arrows(object sender, PaintEventArgs e){
            DateTime dt = DateTime.Now;
            Pen second_pen = new Pen(Color.Black, 2);
            Pen minute_pen = new Pen(Color.Indigo, 4);
            Pen hours_pen = new Pen(Color.Red, 8);
            GraphicsState gs;
            Graphics g = e.Graphics;
            gs = g.Save();
            g.RotateTransform(dt.Second * 6);
            count ++;
            g.DrawLine(second_pen, 0, 0, 0, -width / 2 + eps);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(dt.Minute * 6);
            g.DrawLine(minute_pen, 0, 0, 0, -width / 2 + eps);
            g.Restore(gs);
            gs = g.Save();
            g.RotateTransform(dt.Hour * 15);
            g.DrawLine(hours_pen, 0, 0, 0, -width / 4 + eps);
            g.Restore(gs);
        }
        void paint_clock_face(object sender, PaintEventArgs e){
            int delimiters_len = width / 20;
            Pen cir_pen = new Pen(Color.Black,2);
            Graphics g = e.Graphics;
            GraphicsState begin_state = g.Save();
            g.DrawEllipse(cir_pen, - width / 2 + eps, - height / 2 + eps, width -  2 * eps, height -  2 * eps);
            GraphicsState gs;
            for(int i = 0; i < 4; i++){
                gs = g.Save();
                g.RotateTransform(90 * i);
                g.DrawLine(cir_pen, - width / 2 + eps, 0, - width / 2 + eps + delimiters_len, 0);
                g.Restore(gs);
            }
            g.Restore(begin_state);
        }
        void paint_clock_date(object sender, PaintEventArgs e){
            DateTime dt = DateTime.Now;
            Graphics g = e.Graphics; 
            GraphicsState gs;
            gs = g.Save();
            g.DrawString(dt.ToString("g"), new Font("Arial", 10), new SolidBrush(Color.Black), - 70, height / 4);
            g.Restore(gs);
        }
    }
}
