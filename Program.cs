using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;

class Program
{
    static void Main(string[] args)
    {
        string Folder = "This PC";
        int Attempts = 20;

        if (args.Length > 0)
        {
            Folder = args[0];

            // Fix issue where argument is quoted and ends with backslash
            if (Folder.EndsWith("\""))
            {
                Folder = Folder.Remove(Folder.Length - 1) + "\\";
            }
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

        // Wait for the Administrative Tools or Windows Tools window to appear
        // Check for both short name and full path name

        AutomationElement adminToolsWindow = null;
        AutomationElement adminToolsWindowFP = null;
        AutomationElement windowsToolsWindow = null;
        AutomationElement windowsToolsWindowFP = null;

        // Check for window up to number of times specified by Attempts variable
        // A for loop avoids the possibility of an infinite loop

        for (int i = 0; i < Attempts; i++)
        {

            Thread.Sleep(500);

            adminToolsWindow = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, "Administrative Tools")
            );

            adminToolsWindowFP = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, "Control Panel\\All Control Panel Items\\Administrative Tools")
            );

            windowsToolsWindow = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, "Windows Tools")
            );

            windowsToolsWindowFP = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.NameProperty, "Control Panel\\All Control Panel Items\\Windows Tools")
            );

            if (adminToolsWindow != null)
            {
                SetForegroundWindow((IntPtr)adminToolsWindow.Current.NativeWindowHandle);
                SendKeys.SendWait("%{d}" + Folder + "{Enter}");
                break;
            }

            if (adminToolsWindowFP != null)
            {
                SetForegroundWindow((IntPtr)adminToolsWindowFP.Current.NativeWindowHandle);
                SendKeys.SendWait("%{d}" + Folder + "{Enter}");
                break;
            }

            if (windowsToolsWindow != null)
            {
                SetForegroundWindow((IntPtr)windowsToolsWindow.Current.NativeWindowHandle);
                SendKeys.SendWait("%{d}" + Folder + "{Enter}");
                break;
            }

            if (windowsToolsWindowFP != null)
            {
                SetForegroundWindow((IntPtr)windowsToolsWindowFP.Current.NativeWindowHandle);
                SendKeys.SendWait("%{d}" + Folder + "{Enter}");
                break;
            }
        }
    }
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
}
