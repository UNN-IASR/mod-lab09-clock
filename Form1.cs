using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Policy;

namespace mod_lab09_clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var Height = e.ClipRectangle.Height / 2;
            var Width = e.ClipRectangle.Width / 2;
            g.TranslateTransform(Width, Height);

            GraphicsState state = g.Save();
            g.RotateTransform(180 + DateTime.Now.Second * 6);
            Rectangle rectSeconds = new Rectangle()
            {
                X = -1,
                Y = 0,
                Width = 3,
                Height = (int)Height - 20,
            };
            Pen penSeconds = new Pen(Brushes.Pink);
            g.DrawRectangle(penSeconds, rectSeconds);
            g.Restore(state);

            state = g.Save();
            g.RotateTransform(180 + DateTime.Now.Minute * 6);
            Pen penMinutes = new Pen(Brushes.Black);
            Point point1 = new Point(-4, 0);
            Point point2 = new Point(4, 0);
            Point point3 = new Point(0, Height / 2);
            Point[] curvePoints =
            {
                 point1,
                 point2,
                 point3
            };
            g.DrawPolygon(penMinutes, curvePoints);
            g.Restore(state);

            state = g.Save();
            g.RotateTransform(180 + (DateTime.Now.Hour % 12) * 30);
            Rectangle rectHours = new Rectangle()
            {
                X = -3,
                Y = 0,
                Width = 7,
                Height = (int)Height / 4,
            };
            g.FillRectangle(Brushes.Red, rectHours);
            g.Restore(state);

            Pen penCircle = new Pen(Brushes.Black);
            Rectangle rectCircle = new Rectangle()
            {
                X = -Width + 3,
                Y = -Height + 3,
                Width = Width * 2 - 6,
                Height = Height * 2 - 6
            };
            g.DrawEllipse(penCircle, rectCircle);
            
            for (int i = 0; i < 12; i++)
            {
                float angle = - 90 + i * 30;
                float X = (float)(Math.Cos(angle / 180 * Math.PI) * (Height - 20) - 5);
                float Y = (float)(Math.Sin(angle / 180 * Math.PI) * (Height - 20) - 5);
                g.DrawString(i.ToString(), DefaultFont, Brushes.Black, X, Y);
            }

            for (int i = 0; i < 12; i++)
            {
                g.RotateTransform(30);
                Point p1 = new Point()
                {
                    X = 0,
                    Y = Height - 9
                };
                Point p2 = new Point()
                {
                    X = 0,
                    Y = Height - 3
                };
                g.DrawLine(Pens.Gray, p1, p2);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Form1.ActiveForm != null)
            {
                Form1.ActiveForm.Invalidate();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Form1.ActiveForm != null)
            {
                Form1.ActiveForm.Invalidate();
            }
        }
    }
}
