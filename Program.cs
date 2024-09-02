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
            string f = Folder.ToLower();

            if (f == "desktop") { Folder = "shell:desktop"; }
            if (f == "documents") { Folder = "shell:::{A8CDFF1C-4878-43BE-B5FD-F8091C1C60D0}"; }
            if (f == "downloads") { Folder = "shell:downloads"; }
            if (f == "favorites") { Folder = "shell:::{323CA680-C24D-4099-B94D-446DD2D7249E}"; }
            if (f == "frequent") { Folder = "shell:::{3936E9E4-D92C-4EEE-A85A-BC16D5EA0819}"; }
            if (f == "home") { Folder = "shell:::{679f85cb-0220-4080-b29b-5540cc05aab6}"; }
            if (f == "libraries") { Folder = "shell:libraries"; }
            if (f == "music") { Folder = "shell:::{3DFDF296-DBEC-4FB4-81D1-6A3438BCF4DE}"; }
            if (f == "onedrive") { Folder = "shell:::{018D5C66-4533-4307-9B53-224DE2ED1FE6}"; }
            if (f == "pictures") { Folder = "shell:::{24ad3ad4-a569-4530-98e1-ab02f9417aa8}"; }
            if (f == "public") { Folder = "shell:public"; }
            if (f == "recent") { Folder = "shell:recent"; }
            if (f == "recycle bin") { Folder = "shell:::{645FF040-5081-101B-9F08-00AA002F954E}"; }
            if (f == "this pc") { Folder = "shell:::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"; }
            if (f == "userprofile") { Folder = "shell:::{59031A47-3F72-44A7-89C5-5595FE6B30EE}"; }
            if (f == "videos") { Folder = "shell:::{A0953C92-50DC-43BF-BE83-3742FED03C9C}"; }

            Process.Start(new ProcessStartInfo
            {
                FileName = @"shell:::{26EE0668-A00A-44D7-9371-BEB064C98683}\0\::{4336A54D-038B-4685-AB02-99BB52D3FB8B}",
                UseShellExecute = true,
                Verb = "open"
            });

            string publicPath = Environment.GetEnvironmentVariable("PUBLIC");

            ShellWindows shellWindows = new ShellWindows();

            bool windowFound = false;

            for (int i = 1; i < 20; i++)
            {
                Thread.Sleep(80);
                foreach (InternetExplorer window in shellWindows)
                {
                    if (window.Name == "File Explorer" && window.LocationURL == $"file:///{publicPath}")
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