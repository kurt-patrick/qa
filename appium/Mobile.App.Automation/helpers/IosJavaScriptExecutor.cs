using KPE.Mobile.App.Automation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using System;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Helpers
{
    /// <summary>
    /// An unfortunate bug exists in the iOS 7.0 - 8.x Simulators where ScrollViews, CollectionViews, 
    /// and TableViews don't recognize gestures initiated by UIAutomation (which Appium uses under 
    /// the hood for iOS). To work around this, we have provided access to a different function, 
    /// scroll, which in many cases allows you to do what you wanted to do with one of these views, 
    /// namely, scroll it!
    /// https://github.com/appium/appium/blob/master/docs/en/writing-running-appium/touch-actions.md
    /// https://github.com/appium/appium/blob/master/docs/en/writing-running-appium/ios-xctest-mobile-gestures.md
    /// </summary>
    public class IosJavaScriptExecutor : PageBase
    {
        private const string MobileScroll = "mobile: scroll";

        public IosJavaScriptExecutor(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public void ScrollToElement(string id)
        {
            var args = new Dictionary<string, string>() { { "element", id } };
            ExecuteScript(MobileScroll, args);
        }

        public void ScrollDown()
        {
            var args = new Dictionary<string, string>() { { "direction", "down" } };
            ExecuteScript(MobileScroll, args);
        }

        public void ScrollUp()
        {
            var args = new Dictionary<string, string>() { { "direction", "up" } };
            ExecuteScript(MobileScroll, args);
        }

        private object ExecuteScript(string script, Dictionary<string, string> args)
        {
            if (args == null)
            {
                args = new Dictionary<string, string>();
            }
            return ((IOSDriver<IWebElement>)_driver).ExecuteScript(script, args);
        }

        public override bool IsLoaded()
        {
            throw new NotSupportedException();
        }
    }
}
