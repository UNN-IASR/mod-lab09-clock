using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
//using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src_lab9
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            timer.Interval = 1000; //интервал между срабатываниями 1000 миллисекунд
            timer.Tick += new EventHandler(timer1_Tick); //подписываемся на события Tick
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            DateTime dt = DateTime.Now; //get time

            GraphicsPath hoPath = new GraphicsPath(), minPath = new GraphicsPath();
            hoPath.AddEllipse(new RectangleF(-1, -1, 2, 2)); minPath.AddEllipse(new RectangleF(-1, -1, 2, 2));
            CustomLineCap hoCap = new CustomLineCap(hoPath, null), minCap = new CustomLineCap(minPath, null);

              //create pens
            Pen min_pen = new Pen(new SolidBrush(Color.DarkSlateGray), 3);
            min_pen.CustomStartCap = minCap;
            min_pen.CustomEndCap = new AdjustableArrowCap(2, 5);
            Pen hour_pen = new Pen(new SolidBrush(Color.DimGray), 5);
            hour_pen.CustomStartCap = hoCap;
            hour_pen.CustomEndCap = new AdjustableArrowCap(1, 1);

            Pen sec_pen = new Pen(new SolidBrush(Color.DarkRed), 1.5F);
            

                //get graphics
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            GraphicsState gs;

            float w = this.ClientSize.Width, h = this.ClientSize.Height; //optimize graphic for a clock
            int size = (int)Math.Min(h, w);
            e.Graphics.TranslateTransform(w / 2, h / 2);
            int scale = 200; float mult = (float)(size-10)/scale;
            e.Graphics.ScaleTransform(mult, mult);

            Draw_back(g, scale);  //draw a circle and numbers

            gs = g.Save();  //save state
            g.RotateTransform(30 * (dt.Hour + (float)dt.Minute/60 + (float)dt.Second / 3600)); //rotate degrees on minute arrow
            g.DrawLine(hour_pen, 0, 0, 0, -scale / 3);
            g.Restore(gs);  //restore saved state

            gs = g.Save();
            g.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60)); //rotate degrees on minute arrow
            g.DrawLine(min_pen, 0, 0, 0, -5*scale/12);
            g.Restore(gs);  //restore saved state

            gs = g.Save();
            g.RotateTransform( 6 * (dt.Second ) ); //rotate degrees on minute arrow
            g.DrawLine(sec_pen, 0, scale/9, 0, -11*scale / 24);
            g.Restore(gs);  //restore saved state
        }

        private void Draw_back(Graphics gr, int scale) {
            Pen cir_pen = new Pen(Color.Black, 2);  //create pens

            gr.DrawEllipse(cir_pen, -scale / 2, -scale / 2, scale, scale);   //draw a static circle 
            GraphicsState gs;
            cir_pen.Width = 1;
            for(int i=1; i<= 360; i++){ 
                gs = gr.Save();  //save state
                if (i % 30 == 0)
                {
                    float ang = (float)Math.PI * i / 180;
                    gr.DrawString(Convert.ToString(i / 30), DefaultFont, new SolidBrush(Color.Indigo), (float)(scale* (5F/12)*Math.Sin(ang) - 5F), (float)(scale *(-5F/ 12 )* Math.Cos(ang) -5F));
                    gr.RotateTransform(i); 
                    gr.DrawLine(cir_pen, 0, -11*scale/24, 0, -95*scale / 192);
                }
                else if (i % 6 == 0)
                {
                    gr.RotateTransform(i); 
                    gr.DrawLine(cir_pen, 0, -47 * scale / 96, 0, -95 * scale / 192);
                }
                gr.Restore(gs);  //restore saved state
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

    }
}
