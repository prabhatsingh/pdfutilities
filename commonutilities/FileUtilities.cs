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

        public class FileDetails
        {
            private string _filepath;

            public FileDetails(string f)
            {
                _filepath = f;
            }

            public string Filename
            {
                get
                {
                    return Path.GetFileNameWithoutExtension(_filepath);
                }
            }

            public string Filepath
            {
                get
                {
                    return _filepath;
                }
            }

            public string Extension
            {
                get
                {
                    return Path.GetExtension(_filepath);
                }
            }

            public FileType FType
            {
                get
                {
                    if (Extension.IsImage()) return FileType.IMAGE;
                    if (Extension.IsPdf()) return FileType.PDF;
                    return FileType.UNKNOWN;
                }
            }
        }
    }
}
