using Libraries.CommonUtilities;
using Libraries.CommonUtilities.Models;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfConsole
{
    public partial class RotationForm : Form
    {
        ActionUtilities.ActionInfo actinf;
        float precision = 0.01f;

        private Image optimizdImage;
        private Image originalImage;

        public RotationForm(ActionUtilities.ActionInfo actioninfo)
        {
            InitializeComponent();
            actinf = actioninfo;

            LoadFiles();

            lbl_Rotval.Text = SelectedAngle.ToString();
        }

        async private void LoadFiles()
        {
            filelist.DataSource = actinf.ActionTarget;
            filelist.DisplayMember = "Filename";
            filelist.ValueMember = "Filepath";

            lbl_Status.Text = "Please wait";
            await Task.Factory.StartNew(() => LoadImages(), TaskCreationOptions.LongRunning);
            lbl_Status.Text = "";
        }

        async private void filelist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lbl_Status.Text = "Please wait";
            await Task.Factory.StartNew(() => LoadImages(), TaskCreationOptions.LongRunning);
            lbl_Status.Text = "";
        }

        private void LoadImages()
        {
            string imagefile = "";
            if (filelist.InvokeRequired)
            {
                filelist.Invoke(new MethodInvoker(delegate { imagefile = filelist.SelectedValue.ToString(); }));
            }

            optimizdImage = new ImageLibrary.ImageProcessorHelper().Optimize(imagefile, retobj: true);
            originalImage = new ImageLibrary.ImageProcessorHelper().Optimize(imagefile, finalsize: Image.FromFile(imagefile).Width, retobj: true);

            pb_Image.Image = optimizdImage;
            pb_Image.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private float SelectedAngle { get { return angleSelector.Value * precision; } }

        private void AngleSelector_Scroll(object sender, EventArgs e)
        {
            lbl_Rotval.Text = (angleSelector.Value * precision).ToString();
        }

        private void SizeForm()
        {
            int wid = pb_Image.Right + pb_Image.Left;
            int hgt = pb_Image.Bottom + pb_Image.Left;

            this.ClientSize = new Size(Math.Max(wid, this.ClientSize.Width), Math.Max(hgt, this.ClientSize.Height));
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            var bm = new Bitmap(originalImage);
            var outputpath = filelist.SelectedValue.ToString().GetOutputPath(ActionType.ROTATE, additionalData: Math.Abs(SelectedAngle).ToString(), formatChange: true, newExtension: ".png");

            bm.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);
            RotateBitmap(bm, SelectedAngle, SystemColors.ControlLight).Save(outputpath, System.Drawing.Imaging.ImageFormat.Png);
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

        private Bitmap RotateBitmap(Bitmap bm, float angle, Color backcolor)
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

            GetPointBounds(points, out float xmin, out float xmax, out float ymin, out float ymax);

            // Make a bitmap to hold the rotated result.
            int wid = (int)Math.Round(xmax - xmin);
            int hgt = (int)Math.Round(ymax - ymin);

            Bitmap result = new Bitmap(wid, hgt);
            result.SetResolution(bm.HorizontalResolution, bm.VerticalResolution);

            // Create the real rotation transformation.
            Matrix rotate_at_center = new Matrix();
            rotate_at_center.RotateAt(angle, new PointF(wid / 2f, hgt / 2f));

            // Draw the image onto the new bitmap rotated.
            using (Graphics gr = Graphics.FromImage(result))
            {
                // Use smooth image interpolation.
                gr.InterpolationMode = InterpolationMode.High;

                // Clear with the color in the image's upper left corner.
                //gr.Clear(bm.GetPixel(0, 0));

                //// For debugging. (Makes it easier to see the background.)
                gr.Clear(backcolor);

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

        private void AngleSelector_MouseUp(object sender, MouseEventArgs e)
        {
            pb_Image.Image = RotateBitmap(new Bitmap(optimizdImage), SelectedAngle, SystemColors.ControlLight);
            SizeForm();
        }
    }
}
