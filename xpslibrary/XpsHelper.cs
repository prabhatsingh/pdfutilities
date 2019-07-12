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

            var currentdirectory = Path.GetDirectoryName(inputfile);
            var filename = Path.GetFileNameWithoutExtension(inputfile);

            var outputpath = currentdirectory + Path.DirectorySeparatorChar;
                        
            using (var xpsConverter = new Xps2Image(inputfile))
            {
                var images = xpsConverter.ToBitmap(new Parameters
                {
                    ImageType = ImageType.Png,
                    Dpi = 300
                });

                if (images.Count() > 1)
                {
                    outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + Path.DirectorySeparatorChar;

                    if (!Directory.Exists(outputpath))
                        Directory.CreateDirectory(outputpath);

                    int count = 1;
                    images.ToList().ForEach(f =>
                    {
                        var imgfilename = outputpath + string.Format("{0}_Page_{1}.png", Path.GetFileNameWithoutExtension(inputfile), count++);
                        imagefiles.Add(imgfilename);
                        f.Save(imgfilename);
                    });
                }
                else
                {
                    outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + ".png";
                    imagefiles.Add(outputpath);
                    images.First().Save(outputpath);
                }
            }

            return imagefiles;
        }        
    }
}
