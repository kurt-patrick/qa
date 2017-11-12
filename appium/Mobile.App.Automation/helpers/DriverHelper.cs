using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.QA;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class DriverHelper
    {
        /// <summary>
        /// Creates the Appium driver either AndroidDriver or IOSDriver
        /// </summary>
        /// <param name="driverCaps">WebDriver and application capability keys and values</param>
        /// <returns>AndroidDriver or IOSDriver</returns>
        public static AppiumDriver<IWebElement> CreateAppiumWebDriver(DriverCapabilities driverCaps, Uri appiumUri)
        {
            ObjectQA.ThrowIfNull(driverCaps);
            ObjectQA.ThrowIfIEnumerableIsEmpty(driverCaps.Capabilities);

            string device = driverCaps.GetCapability("device");
            var commandTimeout = TimeSpan.FromSeconds(30);
            var desiredCaps = driverCaps.DesiredCapabilities();

            if ("Android".Equals(device))
            {
                return new AndroidDriver<IWebElement>(appiumUri, desiredCaps, commandTimeout);
            }

            if ("iOS".Equals(device))
            {
                return new IOSDriver<IWebElement>(appiumUri, desiredCaps, commandTimeout);
            }

            throw new NotImplementedException("No logic has been implemented for appium driver type: " + device);

        }

    }
}
