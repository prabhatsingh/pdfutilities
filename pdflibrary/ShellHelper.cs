using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfLibrary
{
    public class ShellHelper
    {
        //Extension - Extension of the file (.zip, .txt etc.)
        //MenuName - Name for the menu item (Play, Open etc.)
        //MenuDescription - The actual text that will be shown
        //MenuCommand - Path to executable
        public static bool AddContextMenuItem(string Extension, string MenuName, string MenuDescription, string MenuCommand)
        {
            bool ret = false;
            RegistryKey rkey = Registry.ClassesRoot.OpenSubKey(Extension);
            if (rkey != null)
            {
                string extstring = rkey.GetValue("").ToString();
                rkey.Close();
                if (extstring != null)
                {
                    if (extstring.Length > 0)
                    {
                        rkey = Registry.ClassesRoot.OpenSubKey(extstring, true);
                        if (rkey != null)
                        {
                            string strkey = "shell\\" + MenuName + "\\command";
                            RegistryKey subky = rkey.CreateSubKey(strkey);
                            if (subky != null)
                            {
                                subky.SetValue("", MenuCommand);
                                subky.Close();
                                subky = rkey.OpenSubKey("shell\\" + MenuName, true);
                                if (subky != null)
                                {
                                    subky.SetValue("", MenuDescription);
                                    subky.Close();
                                }
                                ret = true;
                            }
                            rkey.Close();
                        }
                    }
                }
            }
            return ret;
        }
    }
}
