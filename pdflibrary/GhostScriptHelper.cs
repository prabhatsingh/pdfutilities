using Ghostscript.NET;
using Ghostscript.NET.Processor;
using Ghostscript.NET.Rasterizer;
using Libraries.CommonUtilities;
using Libraries.CommonUtilities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PdfLibrary
{
    public class GhostScriptHelper: Libraries.CommonUtilities.Interfaces.IPdfActions
    {                                
        public static GhostscriptVersionInfo GetGhostscriptVersion()
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

        public void Rotate(string inputfile, float rotation)
        {
            throw new NotImplementedException();
        }

        public void PrintPdf(string inputfile)
        {
            GhostscriptVersionInfo _lastInstalledVersion = GetGhostscriptVersion();

            string printerName = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().ToList().Find(f => f.Contains("XPS"));

            using (GhostscriptProcessor processor = new GhostscriptProcessor(_lastInstalledVersion))
            {
                List<string> switches = new List<string>
                {
                    "-empty",
                    "-dPrinted",
                    "-dBATCH",
                    "-dNOPAUSE",
                    "-dNOSAFER",
                    "-dNumCopies=1",
                    "-sDEVICE=mswinpr2",
                    "-sOutputFile=%printer%" + printerName,
                    "-f",
                    inputfile
                };

                processor.StartProcessing(switches.ToArray(), null);
            }
        }

        public void PdfToImage(string inputfile)
        {
            GhostscriptVersionInfo _lastInstalledVersion = GetGhostscriptVersion();

            int pageCount = GetPageCount(inputfile);

            var outputpath = FileUtilities.GetOutputPath(inputfile, ActionType.PDFTOIMAGE, formatChange: true, newExtension: ".jpeg", hasMultipleOutput: pageCount > 1, outputNameFormat: "{0}_Page_{1}");

            GhostscriptPngDevice img = new GhostscriptPngDevice
            {
                GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                Resolution = 600,
                PostScript = string.Empty
            };

            img.InputFiles.Add(inputfile);

            for (int i = 1; i <= pageCount; i++)
            {
                img.Pdf.FirstPage = i;
                img.Pdf.LastPage = i;

                var outputfilename = string.Format(outputpath, Path.GetFileNameWithoutExtension(inputfile), i);

                if (File.Exists(outputfilename))
                    File.Delete(outputfilename);

                img.OutputPath = outputfilename;                
                img.Process(_lastInstalledVersion, false, null);

                Console.WriteLine("Generated {0}", Path.GetFileName(outputfilename));
            }
        }

        public int GetPageCount(string inputfile)
        {
            using (var _rasterizer = new GhostscriptRasterizer())
            {
                _rasterizer.Open(inputfile, GetGhostscriptVersion(), false);
                return _rasterizer.PageCount;
            }
        }

        public void Split(string inputfile)
        {
            throw new NotImplementedException();
        }

        public void Merge(List<string> inputfiles)
        {
            throw new NotImplementedException();
        }

        public void Combine(List<string> inputfiles)
        {
            throw new NotImplementedException();
        }

        public void ImageToPdf(List<string> inputfiles, string outputlocation = "")
        {
            throw new NotImplementedException();
        }

        public bool IsXFA(string inputfile)
        {
            throw new NotImplementedException();
        }

        public void ConvertToScanned(string filename)
        {
            throw new NotImplementedException();
        }

        public List<string> XpsToImage(string inputfile, bool istemp = false)
        {
            throw new NotImplementedException();
        }
    }
}
