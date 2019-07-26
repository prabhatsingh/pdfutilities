using Libraries.CommonUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Libraries.CommonUtilities
{
    public class ActionUtilities
    {
        public class ActionInfo
        {
            public int resolution;

            public ActionInfo()
            {
                ActionTarget = new List<FileUtilities.FileDetails>();
            }

            public ActionType ActionType { get; set; }

            public FileType ActionTargetType
            {
                get
                {
                    if (ActionTarget.Select(f => f.FType).Distinct().Count() == 1)
                        return ActionTarget.First().FType;
                    else
                        return FileType.COMBINED;
                }
            }

            public List<FileUtilities.FileDetails> ActionTarget { get; set; }

            public void AskUser()
            {
                Dictionary<ActionType, string> options = new Dictionary<ActionType, string>();

                switch (ActionTargetType)
                {
                    case FileType.PDF:
                        options.Add(ActionType.PDFTOIMAGE, "Convert pdf page(s) to image(s)");
                        options.Add(ActionType.SPLIT, "Split pdf file(s)");
                        options.Add(ActionType.ROTATE, "Rotate pdf file(s)");
                        options.Add(ActionType.LOOKSCANNED, "Make pdf file(s) look scanned");
                        if (ActionTarget.Count > 1) options.Add(ActionType.MERGE, "Merge pdf files");
                        break;
                    case FileType.IMAGE:
                        options.Add(ActionType.IMAGETOPDF, "Combine image(s) to pdf");
                        options.Add(ActionType.OPTIMIZE, "Optimize image(s)");
                        options.Add(ActionType.ROTATE, "Rotate image(s)");
                        break;
                    case FileType.COMBINED:
                        options.Add(ActionType.COMBINE, "Combine images & pdf page(s) into a new pdf file");
                        break;
                    case FileType.XPS:
                        options.Add(ActionType.XPSTOIMAGE, "Convert xps page(s) to image(s)");
                        options.Add(ActionType.XPSTOPDF, "Convert xps files to pdf files");
                        break;
                    case FileType.UNSUPPORTED:
                        break;
                }

                ActionType = ParseUserInput(options);

                ConsoleUtilities.PrintLine($"Selected Option Is: {Enum.GetName(ActionType.GetType(), ActionType)}", ConsoleColor.DarkGray);

                if (ActionType == ActionType.ROTATE)
                {
                    ConsoleUtilities.PrintLine("Select rotation type", ConsoleColor.Yellow);

                    options.Clear();
                    options.Add(ActionType.ROTATECW, "Rotate 90° clockwise");
                    options.Add(ActionType.ROTATECCW, "Rotate 90° counter clockwise");
                    options.Add(ActionType.ROTATE180, "Rotate 180°");

                    ActionType = ParseUserInput(options);

                    ConsoleUtilities.PrintLine($"Selected Rotation Option Is: {(int)ActionType}", ConsoleColor.DarkGray);
                }

                if (ActionType == ActionType.OPTIMIZE)
                {
                    ConsoleUtilities.Print("Select final resolution: ", ConsoleColor.Yellow);
                    options.Clear();

                    resolution = ParseUserInput(options);
                    ConsoleUtilities.PrintLine($"Selected resolution is: {resolution}", ConsoleColor.DarkGray);
                }
            }

            private dynamic ParseUserInput(Dictionary<ActionType, string> options)
            {
                if (options.Count != 0)
                    ConsoleUtilities.PrintOptions(options.Values.ToArray(), ConsoleColor.Yellow);

                var userinput = Console.ReadLine();

                var enteredValue = Convert.ToInt32(string.IsNullOrEmpty(userinput) ? "0" : userinput);

                if (options.Count != 0)
                    return options.ElementAt(enteredValue - 1).Key;
                else
                    return enteredValue;
            }
        }
    }
}
