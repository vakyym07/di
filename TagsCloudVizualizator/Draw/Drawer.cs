using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Draw
{
    public class Drawer
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public Drawer(Size imageSize)
        {
            bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(bitmap);
        }

        public Size GetStringSize(string str, string stringFont, int stringSize)
        {
            return Size.Ceiling(graphics.MeasureString(str, new Font(stringFont, stringSize)));
        }

        public void DrawRectangles(IEnumerable<Rectangle> rectangles, Color color)
        {
            foreach (var rectangle in rectangles)
                graphics.DrawRectangle(new Pen(color), rectangle);
        }

        public void DrawRectangle(Rectangle rectangle, Color color)
        {
            graphics.DrawRectangle(new Pen(color), rectangle);
        }

        public void DrawString(string str, Font font, Brush brush, RectangleF layoutRectangle)
        {
            var drawFormat = new StringFormat
            {
                Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
            };
            graphics.DrawString(str, font, brush, layoutRectangle, drawFormat);
        }

        public void DrawPoligon(List<Point> points, Color color)
        {
            points.Add(points[0]);
            for (var i = 1; i < points.Count; i++)
                graphics.DrawLine(new Pen(color), points[i - 1], points[i]);
        }

        public void DrawCurve(Point[] points, Color color)
        {
            graphics.DrawCurve(new Pen(color), points);
        }

        public void SaveImage(string path, ImageFormat imageFormat)
        {
            bitmap.Save(path, imageFormat);
        }

        public void SaveImage(string path)
        {
            bitmap.Save(path);
        }

        public void SetBitmap(PictureBox pictureBox)
        {
            pictureBox.Image = bitmap;
        }
    }
}