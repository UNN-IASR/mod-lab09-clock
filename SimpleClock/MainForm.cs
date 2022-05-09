using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SimpleClock
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      timer.Enabled = true;


    }

    private void timer_Tick(object sender, EventArgs e)
    {
      Invalidate();
    }

    private void MainForm_Paint(object sender, PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      GraphicsState graphicsState;

      int width = this.Width;
      int height = this.Height;

      DateTime dateTime = DateTime.Now;
      graphics.TranslateTransform(width / 2, height / 2);

      Pen penGreen = new Pen(Color.Green, 3);
      Pen penBlack = new Pen(Color.Black, 3);
      Pen penBlue = new Pen(Color.Blue, 3);
      Pen penRed = new Pen(Color.Red, 3);


      graphics.DrawEllipse(penBlack, -100, -100, 200, 200);

      graphicsState = graphics.Save();
      graphics.RotateTransform(6 * dateTime.Second);
      graphics.DrawLine(penGreen, 0, 0, -65, -65);
      graphics.Restore(graphicsState);

      graphicsState = graphics.Save();
      graphics.RotateTransform(6 * dateTime.Minute + dateTime.Second / 5);
      graphics.DrawLine(penRed, 0, 0, -55, -55);
      graphics.Restore(graphicsState);

      graphicsState = graphics.Save();
      graphics.RotateTransform(6 * dateTime.Hour + dateTime.Minute / 5);
      graphics.DrawLine(penBlue, 0, 0, -40, -40);
      graphics.Restore(graphicsState);

      for (int i = 0; i < 12; ++i)
      {
        graphicsState = graphics.Save();
        graphics.RotateTransform(30 * i + 45);
        graphics.DrawLine(penBlack, -60, -60, -70, -70);
        graphics.Restore(graphicsState);
      }

      for (int i = 0; i < 36; ++i)
      {
        graphicsState = graphics.Save();
        graphics.RotateTransform(10 * i);
        graphics.DrawLine(penBlack, -67, -67, -70, -70);
        graphics.Restore(graphicsState);
      }

    }
  }
}
