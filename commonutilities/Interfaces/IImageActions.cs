using System.Collections.Generic;

namespace Libraries.CommonUtilities.Interfaces
{
    public interface IImageActions
    {
        void Optimize(string inputfile, int finalsize = 1024);
        void Rotate(string inputfile, float rotation);
        void ImageToPdf(List<string> inputfiles, string outputlocation = "");
        void ConvertToScanned(string filename);
    }
}
