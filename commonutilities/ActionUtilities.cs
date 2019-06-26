using CommonUtilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonUtilities
{
    public class ActionUtilities
    {
        public class ActionInfo
        {
            public int resolution;

            public ActionInfo()
            {
                actionTarget = new List<FileUtilities.FileDetails>();
            }

            public ActionType actionType { get; set; }

            public FileType actionTargetType
            {
                get
                {
                    if (actionTarget.All(f => f.FType == FileType.PDF)) return FileType.PDF;
                    if (actionTarget.All(f => f.FType == FileType.IMAGE)) return FileType.IMAGE;
                    return FileType.UNKNOWN;
                }
            }

            public List<FileUtilities.FileDetails> actionTarget { get; set; }

            public void AskUser()
            {
                Dictionary<ActionType, string> options = new Dictionary<ActionType, string>();

                switch (actionTargetType)
                {
                    case FileType.PDF:
                        options.Add(ActionType.PDFTOIMAGE, "Convert pdf page(s) to image(s)");
                        options.Add(ActionType.SPLIT, "Split pdf file(s)");
                        options.Add(ActionType.ROTATE, "Rotate pdf file(s)");
                        if (actionTarget.Count > 1) options.Add(ActionType.MERGE, "Merge pdf files");
                        break;
                    case FileType.IMAGE:
                        options.Add(ActionType.IMAGETOPDF, "Combine image(s) to pdf");
                        options.Add(ActionType.OPTIMIZE, "Optimize image(s)");
                        options.Add(ActionType.ROTATE, "Rotate image(s)");
                        break;
                    case FileType.UNKNOWN:
                        break;
                }

                actionType = ParseUserInput(options);

                ConsoleUtilities.PrintLine($"Selected Option Is: {Enum.GetName(actionType.GetType(), actionType)}", ConsoleColor.DarkGray);

                if (actionType == ActionType.ROTATE)
                {
                    ConsoleUtilities.PrintLine("Select rotation type", ConsoleColor.Yellow);

                    options.Clear();
                    options.Add(ActionType.ROTATECW, "Rotate clockwise");
                    options.Add(ActionType.ROTATECCW, "Rotate counter clockwise");
                    options.Add(ActionType.ROTATE180, "Rotate 180 degrees");

                    actionType = ParseUserInput(options);

                    ConsoleUtilities.PrintLine($"Selected Rotation Option Is: {(int)actionType}", ConsoleColor.DarkGray);
                }

                if (actionType == ActionType.OPTIMIZE)
                {
                    ConsoleUtilities.PrintLine("Select final resolution", ConsoleColor.Yellow);
                    options.Clear();

                    resolution = ParseUserInput(options);
                    ConsoleUtilities.PrintLine($"Selected resolution is: {resolution}", ConsoleColor.DarkGray);
                }
            }

            private dynamic ParseUserInput(Dictionary<ActionType, string> options)
            {
                if (options.Count != 0)
                    ConsoleUtilities.PrintOptions(options.Values.ToArray(), ConsoleColor.DarkYellow);

                var userinput = Console.ReadLine();

                var selectedOption = Convert.ToInt32(string.IsNullOrEmpty(userinput) ? "0" : userinput);
                ConsoleUtilities.PrintLine("Processing", ConsoleColor.DarkGray);

                if (options.Count != 0)
                    return options.ElementAt(selectedOption - 1).Key;
                else
                    return selectedOption;
            }
        }
    }
}
