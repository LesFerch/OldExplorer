using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using Microsoft.Win32;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        int Attempts = 20;
        string Folder = "C:";
        bool Clip = true;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].ToLower() == "/x") { Clip = false; }
            else { Folder = args[i]; }
        }
        if (Folder.EndsWith("\""))
        {
            Folder = Folder.Remove(Folder.Length - 1) + "\\";
        }

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "control.exe",
            Arguments = "admintools",
            RedirectStandardOutput = false,
            RedirectStandardError = false,
            UseShellExecute = true,
            CreateNoWindow = false
        };

        Process process = new Process
        {
            StartInfo = startInfo
        };

        process.Start();

        AutomationElement FoundWindow = null;
        string[] WinList = new[]
        {
            "Administrative Tools",
            "Control Panel\\All Control Panel Items\\Administrative Tools",
            "Windows Tools",
            "Control Panel\\All Control Panel Items\\Windows Tools"
        };

        for (int i = 0; i < Attempts; i++)
        {
            for (int j = 0; j < WinList.Length; j++)
            {
                FoundWindow = FindWindowByName(WinList[j]);

                if (FoundWindow != null)
                {
                    SetForegroundWindow((IntPtr)FoundWindow.Current.NativeWindowHandle);
                    Thread.Sleep(200);
                    if (Clip && (Folder.Length > 3))
                    {
                        Clipboard.SetText(Folder);
                        SendKeys.SendWait("^{l}^{v}{Enter}");
                    }
                    else
                    {
                        SendKeys.SendWait("^{l}" + Folder + "{Enter}");
                    }
                    break;
                }
            }
            if (FoundWindow != null) { break; }
            Thread.Sleep(100);
        }
    }
    private static AutomationElement FindWindowByName(string name)
    {
        return AutomationElement.RootElement.FindFirst(
            TreeScope.Children,
            new PropertyCondition(AutomationElement.NameProperty, name)
        );
    }

    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
}