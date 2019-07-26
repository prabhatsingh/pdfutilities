using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Libraries.CommonUtilities;

namespace PdfLibrary
{
    public class ITextHelper : Libraries.CommonUtilities.Interfaces.IPdfActions
    {
        public void Rotate(string inputfile, float desiredRot)
        {
            var outputpath = FileUtilities.GetOutputPath(inputfile, Libraries.CommonUtilities.Models.ActionType.ROTATE, additionalData: desiredRot.ToString());

            FileStream outStream = new FileStream(outputpath, FileMode.Create);

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

            Console.WriteLine("Rotated {0} pages of {1}", pageCount, Path.GetFileName(outputpath));
        }

        public void PrintPdf(string inputfile)
        {
            throw new NotImplementedException();
        }

        public void PdfToImage(string inputfile)
        {
            throw new NotImplementedException();
        }

        public int GetPageCount(string inputfile)
        {
            using (PdfReader reader = new PdfReader(inputfile))
            {
                return reader.NumberOfPages;
            }
        }

        public void Split(string inputfile)
        {
            using (PdfReader reader = new PdfReader(inputfile))
            {
                int pagecount = reader.NumberOfPages;

                if (pagecount == 1)
                {
                    Console.WriteLine("File has only one page, splitting abandoned!");
                    return;
                }

                var outputpath = FileUtilities.GetOutputPath(inputfile, Libraries.CommonUtilities.Models.ActionType.SPLIT, outputNameFormat: "{0}_Page {1}", hasMultipleOutput: true);

                for (int i = 1; i <= pagecount; i++)
                {
                    string outFile = string.Format(outputpath, Path.GetFileNameWithoutExtension(inputfile), i);

                    FileStream stream = new FileStream(outFile, FileMode.Create);

                    Document doc = new Document();
                    PdfCopy pdf = new PdfCopy(doc, stream);

                    doc.Open();
                    PdfImportedPage page = pdf.GetImportedPage(reader, i);
                    pdf.AddPage(page);

                    pdf.Dispose();
                    doc.Dispose();

                    Console.WriteLine("Generated {0}", Path.GetFileName(outFile));
                }
            }
        }

        public void Merge(List<string> inputfiles)
        {
            var outputpath = FileUtilities.GetOutputPath(inputfiles.First(), Libraries.CommonUtilities.Models.ActionType.MERGE);

            Document doc = null;
            PdfSmartCopy pdf = null;

            try
            {
                var stream = new FileStream(outputpath, FileMode.Create);

                doc = new Document();
                pdf = new PdfSmartCopy(doc, stream);

                doc.Open();

                foreach (string file in inputfiles)
                {
                    pdf.AddDocument(new PdfReader(file));
                }

                Console.WriteLine("Merged {0} into {1}", inputfiles.Count, Path.GetFileName(outputpath));
            }
            catch (Exception)
            {
            }
            finally
            {
                pdf?.Dispose();
                doc?.Dispose();
            }
        }

        public void Combine(List<string> inputfiles)
        {
            Console.WriteLine("In Progress, won't work properly");

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

                inputfiles.Sort();
                foreach (var file in inputfiles)
                {
                    if (Path.GetExtension(file) == ".pdf")
                    {
                        doc.NewPage();
                        PdfReader reader = new PdfReader(file);
                        PdfImportedPage page = writer.GetImportedPage(reader, 1);

                        //cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(1).Height);
                        //writer.DirectContent.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSi);
                    }
                    else
                    {
                        Image img = iTextSharp.text.Image.GetInstance(file);
                        img.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
                        img.SetAbsolutePosition((PageSize.A4.Width - img.ScaledWidth) / 2, (PageSize.A4.Height - img.ScaledHeight) / 2);

                        doc.NewPage();
                        writer.DirectContent.AddImage(img);
                    }
                }

                Console.WriteLine("Merged {0} images into {1}", inputfiles.Count, Path.GetFileName(outputpath));
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

        public void ImageToPdf(List<string> inputfiles, string outputlocation = "")
        {
            var outputpath = FileUtilities.GetOutputPath(string.IsNullOrEmpty(outputlocation) ? inputfiles.First() : outputlocation, Libraries.CommonUtilities.Models.ActionType.IMAGETOPDF, formatChange: true, newExtension: ".pdf");

            Document doc = null;
            FileStream fs = null;
            PdfWriter writer = null;

            try
            {
                doc = new Document();
                fs = new FileStream(outputpath, FileMode.Create, FileAccess.Write, FileShare.None);
                writer = PdfWriter.GetInstance(doc, fs);

                doc.Open();

                inputfiles.Sort();
                foreach (var imgf in inputfiles)
                {
                    Image img = iTextSharp.text.Image.GetInstance(imgf);
                    img.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
                    img.SetAbsolutePosition((PageSize.A4.Width - img.ScaledWidth) / 2, (PageSize.A4.Height - img.ScaledHeight) / 2);

                    doc.NewPage();
                    writer.DirectContent.AddImage(img);
                }

                Console.WriteLine("Merged {0} images into {1}", inputfiles.Count, Path.GetFileName(outputpath));
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

        public bool IsXFA(string inputfile)
        {
            return new XfaForm(new PdfReader(inputfile)).XfaPresent;
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
