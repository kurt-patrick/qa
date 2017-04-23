using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Creation.Attributes;
using Tricentis.Automation.Engines;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;

namespace KPE.Tosca.SET
{
    /// <summary>
    /// The value within the attribute SpecialExecutionTaskName must match the name of the XModule in Tosca
    /// </summary>
    [SpecialExecutionTaskName("HelloWorld")]
    public class HelloWorld : SpecialExecutionTask
    {
        public HelloWorld(Validator validator) : base(validator)
        {
        }

        public override ActionResult Execute(ISpecialExecutionTaskTestAction testAction)
        {
            return new PassedActionResult("Hello World");
        }
    }

}
