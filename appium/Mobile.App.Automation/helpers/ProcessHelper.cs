using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class ProcessHelper
    {
        const string CmdExe = "cmd.exe";

        /// <summary>
        /// Helper function to execute commands from the command line
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static Process ExecuteCommandAndTerminate(string arguments)
        {
            QA.StringQA.ThrowIfNullOrWhiteSpace(arguments, nameof(arguments));

            Process proc = new Process();
            proc.StartInfo.FileName = CmdExe;

            // [/C]     Carries out the command specified by string and then terminates
            proc.StartInfo.Arguments = $"/C {arguments}";

            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.WaitForExit();
            return proc;
        }

        public static bool IsAndroidDeviceRunning(string deviceName)
        {
            Process process = null;
            try
            {
                QA.StringQA.ThrowIfNullOrWhiteSpace(deviceName, nameof(deviceName));

                // Call "adb devices" this enables 2 things
                // 1. if "adb start-server" has not been called it wil be called for us
                // 2. we can check if the device we will be using is running
                process = ExecuteCommandAndTerminate("adb devices");

                // check if the device is found
                return GetStandardOutput(process).Any(line => line.Contains(deviceName));

            }
            finally
            {
                process?.Close();
            }

        }

        public static List<string> GetStandardOutput(Process process)
        {
            var retVal = new List<string>();
            while (!process.StandardOutput.EndOfStream)
            {
                retVal.Add(process.StandardOutput.ReadLine() ?? string.Empty);
            }
            return retVal;
        }
    }
}
