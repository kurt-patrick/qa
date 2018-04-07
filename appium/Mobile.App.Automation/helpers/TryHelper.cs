using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class TryHelper
    {
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