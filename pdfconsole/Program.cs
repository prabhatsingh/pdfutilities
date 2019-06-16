using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PdfConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Pdf Utilities by Prabhat Singh";

            ShellHelper.AddMenuItem();

            if (args.Length == 0)
            {
                ShowWelcomeMessage();
                Console.Read();
            }
            else
            {
                PerformActions(args);
            }
        }

        private static void PerformActions(string[] args)
        {
            if (args.Length == 1)
            {
                var file = args.First();
                if (Path.GetExtension(file) == ".pdf")
                {
                    var choice = GetUserChoice('s', 'p', args);

                    if (choice == 1)
                        PdfLibrary.GhostScriptHelper.PdfToImage(args.First());
                    else if (choice == 2)
                        PdfLibrary.ITextHelper.Split(args.First());
                }
                else if (new string[] { ".jpeg", ".png", ".jpg", ".bmp", ".tiff" }.Contains(Path.GetExtension(file)))
                {
                    var choice = GetUserChoice('s', 'i', args);

                    if (choice == 1)
                        PdfLibrary.ITextHelper.ImageToPdf(args.ToList());
                }
            }
            else
            {
                var pdfs = new List<string>();
                var imgs = new List<string>();

                foreach (var file in args.ToList())
                {
                    if (Path.GetExtension(file) == ".pdf")
                        pdfs.Add(file);
                    else if (new string[] { ".jpeg", ".png", ".jpg", ".bmp", ".tiff" }.Contains(Path.GetExtension(file)))
                        imgs.Add(file);
                }

                if (pdfs.Count > 0)
                {
                    var choice = GetUserChoice('m', 'p', args);

                    if (choice == 1)
                        pdfs.ForEach(f => PdfLibrary.GhostScriptHelper.PdfToImage(f));
                    else if (choice == 2)
                        PdfLibrary.ITextHelper.Merge(pdfs);
                    else if (choice == 3)
                        pdfs.ForEach(f =>
                        {
                            if (PdfLibrary.GhostScriptHelper.GetPageCount(f) > 1) PdfLibrary.ITextHelper.Split(f);
                        });
                }
                else if (imgs.Count > 0)
                {
                    var choice = GetUserChoice('m', 'i', args);

                    if (choice == 1)
                        PdfLibrary.ITextHelper.ImageToPdf(imgs);
                }
            }
        }

        private static int GetUserChoice(char ms, char pi, string[] args)
        {
            Console.WriteLine("What do you want to do?");

            switch (ms + pi)
            {
                case 's' + 'p':
                    Console.WriteLine("1. Convert pdf pages to images");
                    Console.WriteLine("2. Split Pdf File");
                    break;
                case 'm' + 'p':
                    Console.WriteLine("1. Convert pdf files to images");
                    Console.WriteLine("2. Merge Pdf Files");
                    Console.WriteLine("3. Split Pdf Files with multiple pages");
                    break;
                case 's' + 'i':
                    Console.WriteLine("1. Convert image to pdf");
                    Console.WriteLine("2. Compress Image");
                    break;
                case 'm' + 'i':
                    Console.WriteLine("1. Combine images in one pdf");
                    Console.WriteLine("2. Compress Images");
                    break;
            }

            return Convert.ToInt32(Console.ReadLine());
        }

        private static void ShowWelcomeMessage()
        {
            string AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string AssemblyTitle = Assembly.GetExecutingAssembly().GetName().Name;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{AssemblyTitle} {AssemblyVersion}\nCopyright(C) 2019 Prabhat Singh\n");
            Console.ResetColor();
            Console.WriteLine("Select pdf or image files in explorer and click on the Pdf Utilities link in context menu's send to!!");
        }
    }
}
