using System.Linq;

namespace CommonUtilities
{
    public class FileUtilities
    {
        public static bool IsImage(string extension)
        {
            return new string[] { "BMP", "JPEG", "JPG", "PNG", "TIFF" }.Contains(extension.Trim(new char[] { ' ', '.' }).ToUpper());
        }

        public static bool IsPdf(string extension)
        {
            return new string[] { "PDF" }.Contains(extension.Trim(new char[] { ' ', '.' }).ToUpper());
        }
    }
}
