using ImageLibrary;
using Libraries.CommonUtilities.Models;
using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Libraries.CommonUtilities;
using System.Drawing.Drawing2D;

namespace PdfConsole
{
    public partial class RotationForm : Form
    {
        string imagepath = string.Empty;
        ActionUtilities.ActionInfo actinf;
        float precision = 0.01f;

        // The original image.
        private Bitmap OriginalBitmap;

        // The rotated image.
        private Bitmap RotatedBitmap;

        public RotationForm(ActionUtilities.ActionInfo actioninfo)
        {
            InitializeComponent();
            actinf = actioninfo;
            lbl_Rotval.Text = (trackBar1.Value * precision).ToString();
            pb_Image.Image = Image.FromFile(actinf.ActionTarget.First().Filepath);
        }

        private void bt_Rotate_Click(object sender, EventArgs e)
        {
            OriginalBitmap = new Bitmap(actinf.ActionTarget.First().Filepath);
            pb_Image.Image = OriginalBitmap;
            pb_Image.Visible = true;

            float angle = trackBar1.Value * precision;

            // Rotate.
            RotatedBitmap = RotateBitmap(OriginalBitmap, angle);

            // Display the result.
            pb_Image.Image = RotatedBitmap;

            // Size the form to fit.
            SizeForm();
        }

        private Bitmap RotateBitmap(Bitmap bm, float angle)
        {
            // Make a Matrix to represent rotation by this angle.
            Matrix rotate_at_origin = new Matrix();
            rotate_at_origin.Rotate(angle);

            // Rotate the image's corners to see how big
            // it will be after rotation.
            PointF[] points =
            {
                new PointF(0, 0),
                new PointF(bm.Width, 0),
                new PointF(bm.Width, bm.Height),
                new PointF(0, bm.Height),
            };
            rotate_at_origin.TransformPoints(points);
            float xmin, xmax, ymin, ymax;
            GetPointBounds(points, out xmin, out xmax, out ymin, out ymax);

            // Make a bitmap to hold the rotated result.
            int wid = (int)Math.Round(xmax - xmin);
            int hgt = (int)Math.Round(ymax - ymin);
            Bitmap result = new Bitmap(wid, hgt);
            result.SetResolution(bm.HorizontalResolution, bm.VerticalResolution);
            // Create the real rotation transformation.
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(angle,
                new PointF(wid / 2f, hgt / 2f));

            // Draw the image onto the new bitmap rotated.
            using (Graphics gr = Graphics.FromImage(result))
            {
                // Use smooth image interpolation.
                gr.InterpolationMode = InterpolationMode.High;

                // Clear with the color in the image's upper left corner.
                //gr.Clear(bm.GetPixel(0, 0));

                //// For debugging. (Makes it easier to see the background.)
                gr.Clear(SystemColors.ControlDark);

                // Set up the transformation to rotate.
                gr.Transform = rotate_at_center;

                // Draw the image centered on the bitmap.
                int x = (wid - bm.Width) / 2;
                int y = (hgt - bm.Height) / 2;
                gr.DrawImage(bm, x, y);
            }

            // Return the result bitmap.
            return result;
        }

        private void GetPointBounds(PointF[] points, out float xmin, out float xmax, out float ymin, out float ymax)
        {
            xmin = points[0].X;
            xmax = xmin;
            ymin = points[0].Y;
            ymax = ymin;
            foreach (PointF point in points)
            {
                if (xmin > point.X) xmin = point.X;
                if (xmax < point.X) xmax = point.X;
                if (ymin > point.Y) ymin = point.Y;
                if (ymax < point.Y) ymax = point.Y;
            }
        }

        // Make sure the form is big enough to show the rotated image.
        private void SizeForm()
        {
            int wid = pb_Image.Right + pb_Image.Left;
            int hgt = pb_Image.Bottom + pb_Image.Left;

            this.ClientSize = new Size(
                Math.Max(wid, this.ClientSize.Width),
                Math.Max(hgt, this.ClientSize.Height));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lbl_Rotval.Text = (trackBar1.Value * precision).ToString();
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            RotatedBitmap.Save(actinf.ActionTarget.First().Filepath.GetOutputPath(ActionType.ROTATE, additionalData: actinf.rotation.ToString()));
        }

        private void pb_Image_Paint(object sender, PaintEventArgs e)
        {
            var gr = e.Graphics;

            var numcell = 10;

            for (int i = 0; i < pb_Image.Height; i = i + pb_Image.Height / numcell)
            {
                gr.DrawLine(new Pen(Color.LightGray, 1), new Point(0, i), new Point(pb_Image.Width, i));
            }
        }
    }
}
