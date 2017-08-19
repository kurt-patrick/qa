using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class ObjectHelper
    {
        public static bool TryDispose(IDisposable obj)
        {
            try
            {
                if (obj != null)
                {
                    obj.Dispose();
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ignored)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return false;
            }
            return true;
        }
    }
}
