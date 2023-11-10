using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using System.Text;

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

            for (int i = 1; i < 20; i++)
            {
                Thread.Sleep(100);
                try
                {
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
                catch
                {
                }
                if (explorerPid != 0) { break; }
            }

            if (explorerPid == 0) { return; }

            IntPtr mainWindowHandle = IntPtr.Zero;

            for (int i = 1; i < 20; i++)
            {
                Thread.Sleep(100);
                mainWindowHandle = GetMainWindowHandle(explorerPid);
                if (mainWindowHandle != IntPtr.Zero) { break; }
            }

            if (mainWindowHandle == IntPtr.Zero) { return; }

            Thread.Sleep(200);

            IntPtr currentHandle = mainWindowHandle;
            string[] controlNames = { "WorkerW", "ReBarWindow32", "Address Band Root", "msctls_progress32", "Breadcrumb Parent", "ToolbarWindow32" };
            foreach (string controlName in controlNames)
            {
                currentHandle = FindWindowEx(currentHandle, IntPtr.Zero, controlName, null);
                if (currentHandle == IntPtr.Zero) { break; }
            }

            if (currentHandle == IntPtr.Zero) { return; }

            for (int i = 1; i < 50; i++)
            {
                SetForegroundWindow(mainWindowHandle);
                const int bufferLength = 256;
                StringBuilder addressText = new StringBuilder(bufferLength);
                GetWindowText(currentHandle, addressText, bufferLength);
                if (addressText.ToString() != "") { break; }
                Thread.Sleep(20);
            }

            Thread.Sleep(100);

            if (Clip && (Folder.Length > 3))
            {
                Clipboard.SetText(Folder);
                SetForegroundWindow(mainWindowHandle);
                SendKeys.SendWait("^{l}^{v}{Enter}");
            }
            else
            {
                SetForegroundWindow(mainWindowHandle);
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
        
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    }
}