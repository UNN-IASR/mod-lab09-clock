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

namespace mips_lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            DateTime dateTime = DateTime.Now;   //Получение доступа к текущему времени

            Graphics graphics = e.Graphics;     //Получение графического контекста
            GraphicsState gState;       //Объявление объекта для сохранения текущего состояния

            int width = this.Width;
            int height = this.Height;

            graphics.TranslateTransform(width / 2, height / 2);  //перенесение начала координат в центр

            //Создание перьев и кистей для рисования
            Pen black_pen = new Pen(Color.Black, 2);
            Pen pink_pen = new Pen(Color.Pink, 2);
            Pen skyblue_pen = new Pen(Color.SkyBlue, 2);
            Pen purple_pen = new Pen(Color.Purple, 2);

            SolidBrush myBrush = new SolidBrush(Color.White);

            graphics.FillEllipse(myBrush, -100, -100, 200, 200);    //пространство между циферблатом и стрелками

            graphics.DrawEllipse(black_pen, -100, -100, 200, 200);   //рисование циферблата
            graphics.DrawEllipse(black_pen, -105, -105, 210, 210);   //рисование циферблата


            //рисование стрелок

            //секундная стрелка
            gState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Second);
            graphics.DrawLine(pink_pen, 3, 0, -60, -60);
            graphics.DrawLine(pink_pen, -3, 0, -60, -60);
            graphics.DrawLine(pink_pen, 0, 0, -60, -60);
            graphics.Restore(gState);

            //минутная стрелка
            gState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Minute + (float)dateTime.Second / 10);
            graphics.DrawLine(purple_pen, 3, 0, -45, -45);
            graphics.DrawLine(purple_pen, -3, 0, -45, -45);
            graphics.DrawLine(purple_pen, 0, 0, -45, -45);
            graphics.Restore(gState);


            //часовая стрелка
            gState = graphics.Save();
            graphics.RotateTransform(6 * dateTime.Hour + (float)dateTime.Minute / 10);
            graphics.DrawLine(skyblue_pen, 2, 0, -30, -30);
            graphics.DrawLine(skyblue_pen, -2, 0, -30, -30);
            graphics.DrawLine(skyblue_pen, 0, 0, -30, -30);
            graphics.Restore(gState);


            //Рисование делений циферблата
            for (int i = 0; i < 12; ++i)
            {
                gState = graphics.Save();
                graphics.RotateTransform(30 * i + 45);
                graphics.DrawLine(black_pen, -60, -60, -70, -70);
                graphics.Restore(gState);
            }

            for (int i = 0; i < 36; ++i)
            {
                gState = graphics.Save();
                graphics.RotateTransform(10 * i);
                graphics.DrawLine(black_pen, -67, -67, -70, -70);
                graphics.Restore(gState);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
