using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Clock
{
    public partial class Form1 : Form
    {
        bool isResizing;
        public Form1()
        {
            InitializeComponent();
            isResizing = false;
            this.timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (!isResizing)
            {
                int w = this.ClientRectangle.Width;
                int h = this.ClientRectangle.Height;
                float r = w < h ? w * 0.4f : h * 0.4f;

                DateTime dt = DateTime.Now;
                //this.Text = dt.ToString();

                Pen cir_pen = new Pen(Color.Black, 5);

                Graphics g = e.Graphics;
                GraphicsState gs;

                g.TranslateTransform(w / 2, h / 2);

                g.DrawEllipse(cir_pen, (int)-r, (int)-r, 2 * (int)r, 2 * (int)r);

                for (int i = 0; i < 60; i++)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 5),
                        (float)Math.Cos(i * Math.PI / 30) * r,
                        (float)Math.Sin(i * Math.PI / 30) * r,
                        (float)Math.Cos(i * Math.PI / 30) * 0.97f * r,
                        (float)Math.Sin(i * Math.PI / 30) * 0.97f * r);
                }

                for (int i = 0; i < 12; i++)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 5),
                        (float)Math.Cos(i * Math.PI / 6) * r,
                        (float)Math.Sin(i * Math.PI / 6) * r,
                        (float)Math.Cos(i * Math.PI / 6) * 0.95f * r,
                        (float)Math.Sin(i * Math.PI / 6) * 0.95f * r);
                }

                g.DrawLine(new Pen(new SolidBrush(Color.Black), 5), 0, r, 0, 0.9f * r);
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 5), 0, -r, 0, -0.9f * r);
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 5), r, 0, 0.9f * r, 0);
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 5), -r, 0, -0.9f * r, 0);

                if (r > 150)
                {
                    g.DrawString("12", new Font(Font.FontFamily, 14), Brushes.Black, -14, -0.9f * r + 4);
                    g.DrawString("3", new Font(Font.FontFamily, 14), Brushes.Black, 0.9f * r - 20, -10);
                    g.DrawString("6", new Font(Font.FontFamily, 14), Brushes.Black, -8, 0.9f * r - 26);
                    g.DrawString("9", new Font(Font.FontFamily, 14), Brushes.Black, -0.9f * r + 6, -10);

                    g.DrawString("1", new Font(Font.FontFamily, 14), Brushes.Black, 
                        (float)Math.Cos(Math.PI / 3) * 0.9f * r - 10, 
                        (float)Math.Sin(-Math.PI / 3) * 0.9f * r - 4);
                    g.DrawString("2", new Font(Font.FontFamily, 14), Brushes.Black,
                        (float)Math.Cos(Math.PI / 6) * 0.9f * r - 10,
                        (float)Math.Sin(-Math.PI / 6) * 0.9f * r - 8);

                    g.DrawString("11", new Font(Font.FontFamily, 14), Brushes.Black,
                        (float)Math.Cos(2 * Math.PI / 3) * 0.9f * r - 10,
                        (float)Math.Sin(-2 * Math.PI / 3) * 0.9f * r - 4);
                    g.DrawString("10", new Font(Font.FontFamily, 14), Brushes.Black,
                        (float)Math.Cos(5 * Math.PI / 6) * 0.9f * r - 10,
                        (float)Math.Sin(-5 * Math.PI / 6) * 0.9f * r - 8);

                    g.DrawString("5", new Font(Font.FontFamily, 14), Brushes.Black,
                        (float)Math.Cos(-Math.PI / 3) * 0.9f * r - 10,
                        (float)Math.Sin(Math.PI / 3) * 0.9f * r - 14);
                    g.DrawString("4", new Font(Font.FontFamily, 14), Brushes.Black,
                        (float)Math.Cos(-Math.PI / 6) * 0.9f * r - 10,
                        (float)Math.Sin(Math.PI / 6) * 0.9f * r - 14);

                    g.DrawString("7", new Font(Font.FontFamily, 14), Brushes.Black,
                        (float)Math.Cos(-2 * Math.PI / 3) * 0.9f * r - 7,
                        (float)Math.Sin(2 * Math.PI / 3) * 0.9f * r - 14);
                    g.DrawString("8", new Font(Font.FontFamily, 14), Brushes.Black,
                        (float)Math.Cos(-5 * Math.PI / 6) * 0.9f * r - 7,
                        (float)Math.Sin(5 * Math.PI / 6) * 0.9f * r - 14);
                }

                gs = g.Save();

                float[] secs = new float[2];
                secs[0] = (float)Math.Cos((dt.Second - 15) * Math.PI / 30) * 0.9f * r;
                secs[1] = (float)Math.Sin((dt.Second - 15) * Math.PI / 30) * 0.9f * r;
                float[] mins = new float[2];
                mins[0] = (float)Math.Cos((dt.Minute - 15 + (dt.Second / 60f)) * Math.PI / 30) * 0.7f * r;
                mins[1] = (float)Math.Sin((dt.Minute - 15 + (dt.Second / 60f)) * Math.PI / 30) * 0.7f * r;
                float[] hours = new float[2];
                hours[0] = (float)Math.Cos((dt.Hour - 3 + (dt.Minute / 50f)) * Math.PI / 6) * 0.4f * r;
                hours[1] = (float)Math.Sin((dt.Hour - 3 + (dt.Minute / 50f)) * Math.PI / 6) * 0.4f * r;
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), 0, 0, secs[0], secs[1]);
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), 0, 0, mins[0], mins[1]);
                g.DrawLine(new Pen(new SolidBrush(Color.Black), 6), 0, 0, hours[0], hours[1]);

                g.FillEllipse(new SolidBrush(Color.Black), -3, -3, 6, 6);

                g.Restore(gs);
            }
            else
            {
                Graphics g = e.Graphics;
                GraphicsState gs;
                gs = g.Save();
                g.Restore(gs);
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            isResizing = false;
            this.Invalidate();
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            isResizing = true;
            this.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
