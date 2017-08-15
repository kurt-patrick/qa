using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;

namespace KPE.Mobile.App.Automation.PageObjects
{
    public static class PageObjectFactory
    {
        public static T Create<T>(AppiumDriver<IWebElement> driver)  where T : PageBase
        {
            QA.ObjectQA.ThrowIfNull(driver);
            return (T)Activator.CreateInstance(typeof(T), new object[] { driver });
        }
    }
}
