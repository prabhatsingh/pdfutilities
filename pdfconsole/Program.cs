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
                PerformAction(actinf);

                ConsoleUtilities.PrintLine("Job completed successfully!", ConsoleColor.Green);
                System.Threading.Timer closetimer = new System.Threading.Timer(Closeapp, null, 2000, 5000);
            }

            Console.ReadKey();
        }

        private static void PerformAction(ActionUtilities.ActionInfo actinf)
        {
            if (actinf.actionTargetType == FileType.PDF)
            {
                switch (actinf.actionType)
                {
                    case ActionType.PDFTOIMAGE:
                        actinf.actionTarget.ForEach(file => PdfLibrary.GhostScriptHelper.PdfToImage(file.Filepath));
                        break;
                    case ActionType.SPLIT:
                        actinf.actionTarget.ForEach(file => PdfLibrary.ITextHelper.Split(file.Filepath));
                        break;
                    case ActionType.ROTATECW:
                    case ActionType.ROTATECCW:
                    case ActionType.ROTATE180:
                        actinf.actionTarget.ForEach(file => PdfLibrary.ITextHelper.Rotate(file.Filepath, (int)actinf.actionType));
                        break;
                    case ActionType.MERGE:
                        PdfLibrary.ITextHelper.Merge(actinf.actionTarget.Select(f => f.Filepath).ToList());
                        break;
                }
            }
            else if (actinf.actionTargetType == FileType.IMAGE)
            {
                switch (actinf.actionType)
                {
                    case ActionType.IMAGETOPDF:
                        PdfLibrary.ITextHelper.ImageToPdf(actinf.actionTarget.Select(f => f.Filepath).ToList());
                        break;
                    case ActionType.OPTIMIZE:
                        actinf.actionTarget.ForEach(file => ImageLibrary.ImageProcessorHelper.Optimize(file.Filepath, actinf.resolution));
                        break;
                    case ActionType.ROTATECW:
                    case ActionType.ROTATECCW:
                    case ActionType.ROTATE180:
                        actinf.actionTarget.ForEach(file => ImageLibrary.ImageProcessorHelper.Rotate(file.Filepath, (float)actinf.actionType));
                        break;
                }
            }
        }

        private static ActionUtilities.ActionInfo PrepareAction(string[] args)
        {
            var actionInfo = new ActionUtilities.ActionInfo();
            args.ToList().ForEach(f => actionInfo.actionTarget.Add(new FileUtilities.FileDetails(f)));
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{AssemblyTitle} {AssemblyVersion}\nCopyright(C) {DateTime.Now.Year} Prabhat Singh\n");
            Console.ResetColor();
            Console.WriteLine("Select pdf or image files in explorer and click on the Pdf Utilities link in context menu's send to!!");
        }
    }
}
