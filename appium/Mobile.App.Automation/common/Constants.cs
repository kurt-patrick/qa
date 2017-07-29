using OpenQA.Selenium.Appium.PageObjects;
using System;

namespace KPE.Mobile.App.Automation.Common
{
    public static class Constants
    {
        public const int DefaultTimeOut = 40;
        public static TimeSpan DefaultTimeOutTimeSpan = TimeSpan.FromSeconds(DefaultTimeOut);
        public static TimeOutDuration DefaultTimeOutDuration = new TimeOutDuration(DefaultTimeOutTimeSpan);
    }
}
