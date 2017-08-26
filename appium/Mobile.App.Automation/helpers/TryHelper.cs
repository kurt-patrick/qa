using KPE.Mobile.App.Automation.Common;
using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class TryHelper
    {
        public static bool TryWaitForCondition(Func<bool> condition)
        {
            return TryWaitForCondition(condition, Constants.DefaultTimeOut);
        }

        public static bool TryWaitForCondition(Func<bool> condition, int timeOut)
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

        public static bool TryCatch(Action condition)
        {
            try
            {
                condition();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}