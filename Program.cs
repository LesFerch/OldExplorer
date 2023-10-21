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
                Process[] processes = Process.GetProcessesByName("explorer");
                foreach (Process process in processes)
                {
                    string commandLine = GetCommandLine(process.Id);
                    if (commandLine.Equals(targetCommandLine, StringComparison.OrdinalIgnoreCase))
                    {
                        explorerPid = (uint)process.Id;
                        break;
                    }
                }
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

            Thread.Sleep(200);
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

        static string GetCommandLine(int processId)
        {
            using (ManagementObjectSearcher mos = new ManagementObjectSearcher($"SELECT CommandLine FROM Win32_Process WHERE ProcessId = {processId}"))
            using (ManagementObjectCollection moc = mos.Get())
            {
                foreach (ManagementBaseObject mo in moc)
                {
                    return mo["CommandLine"]?.ToString();
                }
            }
            return null;
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