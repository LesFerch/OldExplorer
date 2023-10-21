using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Management;

namespace OldExplorer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string Folder = "\\";
            bool Clip = true;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "/x") { Clip = false; }
                else { Folder = args[i]; }
            }
            Folder = Folder.Replace("\"", "\\");

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "control.exe",
                Arguments = "admintools",
                UseShellExecute = false,
            };

            Process controlProcess = new Process();
            controlProcess.StartInfo = startInfo;
            controlProcess.Start();

            //This is the explorer.exe task's command line when spawned via control admintools:
            string targetCommandLine = "C:\\WINDOWS\\explorer.exe /factory,{5BD95610-9434-43c2-886c-57852CC8A120} -Embedding";

            uint explorerPid = 0;

            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(200);
                explorerPid = GetLatestMatchingExplorerPid(targetCommandLine);
                if (explorerPid != 0) { break; }
            }

            if (explorerPid == 0) { return; }

            IntPtr mainWindowHandle = IntPtr.Zero;

            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(200);
                mainWindowHandle = GetMainWindowHandle(explorerPid);
                if (mainWindowHandle != IntPtr.Zero) { break; }
            }

            if (mainWindowHandle == IntPtr.Zero) { return; }

            SetForegroundWindow(mainWindowHandle);

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
        }

        static uint GetLatestMatchingExplorerPid(string targetCommandLine)
        {
            Process[] explorerProcesses = Process.GetProcessesByName("explorer");

            Process latestExplorerProcess = null;
            DateTime latestStartTime = DateTime.MinValue;

            foreach (Process explorerProcess in explorerProcesses)
            {
                if (IsMatchingCommandLine(explorerProcess, targetCommandLine))
                {
                    if (explorerProcess.StartTime > latestStartTime)
                    {
                        latestStartTime = explorerProcess.StartTime;
                        latestExplorerProcess = explorerProcess;
                    }
                }
            }

            return latestExplorerProcess != null ? (uint)latestExplorerProcess.Id : 0;
        }

        static bool IsMatchingCommandLine(Process process, string targetCommandLine)
        {
            string processCommandLine = GetCommandLine(process);
            return processCommandLine != null && processCommandLine.Equals(targetCommandLine, StringComparison.OrdinalIgnoreCase);
        }

        static string GetCommandLine(Process process)
        {
            try
            {
                using (ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
                using (ManagementObjectCollection moc = mos.Get())
                {
                    return moc.OfType<ManagementObject>().FirstOrDefault()?["CommandLine"]?.ToString();
                }
            }
            catch
            {
                return null;
            }
        }

        private static IntPtr GetMainWindowHandle(uint processId)
        {
            Process process = Process.GetProcessById((int)processId);
            return process.MainWindowHandle;
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}