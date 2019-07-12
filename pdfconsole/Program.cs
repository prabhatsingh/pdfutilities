using CommonUtilities;
using CommonUtilities.Models;
using System;
using System.Linq;
using System.Reflection;

namespace PdfConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrintTest(args);

            Console.Title = "Pdf Utilities by Prabhat Singh";

            ShellHelper.AddMenuItem();

            if (args.Length == 0)
                ShowWelcomeMessage();
            else
            {
                ActionUtilities.ActionInfo actinf = PrepareAction(args);
                PerformAction(actinf);

                ConsoleUtilities.PrintLine("Job completed successfully!", ConsoleColor.Green);
                System.Threading.Timer closetimer = new System.Threading.Timer(Closeapp, null, 2000, 5000);
            }

            Console.ReadKey();
        }

        //private static void PrintTest(string[] args) => PdfLibrary.PdfiumHelper.PrintPdfToXps(args.ToList().First());

        private static void PerformAction(ActionUtilities.ActionInfo actinf)
        {
            if (actinf.ActionTargetType == FileType.PDF)
            {
                actinf.ActionTarget.ForEach(file =>
                {
                    if (PdfLibrary.ITextHelper.IsXFA(file.Filepath))
                    {
                        ConsoleUtilities.PrintLine("The file {0} contains forms, printing it to XPS", ConsoleColor.Green, file.Filename);
                        PdfLibrary.AdobeHelper.PrintPdf(file.Filepath);
                    }
                });

                actinf.ActionTarget.RemoveAll(f => PdfLibrary.ITextHelper.IsXFA(f.Filepath));

                switch (actinf.ActionType)
                {
                    case ActionType.PDFTOIMAGE:
                        actinf.ActionTarget.ForEach(file => PdfLibrary.GhostScriptHelper.PdfToImage(file.Filepath));
                        break;
                    case ActionType.SPLIT:
                        actinf.ActionTarget.ForEach(file => PdfLibrary.ITextHelper.Split(file.Filepath));
                        break;
                    case ActionType.ROTATECW:
                    case ActionType.ROTATECCW:
                    case ActionType.ROTATE180:
                        actinf.ActionTarget.ForEach(file => PdfLibrary.ITextHelper.Rotate(file.Filepath, (int)actinf.ActionType));
                        break;
                    case ActionType.MERGE:
                        PdfLibrary.ITextHelper.Merge(actinf.ActionTarget.Select(f => f.Filepath).ToList());
                        break;
                }
            }
            else if (actinf.ActionTargetType == FileType.IMAGE)
            {
                switch (actinf.ActionType)
                {
                    case ActionType.IMAGETOPDF:
                        PdfLibrary.ITextHelper.ImageToPdf(actinf.ActionTarget.Select(f => f.Filepath).ToList());
                        break;
                    case ActionType.OPTIMIZE:
                        actinf.ActionTarget.ForEach(file => ImageLibrary.ImageProcessorHelper.Optimize(file.Filepath, actinf.resolution));
                        break;
                    case ActionType.ROTATECW:
                    case ActionType.ROTATECCW:
                    case ActionType.ROTATE180:
                        actinf.ActionTarget.ForEach(file => ImageLibrary.ImageProcessorHelper.Rotate(file.Filepath, (float)actinf.ActionType));
                        break;
                }
            }
            else if (actinf.ActionTargetType == FileType.XPS)
            {
                switch (actinf.ActionType)
                {
                    case ActionType.XPSTOIMAGE:
                        actinf.ActionTarget.ForEach(file => XpsLibrary.XpsHelper.XpsToImage(file.Filepath));
                        break;
                    case ActionType.XPSTOPDF:
                        actinf.ActionTarget.ForEach(file => PdfLibrary.ITextHelper.ImageToPdf(XpsLibrary.XpsHelper.XpsToImage(file.Filepath)));
                        break;
                }
            }
            else if (actinf.ActionTargetType == FileType.COMBINED)
            {
                switch (actinf.ActionType)
                {
                    case ActionType.COMBINE:
                        PdfLibrary.ITextHelper.Combine(actinf.ActionTarget.Select(f => f.Filepath).ToList());
                        break;
                }
            }
        }

        private static ActionUtilities.ActionInfo PrepareAction(string[] args)
        {
            var actionInfo = new ActionUtilities.ActionInfo();
            args.ToList().ForEach(f => actionInfo.ActionTarget.Add(new FileUtilities.FileDetails(f)));
            actionInfo.AskUser();
            return actionInfo;
        }

        private static void Closeapp(object state)
        {
            Environment.Exit(0);
        }

        private static void ShowWelcomeMessage()
        {
            string AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string AssemblyTitle = Assembly.GetExecutingAssembly().GetName().Name;

            Console.WriteLine();
            ConsoleUtilities.PrintLine($"{AssemblyTitle} {AssemblyVersion}\nCopyright © {DateTime.Now.Year} Prabhat Singh\n", ConsoleColor.Green);
            ConsoleUtilities.PrintLine("Select pdf or image files in explorer and click on \nthe Pdf Utilities link in context menu's send to!!", ConsoleColor.Yellow);
        }
    }
}
