using System.Linq;

namespace PdfLibrary
{
    public class AdobeHelper
    {
        public static void PrintPdf(string inputfilename)
        {
            string acropath = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\AcroRd32.exe").GetValue("").ToString();
            var printer = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().ToList().Find(f => f.Contains("XPS"));                        
            System.Diagnostics.Process.Start(acropath, string.Format("/h /t \"{0}\" \"{1}\"", inputfilename, printer));
        }
    }
}
