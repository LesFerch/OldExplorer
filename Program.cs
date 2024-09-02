using System;
using System.Diagnostics;
using System.Threading;
using SHDocVw;

namespace OldExplorer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string Folder = @"\";

            for (int i = 0; i < args.Length; i++)
            {
                Folder = args[i];
            }

            Folder = Folder.Replace("\"", "\\");

            Process.Start(new ProcessStartInfo
            {
                FileName = @"shell:::{26EE0668-A00A-44D7-9371-BEB064C98683}\0\::{4336A54D-038B-4685-AB02-99BB52D3FB8B}",
                UseShellExecute = true,
                Verb = "open"
            });

            ShellWindows shellWindows = new ShellWindows();

            bool windowFound = false;

            for (int i = 1; i < 20; i++)
            {
                Thread.Sleep(80);
                foreach (InternetExplorer window in shellWindows)
                {
                    if (window.Name == "File Explorer" && window.LocationURL == "file:///C:/Users/Public")
                    {
                        window.Navigate(Folder);
                        windowFound = true;
                        break;
                    }
                }
                if (windowFound) { break; }
            }
        }
    }
}