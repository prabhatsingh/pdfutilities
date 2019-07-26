using System.Collections.Generic;

namespace Libraries.CommonUtilities.Interfaces
{
    public interface IPdfActions
    {
        void Rotate(string inputfile, float rotation);
        void PrintPdf(string inputfile);
        void PdfToImage(string inputfile);
        int GetPageCount(string inputfile);
        void Split(string inputfile);
        void Merge(List<string> inputfiles);
        void Combine(List<string> inputfiles);
        void ImageToPdf(List<string> inputfiles, string outputlocation = "");
        bool IsXFA(string inputfile);
        void ConvertToScanned(string filename);
        List<string> XpsToImage(string inputfile, bool istemp = false);
    }
}
