using Libraries.CommonUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XpsLibrary
{
    public class XpsHelper
    {
        public static List<string> XpsToImage(string inputfile, bool istemp = false)
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
    }
}
