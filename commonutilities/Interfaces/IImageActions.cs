using System.Collections.Generic;

namespace Libraries.CommonUtilities.Interfaces
{
    public interface IImageActions
    {
        void Optimize(string inputfile, int finalsize = 1024);
        System.Drawing.Image Rotate(string inputfile, float rotation, bool retobj = false);
        void ImageToPdf(List<string> inputfiles, string outputlocation = "");
        void ConvertToScanned(string filename);
    }
}
