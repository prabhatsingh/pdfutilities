using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace PdfLibrary
{
    public class PdfiumHelper
    {
        public static string PrintPdfToXps(string inputfilename)
        {
            var currentdirectory = Path.GetDirectoryName(inputfilename);
            var filename = Path.GetFileNameWithoutExtension(inputfilename);

            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + ".xps";
            /*PdfForms pfrms = new PdfForms();
            var doc = PdfDocument.Load(inputfilename, pfrms);
            
            //Initialize the SDK library    //You have to call this function before you can call any PDF processing functions.    
            PdfCommon.Initialize();

            //Gets second page from document;    
            var page = doc.Pages[0];
            //Gets page width and height measured in points. One point is 1/72 inch (around 0.3528 mm)    
            int width = (int)page.Width; int height = (int)page.Height;    //Create a bitmap    
            using (var bmp = new Bitmap(width, height))
            {        //create drawing surface        

                using (var g = Graphics.FromImage(bmp))
                {            //Render contents in a page to a drawing surface specified by a coordinate pair, a width, and a height.            
                    page.Render(g, 0, 0, width, height, Patagames.Pdf.Enums.PageRotate.Normal, Patagames.Pdf.Enums.RenderFlags.FPDF_ANNOT);
                }
            }





            var printDoc = new PdfPrintDocument(doc);
            var aa = printDoc.Document.Pages;
            PrintController printController = new StandardPrintController();
           
            printDoc.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters.Cast<string>().ToList().Find(f => f.Contains("XPS"));
            printDoc.PrinterSettings.PrintToFile = true;
            printDoc.PrinterSettings.PrintFileName = outputpath;

            printDoc.DefaultPageSettings.Landscape = false;
            printDoc.DefaultPageSettings.Color = true;

            printDoc.PrintSizeMode = PrintSizeMode.Fit;
            printDoc.PrintController = printController;
            printDoc.Print();
            //printDoc.EndPrint += PrintDoc_EndPrint;*/

            return outputpath;
        }

        private static void PrintDoc_EndPrint(object sender, PrintEventArgs e)
        {
            //GeneratePdf(images, outputpath);
        }

        public static string Flatten(string inputfilename)
        {
            var currentdirectory = Path.GetDirectoryName(inputfilename) + Path.DirectorySeparatorChar + "temp";
            Directory.CreateDirectory(currentdirectory);
            var filename = Path.GetFileNameWithoutExtension(inputfilename);
            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + ".pdf";

            var outputxpspath = PrintPdfToXps(inputfilename);

            return outputpath;
        }

        /// <summary>
        /// Generate PDF document From Multiple Images in C# using PDF Library
        /// </summary>
        public static void GeneratePdf(List<string> images, string outputpath)
        {

        }
    }
}
