using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class WaitHelper
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

        //public static TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, IWebDriver driver)
        //{
        //    return WaitUntil(condition, driver, true, Settings.Instance.DefaultTimeOut);
        //}

        //public static TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, IWebDriver driver, bool throwEx, int timeOut)
        //{
        //    qa.ObjectQA.ThrowIfNull(driver);
        //    try
        //    {
        //        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
        //        return wait.Until(condition);
        //    }
        //    catch
        //    {
        //        if (throwEx)
        //        {
        //            throw;
        //        }
        //    }
        //    return default(TResult);
        //}
    }
}