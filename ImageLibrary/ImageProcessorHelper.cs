using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using Libraries.CommonUtilities;
using Libraries.CommonUtilities.Models;
using System.Collections.Generic;
using System.Drawing;

namespace ImageLibrary
{
    public class ImageProcessorHelper : Libraries.CommonUtilities.Interfaces.IImageActions
    {
        public void ConvertToScanned(string filename)
        {
            throw new System.NotImplementedException();
        }

        public void ImageToPdf(List<string> inputfiles, string outputlocation = "")
        {
            throw new System.NotImplementedException();
        }

        public void Optimize(string inputfile, int finalsize = 1024)
        {
            var outputpath = inputfile.GetOutputPath(ActionType.OPTIMIZE);

            ISupportedImageFormat format = new JpegFormat();
            Size size = new Size(finalsize, 0);

            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                imageFactory.Load(inputfile)
                            .Resize(size)
                            .Format(format)
                            .Save(outputpath);
            }
        }

        public void Rotate(string inputfile, float rotation)
        {
            var outputpath = inputfile.GetOutputPath(ActionType.ROTATE, additionalData: rotation.ToString());

            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                imageFactory.Load(inputfile)
                            .Rotate(rotation)
                            .Save(outputpath);
            }
        }
    }
}
