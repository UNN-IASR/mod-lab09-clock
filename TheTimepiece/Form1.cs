using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

namespace TheTimepiece
{
    public partial class MainForm : Form
    {
        int i;
        PointF[] min_points, sec_points, hour_points;
        Matrix rotate_min_matrix, rotate_sec_matrix, rotate_hour_matrix;
        Pen cir_pen = new Pen(Color.Black, 4);
        Pen sec_pen = new Pen(Color.SaddleBrown, 6);
        Pen str_pen = new Pen(Color.SaddleBrown, 4);
        Brush brush = new SolidBrush(Color.SandyBrown);
        public MainForm()
        {
            i = (DateTime.Now.Hour%12)*3600 + DateTime.Now.Minute*60 + DateTime.Now.Second + 1;
            rotate_min_matrix = new Matrix();
            rotate_sec_matrix = new Matrix();
            rotate_hour_matrix = new Matrix();

            //rotate_sec_matrix.Rotate(6 * i);
            //rotate_min_matrix.Rotate((float)0.1 * i);
            //rotate_hour_matrix.Rotate((float)0.05 * for_hour);

            sec_points = [new PointF(0, 0), new PointF(0, -340)];
            min_points = [ new PointF(0, 0), new PointF(-10, -20), new PointF(-10, -340),
                           new PointF(0, -360), new PointF(10, -340), new PointF(10, -20)];
            hour_points = [ new PointF(0, 0), new PointF(-15, -20), new PointF(-15, -220),
                           new PointF(0, -240), new PointF(15, -220), new PointF(15, -20)];

            InitializeComponent();
            timer1.Start();

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            rotate_min_matrix = new Matrix();
            rotate_sec_matrix = new Matrix();
            rotate_hour_matrix = new Matrix();

            PointF[] loc_min_points, loc_sec_points, loc_hour_points;
            loc_sec_points = (PointF[])sec_points.Clone();
            loc_min_points = (PointF[])min_points.Clone();
            loc_hour_points = (PointF[])hour_points.Clone();

            Graphics g = e.Graphics;
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            g.TranslateTransform(w / 2, h / 2);
            g.DrawEllipse(cir_pen, -w / 2, -h / 2, w, h);

            rotate_sec_matrix.Rotate(6*i);
            rotate_sec_matrix.TransformPoints(loc_sec_points);

            rotate_min_matrix.Rotate((float)0.1*i);
            rotate_min_matrix.TransformPoints(loc_min_points);

            rotate_hour_matrix.Rotate((float)0.05 * i/6);
            rotate_hour_matrix.TransformPoints(loc_hour_points);

            g.FillPolygon(brush, loc_min_points);
            g.DrawPolygon(str_pen, loc_min_points);

            g.FillPolygon(brush, loc_hour_points);
            g.DrawPolygon(str_pen, loc_hour_points);

            g.DrawLine(sec_pen, loc_sec_points[0], loc_sec_points[1]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i = (i + 1) % 43200;
            this.Invalidate();
        }
    }
}
