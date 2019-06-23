using CommonUtilities;
using CommonUtilities.Models;
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
                ShowWelcomeMessage();
            else
            {
                ActionUtilities.ActionInfo actinf = PrepareAction(args);
                PerformActions(args);

                ConsoleUtilities.PrintLine("Job completed successfully!", ConsoleColor.Green);
                System.Threading.Timer closetimer = new System.Threading.Timer(Closeapp, null, 2000, 5000);
            }

            Console.ReadKey();
        }

        private static ActionUtilities.ActionInfo PrepareAction(string[] args)
        {
            var actionInfo = new ActionUtilities.ActionInfo();
            args.ToList().ForEach(f => actionInfo.actionTarget.Add(new FileUtilities.FileDetails(f)));

            //DETERMINE FILETYPE
            var fileType = actionInfo.actionTargetType;
            //ASK USER FOR ACTIONS
            //DETERMINE ACTION SELECTED
            //PERFORM ACTION
            return null;
        }

        private static void Closeapp(object state)
        {
            Environment.Exit(0);
        }

        private static void PerformActions(string[] args)
        {
            if (args.Length == 1)
            {
                var file = args.First();
                if (Path.GetExtension(file).IsPdf())
                {
                    var choice = GetUserChoice("s", "p", args);

                    if (choice == 1)
                        PdfLibrary.GhostScriptHelper.PdfToImage(file);
                    else if (choice == 2)
                        PdfLibrary.ITextHelper.Split(file);
                    else if (choice == 3)
                    {
                        choice = GetUserChoice("s", "r", args);

                        if (choice == 1)
                            PdfLibrary.ITextHelper.Rotate(file, 90);
                        else if (choice == 2)
                            PdfLibrary.ITextHelper.Rotate(file, 270);
                        else if (choice == 3)
                            PdfLibrary.ITextHelper.Rotate(file, 180);
                    }
                }
                else if (Path.GetExtension(file).IsImage())
                {
                    var choice = GetUserChoice("s", "i", args);

                    if (choice == 1)
                        PdfLibrary.ITextHelper.ImageToPdf(args.ToList());
                    else if (choice == 2)
                    {
                        choice = GetUserChoice("s", "o", args);

                        if (choice != 0)
                            ImageLibrary.ImageProcessorHelper.Optimize(file, choice);
                        else
                            ImageLibrary.ImageProcessorHelper.Optimize(file);
                    }
                    else if (choice == 3)
                    {
                        choice = GetUserChoice("s", "r", args);

                        if (choice == 1)
                            ImageLibrary.ImageProcessorHelper.Rotate(file, 90);
                        else if (choice == 2)
                            ImageLibrary.ImageProcessorHelper.Rotate(file, 270);
                        else if (choice == 3)
                            ImageLibrary.ImageProcessorHelper.Rotate(file, 180);
                    }
                }
            }
            else
            {
                var pdfs = new List<string>();
                var imgs = new List<string>();

                foreach (var file in args.ToList())
                {
                    if (Path.GetExtension(file).IsPdf())
                        pdfs.Add(file);
                    else if (Path.GetExtension(file).IsImage())
                        imgs.Add(file);
                }

                if (pdfs.Count > 0)
                {
                    var choice = GetUserChoice("m", "p", args);

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
                        choice = GetUserChoice("m", "r", args);

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
                    var choice = GetUserChoice("m", "i", args);

                    if (choice == 1)
                        PdfLibrary.ITextHelper.ImageToPdf(imgs);
                    else if (choice == 2)
                    {
                        choice = GetUserChoice("m", "o", args);

                        if (choice != 0)
                            imgs.ForEach(f => ImageLibrary.ImageProcessorHelper.Optimize(f, choice));
                        else
                            imgs.ForEach(f => ImageLibrary.ImageProcessorHelper.Optimize(f));
                    }
                    else if (choice == 3)
                    {
                        choice = GetUserChoice("m", "r", args);

                        if (choice == 1)
                            imgs.ForEach(f => ImageLibrary.ImageProcessorHelper.Rotate(f, 90));
                        else if (choice == 2)
                            imgs.ForEach(f => ImageLibrary.ImageProcessorHelper.Rotate(f, 270));
                        else if (choice == 3)
                            imgs.ForEach(f => ImageLibrary.ImageProcessorHelper.Rotate(f, 180));
                    }
                }
            }
        }

        private static int GetUserChoice(string ms, string irp, string[] args)
        {
            if (irp == "r")
                Console.WriteLine("Select rotation type");
            else if (irp == "o")
                Console.Write("Provide the required resolution: ");
            else
                Console.WriteLine("What do you want to do?");

            string[] options;

            switch (ms + irp)
            {
                case "s" + "p":
                    options = new string[] {
                        "Convert pdf pages to images",
                        "Split Pdf File",
                        "Rotate Pdf file"
                    };
                    break;
                case "m" + "p":
                    options = new string[] {
                        "Convert pdf files to images",
                        "Merge Pdf Files",
                        "Split Pdf Files with multiple pages",
                        "Rotate Pdf files"
                    };
                    break;
                case "s" + "i":
                    options = new string[] {
                        "Convert image to pdf",
                        "Compress Image",
                        "Rotate Image"
                    };
                    break;
                case "m" + "i":
                    options = new string[] {
                        "Combine images in one pdf",
                        "Compress Images",
                        "Rotate Images"
                    };
                    break;
                case "m" + "r":
                case "s" + "r":
                    options = new string[] {
                        "Rotate clockwise",
                        "Rotate counter clockwise",
                        "Rotate 180 degrees"
                    };
                    break;
                default:
                    options = new string[0];
                    break;
            }

            if (options.Length != 0)
                ConsoleUtilities.PrintOptions(options, ConsoleColor.DarkYellow);

            var userinput = Console.ReadLine();

            var selectedOption = Convert.ToInt32(string.IsNullOrEmpty(userinput) ? "0" : userinput);
            ConsoleUtilities.PrintLine("Processing", ConsoleColor.DarkGray);
            return selectedOption;
        }

        private static void ShowWelcomeMessage()
        {
            string AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string AssemblyTitle = Assembly.GetExecutingAssembly().GetName().Name;

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{AssemblyTitle} {AssemblyVersion}\nCopyright(C) {DateTime.Now.Year} Prabhat Singh\n");
            Console.ResetColor();
            Console.WriteLine("Select pdf or image files in explorer and click on the Pdf Utilities link in context menu's send to!!");
        }
    }
}
