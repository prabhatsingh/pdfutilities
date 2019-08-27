using Libraries.CommonUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XpsLibrary
{
    public class XpsHelper : Libraries.CommonUtilities.Interfaces.IPdfActions
    {
        public List<string> XpsToImage(string inputfile, bool istemp = false)
        {
            var imagefiles = new List<string>();

            using (var xpsConverter = new Xps2Image(inputfile))
            {
                var images = xpsConverter.ToBitmap(new Parameters
                {
                    ImageType = ImageType.Png,
                    Dpi = 300
                });

                if (images.Count() > 1)
                {
                    var outputpath = FileUtilities.GetOutputPath(inputfile, Libraries.CommonUtilities.Models.ActionType.XPSTOIMAGE, isTemp: istemp, formatChange: true, newExtension: ".png", outputNameFormat: "{0}_Image_{1}", hasMultipleOutput: true);

                    int count = 1;
                    images.ToList().ForEach(f =>
                    {
                        var imgfilename = string.Format(outputpath, Path.GetFileNameWithoutExtension(inputfile), count++);
                        imagefiles.Add(imgfilename);
                        f.Save(imgfilename);
                    });
                }
                else
                {
                    var outputpath = FileUtilities.GetOutputPath(inputfile, Libraries.CommonUtilities.Models.ActionType.XPSTOIMAGE, isTemp: istemp, formatChange: true, newExtension: ".png");
                    imagefiles.Add(outputpath);
                    images.First().Save(outputpath);
                }
            }

            return imagefiles;
        }

        public void Combine(List<string> inputfiles)
        {
            throw new NotImplementedException();
        }

        public void ConvertToScanned(string filename)
        {
            throw new NotImplementedException();
        }

        public int GetPageCount(string inputfile)
        {
            throw new NotImplementedException();
        }

        public void ImageToPdf(List<string> inputfiles, string outputlocation = "")
        {
            throw new NotImplementedException();
        }

        public bool IsXFA(string inputfile)
        {
            throw new NotImplementedException();
        }

        public void Merge(List<string> inputfiles)
        {
            throw new NotImplementedException();
        }

        public void PdfToImage(string inputfile)
        {
            throw new NotImplementedException();
        }

        public void PrintPdf(string inputfile)
        {
            throw new NotImplementedException();
        }

        public void Rotate(string inputfile, float rotation)
        {
            throw new NotImplementedException();
        }

        public void Split(string inputfile)
        {
            throw new NotImplementedException();
        }
    }
}
