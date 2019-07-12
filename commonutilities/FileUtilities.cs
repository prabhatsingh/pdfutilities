using CommonUtilities.Models;
using System.IO;
using System.Linq;

namespace CommonUtilities
{
    public static class FileUtilities
    {
        public static bool IsImage(this string extension)
        {
            return new string[] { "jpeg", "png", "jpg", "bmp", "tiff" }.Contains(extension.Trim(new char[] { '.', ' ' }).ToLowerInvariant());
        }

        public static bool IsPdf(this string extension)
        {
            return new string[] { "pdf" }.Contains(extension.Trim(new char[] { '.', ' ' }).ToLowerInvariant());
        }

        public static bool IsXps(this string extension)
        {
            return new string[] { "xps", "oxps" }.Contains(extension.Trim(new char[] { '.', ' ' }).ToLowerInvariant());
        }

        public class FileDetails
        {
            public FileDetails(string f)
            {
                Filepath = f;
            }

            public string Filename
            {
                get
                {
                    return Path.GetFileNameWithoutExtension(Filepath);
                }
            }

            public string Filepath { get; set; }

            public string Extension
            {
                get
                {
                    return Path.GetExtension(Filepath);
                }
            }

            public FileType FType
            {
                get
                {
                    if (Extension.IsImage()) return FileType.IMAGE;
                    if (Extension.IsPdf()) return FileType.PDF;
                    if (Extension.IsXps()) return FileType.XPS;
                    return FileType.UNSUPPORTED;
                }
            }
        }
    }
}
