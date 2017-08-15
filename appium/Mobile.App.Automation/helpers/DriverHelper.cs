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
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Creates the Appium driver either AndroidDriver or IOSDriver
        /// </summary>
        /// <param name="driverType">Android, AndroidDriver or IOSDriver</param>
        /// <param name="uri">The uri of the Appium node server</param>
        /// <param name="driverCaps"></param>
        /// <returns>AndroidDriver or IOSDriver</returns>
        public static AppiumDriver<IWebElement> CreateAppiumWebDriver(DriverCapabilities driverCaps)
        {
            ObjectQA.ThrowIfNull(driverCaps);
            ObjectQA.ThrowIfIEnumerableIsEmpty(driverCaps.Capabilities);

            string device = driverCaps.GetCapability("device");
            var appiumUri = new Uri(Settings.Instance().AppiumHubUri);
            var commandTimeout = TimeSpan.FromMinutes(10);
            var desiredCaps = GetDesiredCapabilities(driverCaps);

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

        private static DesiredCapabilities GetDesiredCapabilities(DriverCapabilities driverCaps)
        {
            var desiredCaps = new DesiredCapabilities();
            driverCaps.Capabilities.ForEach(cap => desiredCaps.SetCapability(cap.Key, cap.Value));
            return desiredCaps;
        }


    }
}
