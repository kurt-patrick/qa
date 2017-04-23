using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.Automation.AutomationInstructions.Configuration;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;

namespace KPE.Tosca.SETs
{
    [SpecialExecutionTaskName("GetProcessWindowTitle")]
    public class GetProcessWindowTitle : SpecialExecutionTaskEnhanced
    {
        public GetProcessWindowTitle(Validator validator) : base(validator)
        {
        }

        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
        {
            string bufferName = testAction.GetParameter("BufferName").GetAsInputValue().Value;
            string processName = testAction.GetParameter("ProcessName").GetAsInputValue().Value;

            // Clear out any existing values
            Buffers.Instance.SetBuffer(bufferName, "", false);

            // Detect if any browsers are open with window titles
            string windowTitle = GetWindowTitle(processName);

            // Write to buffer
            Buffers.Instance.SetBuffer(bufferName, windowTitle, false);

            // Ouput for tosca scratchbook
            testAction.SetResult(new PassedActionResult(string.Format("Buffer ({0}) has been set as ({1})", bufferName, windowTitle)));

        }

        private static string GetWindowTitle(string processName)
        {
            string windowTitle = null;

            if (string.IsNullOrWhiteSpace(processName))
            {
                // auto-detect which, if any, browser is displayed
                var titles = new List<string> { GetFirstProcessWindowWithTitle("firefox"), GetFirstProcessWindowWithTitle("iexplore"), GetFirstProcessWindowWithTitle("chrome") };
                windowTitle = titles.FirstOrDefault(title => !string.IsNullOrWhiteSpace(title));
            }
            else
            {
                // detect if the specific browser is displayed
                windowTitle = GetFirstProcessWindowWithTitle(processName);
            }

            return windowTitle ?? string.Empty;
        }

        private static List<System.Diagnostics.Process> GetProcessesByName(string name)
        {
            return System.Diagnostics.Process.GetProcessesByName(name).Where(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle)).ToList();
        }

        private static string GetFirstProcessWindowWithTitle(string name)
        {
            string retVal = string.Empty;
            var processes = GetProcessesByName(name);
            if (processes.Count() > 0)
            {
                // if no special characters return the entire title
                retVal = processes[0].MainWindowTitle.Trim();

                // special characters exist, return the longest word
                if (retVal.IndexOf("'") + retVal.IndexOf("{") + retVal.IndexOf("}") + retVal.IndexOf(",") != -4)
                {
                    retVal =
                        retVal.Replace("'", " ").Replace("{", " ").Replace("}", " ").Replace(",", " ")
                        .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList().OrderByDescending(str => str.Length)
                        .First();
                }

            }
            return retVal;
        }

        internal static void Test()
        {
            string windowTitle = string.Empty;

            windowTitle = GetWindowTitle(null);
            windowTitle = GetWindowTitle("     ");
            //windowTitle = GetWindowTitle("firefox");
            //windowTitle = GetWindowTitle("chrome");
            //windowTitle = GetWindowTitle("iexplore");
        }


    }
}
