﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public static class ConsoleUtilities
    {
        public static void PrintLine(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void PrintOptions(string[] msg, ConsoleColor color)
        {
            int opcnt = 0;

            if(msg.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No options found");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = color;
            msg.ToList().ForEach(f => Console.WriteLine("{0}. {1}", ++opcnt, f));
            Console.ResetColor();
        }
    }
}