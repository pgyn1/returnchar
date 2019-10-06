using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace ReturnChar
{
    class Program

    {
        static void Main(string[] args)
        {
           
            //0 2 3 6 8 9 9 12 34 71
            int[] keys = new int[10] { 6, 2, 9, 71, 3, 9, 12, 0, 8, 34 };
            int[] items = new int[10] { 74, 221, 48,834, 3, 334435, 12, 1322, 64, 22 };

            Array.Sort(keys, items, 0, 10);

            Console.WriteLine($"{items[4]}");

            if (HighScorePath())
            {
                //Menu.IntroAnimation();
                Player.Restoreplayerlevelscore(0);
                Menu.DisplayMenuOptions();
            }
            else
            {
                Console.WriteLine("Path not set"); Console.ReadLine();
            }


        }

        public static bool HighScorePath()
        {

            const string highscorefilepath = @"C:\program files\returnchar\highscores.csv";

            try
            {

                if (File.Exists(highscorefilepath))
                {
                    StreamClass.SetHighScorePath(highscorefilepath);
                    Console.WriteLine($"Path successfully set");
                    Console.ReadLine();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception secexc)
            {
                Console.WriteLine($"{secexc.Message}");
                return false;
            }
            
        }
    }
}