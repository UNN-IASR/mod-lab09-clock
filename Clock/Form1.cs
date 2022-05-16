using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Clock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            double a = 30;
            Label[] label = new Label[12];
            for (int i = 0; i < 12; i++)
            {
                label[i] = new Label();
                if (i < 6) label[i].Location = new Point(380 + Convert.ToInt32(265 * MySin(a)), 380 - Convert.ToInt32(265 * MyCos(a)));
                else label[i].Location = new Point(380 + Convert.ToInt32(260 * MySin(a)), 380 - Convert.ToInt32(260 * MyCos(a)));
                label[i].Text = (i + 1).ToString();
                label[i].Font = new Font("Arial", 22, FontStyle.Bold);
                label[i].Size = new Size(100, 100);
                label[i].BackColor = Color.Transparent;
                Controls.Add(label[i]);
                a += 30;
            }
            timer1.Start();
            this.Paint += Form1_Paint;
            DoubleBuffered = true;
            Width = 800;
            Height = 800;
        }

        static double MySin(double angle)
        {
            return Math.Sin((angle / 180D) * Math.PI);
        }
        static double MyCos(double angle)
        {
            return Math.Cos((angle / 180D) * Math.PI);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DateTime dt = DateTime.Now;
            Pen cir_pen = new Pen(Color.Black, 2);
            Pen k_pen = new Pen(Color.Black, 4);
            Brush brush = new SolidBrush(Color.Indigo);
            Graphics draw = e.Graphics;
            GraphicsState gs;

            draw.TranslateTransform(Width / 2, Height / 2);
            draw.ScaleTransform(370 / 180, 370 / 180);
            draw.DrawEllipse(k_pen, -140, -140, 280, 280);
            gs = draw.Save();
            SolidBrush color = new SolidBrush(Color.Gold);
            e.Graphics.FillEllipse(color, -140, -140, 280, 280);
            for (int i = 0; i < 12; i++)
            {
                draw.RotateTransform(30 * i);
                draw.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, -120, 0, -100);
            }

            draw.RotateTransform(-120);
            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, -120, 0, -100);
            draw.RotateTransform(90);
            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, -120, 0, -100);
            draw.RotateTransform(90);
            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, -120, 0, -100);
            draw.RotateTransform(90);
            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, -120, 0, -100);

            for (int i = 0; i < 60; i++)
            {
                draw.RotateTransform(6 * i);
                draw.DrawLine(new Pen(new SolidBrush(Color.Black), 1), 0, -120, 0, -120 + 10);
                draw.Restore(gs);
                gs = draw.Save();
            }

            gs = draw.Save();

            draw.RotateTransform(6 * (dt.Minute + (float)dt.Second / 60));
            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 3), 0, 0, 0, -75);
            draw.Restore(gs);
            gs = draw.Save();
            draw.RotateTransform(6 * (float)dt.Second);
            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 2), 0, 0, 0, -100);
            draw.Restore(gs);
            gs = draw.Save();
            draw.RotateTransform(30 * (dt.Hour + (float)dt.Minute / 60 + (float)dt.Second / 3600));
            draw.DrawLine(new Pen(new SolidBrush(Color.Black), 2), 0, 0, 0, -50);
            draw.Restore(gs);
            gs = draw.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
