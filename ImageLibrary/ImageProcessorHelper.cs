using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLibrary
{
    public class ImageProcessorHelper
    {
        public static void Optimize(string inputfile, int finalsize = 1024)
        {
            var currentdirectory = Path.GetDirectoryName(inputfile);
            var filename = Path.GetFileNameWithoutExtension(inputfile);

            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + "_Resized" + Path.GetExtension(inputfile);

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

        public static void Rotate(string inputfile, float rotation)
        {
            var currentdirectory = Path.GetDirectoryName(inputfile);
            var filename = Path.GetFileNameWithoutExtension(inputfile);

            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + "_Rotated" + Path.GetExtension(inputfile);
            
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                imageFactory.Load(inputfile)
                            .Rotate(rotation)
                            .Save(outputpath);
            }
        }
    }
}
