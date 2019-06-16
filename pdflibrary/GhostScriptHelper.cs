using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;
using System;
using System.IO;
using System.Reflection;

namespace PdfLibrary
{
    public class GhostScriptHelper
    {
        public static void PdfToImage(string inputfile)
        {
            GhostscriptVersionInfo _lastInstalledVersion = GetGhostscriptVersion();

            int pageCount = GetPageCount(inputfile);

            var currentdirectory = Path.GetDirectoryName(inputfile);
            var filename = Path.GetFileNameWithoutExtension(inputfile);

            var outputpath = currentdirectory + Path.DirectorySeparatorChar;

            if (pageCount > 1)
                outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + Path.DirectorySeparatorChar;

            if (!Directory.Exists(outputpath))
                Directory.CreateDirectory(outputpath);

            GhostscriptPngDevice img = new GhostscriptPngDevice
            {
                GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                ResolutionXY = new GhostscriptImageDeviceResolution(1200, 1200),
                PostScript = string.Empty
            };

            img.InputFiles.Add(inputfile);

            for (int i = 1; i <= pageCount; i++)
            {
                img.Pdf.FirstPage = i;
                img.Pdf.LastPage = i;

                var outputfilename = outputpath + string.Format("{0}_Page_{1}.jpeg", Path.GetFileNameWithoutExtension(inputfile), i);

                if (File.Exists(outputfilename))
                    File.Delete(outputfilename);

                img.OutputPath = outputfilename;
                img.Process(_lastInstalledVersion, false, null);
            }
        }

        public static int GetPageCount(string inputfile)
        {            
            using (var _rasterizer = new GhostscriptRasterizer())
            {
                _rasterizer.Open(inputfile, GetGhostscriptVersion(), false);
                return _rasterizer.PageCount;
            }
        }

        private static GhostscriptVersionInfo GetGhostscriptVersion()
        {
            var dllPath = Path.Combine(GetBinPath(), Environment.Is64BitProcess ? "gsdll64.dll" : "gsdll32.dll");
            var dllResourcePath = Path.Combine(GetBinPath(),"Resources", Environment.Is64BitProcess ? "gsdll64.dll" : "gsdll32.dll");

            if (File.Exists(dllPath))
            {
                // use DLL in bin folder
                return new GhostscriptVersionInfo(new System.Version(0, 0, 0), dllPath, string.Empty, GhostscriptLicense.GPL | GhostscriptLicense.AFPL);
            }
            else if(File.Exists(dllResourcePath))
            {
                // use DLL in Resources folder
                return new GhostscriptVersionInfo(new System.Version(0, 0, 0), dllResourcePath, string.Empty, GhostscriptLicense.GPL | GhostscriptLicense.AFPL);
            }
            else
            {
                // try to use installed DLL
                return GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);
            }
        }

        private static string GetBinPath()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;

            UriBuilder uri = new UriBuilder(codeBase);

            string path = Uri.UnescapeDataString(uri.Path);

            return Path.GetDirectoryName(path);
        }
    }
}
