using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Tosca.SETs
{
    class Program
    {
        static void Main(string[] args)
        {
            KPE.Tosca.SETs.GetProcessWindowTitle.Test();
        }

        private static List<System.Diagnostics.Process> GetProcessesByName(string name)
        {
            return System.Diagnostics.Process.GetProcessesByName(name).Where(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle)).ToList();
        }

        private static string GetProcessWindowTitle(string name)
        {
            var processes = GetProcessesByName(name);
            if (processes.Count() > 0)
            {
                return processes[0].MainWindowTitle;
            }
            return string.Empty;
        }

        private static void KillProcess(string name)
        {
            var processes = GetProcessesByName(name);
            if (processes.Count() > 0)
            {
                // kill processes - ignore exceptions
                foreach(var process in processes)
                {
                    try { process.Kill(); } catch {}
                }

                // sleep briefly to let the system do its thing
                System.Threading.Thread.Sleep(1000);

                // check all processes have been killed
                processes = GetProcessesByName(name);

                // throw exception on failure
                if(processes.Count > 0)
                {
                    throw new Exception("Failed to kill all instances of process: " + name);
                }

            }

        }

    }
}
