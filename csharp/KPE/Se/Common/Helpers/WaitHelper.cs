using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.Common.Helpers
{
    public static class WaitHelper
    {
        public static bool TryWaitForCondition(Func<bool> condition, int timeOut = Periods.TimeOutDefault)
        {
            var finishTime = DateTime.Now.AddSeconds(timeOut);

            do
            {
                if (condition())
                {
                    return true;
                }
                System.Threading.Thread.Sleep(500);
            }
            while (DateTime.Now < finishTime);

            // fail
            return false;
        }
    }
}
