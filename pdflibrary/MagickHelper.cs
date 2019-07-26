﻿using ImageMagick;
using System.Collections.Generic;
using System.IO;

namespace PdfLibrary
{
    public class MagickHelper : Libraries.CommonUtilities.Interfaces.IPdfActions
    {
        public void Combine(List<string> inputfiles)
        {
            throw new System.NotImplementedException();
        }

        public void ConvertToScanned(string filename)
        {
            MagickReadSettings settings = new MagickReadSettings
            {
                Density = new Density(300, 300)
            };

            MagickNET.SetGhostscriptDirectory(Path.GetDirectoryName(GhostScriptHelper.GetGhostscriptVersion().DllPath));

            List<string> scannedimgfiles = new List<string>();

            using (MagickImageCollection images = new MagickImageCollection())
            {
                images.Read(filename, settings);

                int page = 1;

                foreach (MagickImage image in images)
                {
                    image.ColorSpace = ColorSpace.Gray;
                    var clone = image.Clone();
                    clone.Blur(0, 1);
                    clone.Compose = CompositeOperator.DivideDst;
                    image.Composite(clone);
                    image.LinearStretch(new Percentage(5), new Percentage(0));
                    image.Rotate(0.2);

                    var tempimgpath = Path.GetTempPath() + Path.GetFileNameWithoutExtension(filename) + "_img_" + page + ".png";
                    image.Write(tempimgpath);
                    scannedimgfiles.Add(tempimgpath);

                    page++;
                }
            }

            new ITextHelper().ImageToPdf(scannedimgfiles, filename);

            scannedimgfiles.ForEach(f => File.Delete(f));
        }

        public int GetPageCount(string inputfile)
        {
            throw new System.NotImplementedException();
        }

        public void ImageToPdf(List<string> inputfiles, string outputlocation = "")
        {
            throw new System.NotImplementedException();
        }

        public bool IsXFA(string inputfile)
        {
            throw new System.NotImplementedException();
        }

        public void Merge(List<string> inputfiles)
        {
            throw new System.NotImplementedException();
        }

        public void PdfToImage(string inputfile)
        {
            throw new System.NotImplementedException();
        }

        public void PrintPdf(string inputfile)
        {
            throw new System.NotImplementedException();
        }

        public void Rotate(string inputfile, float rotation)
        {
            throw new System.NotImplementedException();
        }

        public void Split(string inputfile)
        {
            throw new System.NotImplementedException();
        }

        public List<string> XpsToImage(string inputfile, bool istemp = false)
        {
            throw new System.NotImplementedException();
        }
    }
}
