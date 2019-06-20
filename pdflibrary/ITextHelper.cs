using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace PdfLibrary
{
    public class ITextHelper
    {
        public static void Split(string inputfile)
        {
            var currentdirectory = Path.GetDirectoryName(inputfile);
            var filename = Path.GetFileNameWithoutExtension(inputfile);

            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(outputpath);

            using (PdfReader reader = new PdfReader(inputfile))
            {
                int NumPages = reader.NumberOfPages;

                int digitN = NumPages.ToString().Length;

                for (int i = 1; i <= NumPages; i++)
                {
                    string outFile = string.Format("{0}{1}_Page {2:D" + digitN + "}.pdf", outputpath, filename, i);
                    FileStream stream = new FileStream(outFile, FileMode.Create);

                    Document doc = new Document();
                    PdfCopy pdf = new PdfCopy(doc, stream);

                    doc.Open();
                    PdfImportedPage page = pdf.GetImportedPage(reader, i);
                    pdf.AddPage(page);

                    pdf.Dispose();
                    doc.Dispose();
                    stream.Dispose();
                    Console.WriteLine("Generated {0}", outFile);
                }
            }
        }

        public static void Merge(List<string> inputfiles)
        {
            var currentdirectory = Path.GetDirectoryName(inputfiles.First());
            var filename = Path.GetFileNameWithoutExtension(inputfiles.First());

            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + "_Merged" + Path.GetExtension(inputfiles.First());

            FileStream stream = null;
            Document doc = null;
            PdfCopy pdf = null;

            try
            {
                stream = new FileStream(outputpath, FileMode.Create);
                doc = new Document();
                pdf = new PdfCopy(doc, stream);

                doc.Open();

                foreach (string file in inputfiles)
                {
                    pdf.AddDocument(new iTextSharp.text.pdf.PdfReader(file));
                }
                
                Console.WriteLine("Merged {0} into {1}", inputfiles.Count, Path.GetFileNameWithoutExtension(outputpath));
            }
            catch (Exception)
            {
            }
            finally
            {
                pdf?.Dispose();
                doc?.Dispose();
                stream?.Dispose();
            }
        }
        
        public static void ImageToPdf(List<string> inputfiles)
        {
            var currentdirectory = Path.GetDirectoryName(inputfiles.First());
            var filename = Path.GetFileNameWithoutExtension(inputfiles.First());

            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + "_Merged.pdf";

            Document doc = null;
            FileStream fs = null;
            PdfWriter writer = null;

            try
            {
                doc = new Document();
                fs = new FileStream(outputpath, FileMode.Create, FileAccess.Write, FileShare.None);
                writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                foreach (var imgf in inputfiles)
                {
                    Image img = iTextSharp.text.Image.GetInstance(imgf);
                    
                    doc.SetPageSize(new iTextSharp.text.Rectangle(0, 0, img.Width, img.Height, 0));
                    doc.NewPage();

                    img.SetAbsolutePosition(0, 0);
                    writer.DirectContent.AddImage(img);
                }
                
                Console.WriteLine("Merged {0} images into {1}", inputfiles.Count, Path.GetFileNameWithoutExtension(outputpath));
            }
            catch (Exception)
            {

            }
            finally
            {
                doc?.Dispose();
                fs?.Dispose();
                writer?.Dispose();
            }
        }

        public static void Rotate(string inputfile, int desiredRot)
        {
            var currentdirectory = Path.GetDirectoryName(inputfile);
            var filename = Path.GetFileNameWithoutExtension(inputfile);

            var outputpath = currentdirectory + Path.DirectorySeparatorChar + filename + "_Rotated" + Path.GetExtension(inputfile);

            using (FileStream outStream = new FileStream(outputpath, FileMode.Create))
            {
                PdfReader reader = new PdfReader(inputfile);
                PdfStamper stamper = new PdfStamper(reader, outStream);

                int pageCount = reader.NumberOfPages;

                for (int n = 1; n <= pageCount; n++)
                {
                    PdfDictionary pageDict = reader.GetPageN(n);

                    PdfNumber rotation = pageDict.GetAsNumber(PdfName.ROTATE);

                    if (rotation != null)
                    {
                        desiredRot += rotation.IntValue;
                        desiredRot %= 360; // must be 0, 90, 180, or 270
                    }
                    pageDict.Put(PdfName.ROTATE, new PdfNumber(desiredRot));
                }

                stamper.Close();
                reader.Close();
                
                Console.WriteLine("Rotated {0} pages of {1}", pageCount, filename);
            }
        }
    }
}
