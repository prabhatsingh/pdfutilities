using Libraries.CommonUtilities.Models;
using System;
using System.IO;
using System.Linq;

namespace Libraries.CommonUtilities
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

        /// <summary>
        /// Generates the output path based on provided parameters
        /// </summary>
        /// <param name="inputfile"></param>
        /// <param name="acttype"></param>
        /// <param name="isTemp">If true, generates the output path to system temporary directory</param>
        /// <param name="formatChange"></param>
        /// <param name="newExtension">The new extension with preceding '.'</param>
        /// <param name="additionalData">Any text to be added at the end of the filename</param>
        /// <param name="outputNameFormat"></param>
        /// <param name="hasMultipleOutput"></param>
        /// <returns></returns>
        public static string GetOutputPath(this string inputfile, ActionType acttype, bool isTemp = false, bool formatChange = false, string newExtension = "", string additionalData = "", string outputNameFormat = "", bool hasMultipleOutput = false)
        {
            var currentdirectory = isTemp ? Path.GetTempPath() : Path.GetDirectoryName(inputfile);
            var filename = string.IsNullOrEmpty(outputNameFormat) ? Path.GetFileNameWithoutExtension(inputfile) : outputNameFormat;

            var action = Enum.GetName(acttype.GetType(), acttype).ToLower();
            var extension = Path.GetExtension(inputfile);

            if (formatChange)
                extension = newExtension;

            if (hasMultipleOutput)
            {
                var outdir = $"{currentdirectory}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(inputfile)}{Path.DirectorySeparatorChar}{filename}_{action}_{additionalData}{extension}";
                if (!Directory.Exists(Path.GetDirectoryName(outdir)))
                    Directory.CreateDirectory(Path.GetDirectoryName(outdir));

                return outdir;
            }
            else
                return $"{currentdirectory}{Path.DirectorySeparatorChar}{filename}_{action}_{additionalData}{extension}";
        }
    }
}
