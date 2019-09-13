using System.Collections.Generic;

namespace Libraries.CommonUtilities.Interfaces
{
    public interface IImageActions
    {
        System.Drawing.Image Optimize(string inputfile, int finalsize = 1024, bool retobj = false, float hdpi = 0, float vdpi = 0);
        System.Drawing.Image Rotate(string inputfile, float rotation, bool retobj = false);
        void ImageToPdf(List<string> inputfiles, string outputlocation = "");
        void ConvertToScanned(string filename);
    }
}
