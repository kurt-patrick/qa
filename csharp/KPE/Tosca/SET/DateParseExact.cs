using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.Automation.AutomationInstructions.Configuration;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
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
    [SpecialExecutionTaskName("DateParseExact")]
    public class DateParseExact : SpecialExecutionTask
    {
        public DateParseExact(Validator validator)
            : base(validator)
        {
        }

        public override ActionResult Execute(ISpecialExecutionTaskTestAction testAction)
        {
            try
            {
                string inDate = testAction.GetParameter("Date").GetAsInputValue().Value;
                string inFormat = testAction.GetParameter("Format").GetAsInputValue().Value;
                string outFormat = testAction.GetParameter("NewFormat").GetAsInputValue().Value;
                string bufferName = testAction.GetParameter("BufferName").GetAsInputValue().Value;

                // "M/dd/yyyy"
                var parsedDate = DateTime.ParseExact(inDate, inFormat, System.Globalization.CultureInfo.InvariantCulture);

                // "dd.MM.yyyy"
                var outDate = parsedDate.ToString(outFormat);

                // Write date to buffer
                Buffers.Instance.SetBuffer(bufferName, outDate, false);


                //Buffers
                return new PassedActionResult(outDate);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
