using KPE.Mobile.App.Automation.QA;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Linq;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class DriverHelper
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Configure the driver for sauce labs grid
        /// https://wiki.saucelabs.com/display/DOCS/Platform+Configurator#/
        /// https://github.com/saucelabs-sample-test-frameworks/CSharp-Nunit-Selenium/blob/master/ParallelSelenium/ParallelSearchTests.cs
        /// </summary>
        public static IWebDriver CreateRemoteWebDriver(string uri, DesiredCapabilities capabilities)
        {
            StringQA.ThrowIfNullOrWhiteSpace(uri);
            ObjectQA.ThrowIfNull(capabilities);

            return new RemoteWebDriver(new Uri(uri), capabilities);
        }

        /// <summary>
        /// Creates the Appium driver either AndroidDriver or IOSDriver
        /// </summary>
        /// <param name="driverType">Android, AndroidDriver or IOSDriver</param>
        /// <param name="uri">The uri of the Appium node server</param>
        /// <param name="capabilities"></param>
        /// <returns>AndroidDriver or IOSDriver</returns>
        public static AppiumDriver<IWebElement> CreateAppiumWebDriver(string driverType, string uri, DesiredCapabilities capabilities)
        {
            StringQA.ThrowIfNullOrWhiteSpace(driverType);
            StringQA.ThrowIfNullOrWhiteSpace(uri);
            ObjectQA.ThrowIfNull(capabilities);

            if (new string[] { "AndroidDriver", "Android" }.Contains(driverType, StringComparer.CurrentCultureIgnoreCase))
            {
                return new AndroidDriver<IWebElement>(new Uri(uri), capabilities, TimeSpan.FromMinutes(10));
            }

            else if (string.Equals("IOSDriver", driverType, StringComparison.CurrentCultureIgnoreCase))
            {
                return new IOSDriver<IWebElement>(new Uri(uri), capabilities, TimeSpan.FromMinutes(10));
            }

            throw new NotImplementedException("No logic has been implemented for appium driver type: " + driverType);

        }

    }
}
