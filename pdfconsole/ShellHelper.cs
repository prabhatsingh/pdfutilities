using IWshRuntimeLibrary;
using System;
using System.IO;

namespace PdfConsole
{
    public class ShellHelper
    {
        public static void AddMenuItem()
        {
            string sendto = Environment.GetFolderPath(Environment.SpecialFolder.SendTo);
            string shortcutpath = sendto + Path.DirectorySeparatorChar + "PDFUtilities.lnk";

            object shDesktop = "Desktop";
            WshShell shell = new WshShell();

            if (System.IO.File.Exists(shortcutpath))
                System.IO.File.Delete(shortcutpath);

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutpath);
            shortcut.Description = "Shortcut for PDF Utilities";
            shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            shortcut.Save();

            Console.Write("Link Updated...");
        }
    }
}
