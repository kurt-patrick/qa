using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.QA;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class DriverHelper
    {
        /// <summary>
        /// Creates the Appium driver either AndroidDriver or IOSDriver
        /// </summary>
        /// <param name="cabilities">WebDriver and application capability keys and values</param>
        /// <returns>AndroidDriver or IOSDriver</returns>
        public static AppiumDriver<IWebElement> CreateAppiumWebDriver(DesiredCapabilities cabilities, Uri uri)
        {
            ObjectQA.ThrowIfNull(cabilities);
            ObjectQA.ThrowIfIEnumerableIsEmpty(cabilities.ToDictionary().Keys);

            string device = cabilities.GetCapability("device").ToString();
            var commandTimeout = TimeSpan.FromSeconds(30);

            if ("Android".Equals(device))
            {
                // https://github.com/SeleniumHQ/selenium/issues/4142
                // https://discuss.appium.io/t/gridexception-cannot-extract-a-capabilities-from-the-request/17265
                // https://www.youtube.com/watch?v=vXpskMkytD8&feature=youtu.be

                return new AndroidDriver<IWebElement>(uri, cabilities, commandTimeout);
            }

            if ("iOS".Equals(device))
            {
                return new IOSDriver<IWebElement>(uri, cabilities, commandTimeout);
            }

            throw new NotImplementedException("No logic has been implemented for appium driver type: " + device);

        }

    }
}
