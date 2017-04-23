using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Tricentis.Automation.AutomationInstructions.Configuration;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;

namespace KPE.Tosca.SETs
{
    [SpecialExecutionTaskName("KillBrowserInstances")]
    public class KillBrowserInstances : SpecialExecutionTaskEnhanced
    {
        public KillBrowserInstances(Validator validator)
            : base(validator)
        {
        }

        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
        {
            KillAllBrowsersWithWindowTitle();
        }

        private static void KillAllBrowsersWithWindowTitle()
        {
            var list = new List<String>() { "firefox", "chrome", "iexplore" };
            list.ForEach(name => KillProcessesByName(name));
        }

        private static void KillProcessesByName(string name)
        {
            var processes = GetProcessesByName(name);

            if (processes.Count() > 0)
            {
                // kill processes - ignore exceptions
                foreach (var process in processes)
                {
                    try { process.Kill(); }
                    catch { }
                }

                // sleep briefly to let the system do its thing
                System.Threading.Thread.Sleep(1000);

                // check all processes have been killed
                processes = GetProcessesByName(name);

                // throw exception on failure
                if (processes.Count > 0)
                {
                    throw new Exception("Failed to kill all instances of process: " + name);
                }

            }

        }

        private static List<Process> GetProcessesByName(string name)
        {
            return Process.GetProcessesByName(name).ToList();
        }

    }
}
