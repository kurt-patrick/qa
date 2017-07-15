using OpenQA.Selenium.Appium.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Common
{
    public static class Constants
    {
        public const int DefaultTimeOut = 20;
        public static TimeSpan DefaultTimeOutTimeSpan = TimeSpan.FromSeconds(DefaultTimeOut);
        public static TimeOutDuration DefaultTimeOutDuration = new TimeOutDuration(DefaultTimeOutTimeSpan);
    }
}
