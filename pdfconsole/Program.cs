using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
                Console.Read();
            }
        }

        private static void PerformActions(string[] args)
        {
            if (args.Length == 1)
            {
                var file = args.First();
                if (CommonUtilities.FileUtilities.IsPdf(Path.GetExtension(file)))
                {
                    var choice = GetUserChoice('s', 'p', args);

                    if (choice == 1)
                        PdfLibrary.GhostScriptHelper.PdfToImage(args.First());
                    else if (choice == 2)
                        PdfLibrary.ITextHelper.Split(args.First());
                    else if (choice == 3)
                    {
                        choice = GetUserChoice('s', 'r', args);

                        if (choice == 1)
                            PdfLibrary.ITextHelper.Rotate(args.First(), 90);
                        else if (choice == 2)
                            PdfLibrary.ITextHelper.Rotate(args.First(), 270);
                        else if (choice == 3)
                            PdfLibrary.ITextHelper.Rotate(args.First(), 180);
                    }
                }
                else if (CommonUtilities.FileUtilities.IsImage(Path.GetExtension(file)))
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
                    if (CommonUtilities.FileUtilities.IsPdf(Path.GetExtension(file)))
                        pdfs.Add(file);
                    else if (CommonUtilities.FileUtilities.IsImage(Path.GetExtension(file)))
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
                    else if (choice == 4)
                    {
                        choice = GetUserChoice('m', 'r', args);

                        if (choice == 1)
                            pdfs.ForEach(f => PdfLibrary.ITextHelper.Rotate(f, 90));
                        else if (choice == 2)
                            pdfs.ForEach(f => PdfLibrary.ITextHelper.Rotate(f, 270));
                        else if (choice == 3)
                            pdfs.ForEach(f => PdfLibrary.ITextHelper.Rotate(f, 180));
                    }
                }
                else if (imgs.Count > 0)
                {
                    var choice = GetUserChoice('m', 'i', args);

                    if (choice == 1)
                        PdfLibrary.ITextHelper.ImageToPdf(imgs);
                }
            }
        }

        private static int GetUserChoice(char ms, char pir, string[] args)
        {
            if (pir == 'r')
                Console.WriteLine("Select rotation type");
            else
                Console.WriteLine("What do you want to do?");

            switch (ms + pir)
            {
                case 's' + 'p':
                    Console.WriteLine("1. Convert pdf pages to images");
                    Console.WriteLine("2. Split Pdf File");
                    Console.WriteLine("3. Rotate Pdf file");
                    break;
                case 'm' + 'p':
                    Console.WriteLine("1. Convert pdf files to images");
                    Console.WriteLine("2. Merge Pdf Files");
                    Console.WriteLine("3. Split Pdf Files with multiple pages");
                    Console.WriteLine("4. Rotate Pdf files");
                    break;
                case 's' + 'i':
                    Console.WriteLine("1. Convert image to pdf");
                    Console.WriteLine("2. Compress Image");
                    break;
                case 'm' + 'i':
                    Console.WriteLine("1. Combine images in one pdf");
                    Console.WriteLine("2. Compress Images");
                    break;
                case 'm' + 'r':
                case 's' + 'r':
                    Console.WriteLine("1. Rotate clockwise");
                    Console.WriteLine("2. Rotate counter clockwise");
                    Console.WriteLine("2. Rotate 180 degrees");
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
