using au.com.kleenheat.se.common;
using au.com.kleenheat.se.qa;
using au.com.kleenheat.se.tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se.helpers
{
    public static class DriverHelper
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Firefox Driver 
        /// Is capable of being run and is tested on Windows, Mac, Linux.
        /// Currently on versions 3.6, 10, latest - 1, latest
        ///
        /// Internet Explorer Driver
        /// This driver has been tested with IE 7, 8, 9, 10, and 11 
        /// on appropriate combinations of Vista, Windows 7, Windows 8, and Windows 8.1. 
        /// As of 15 April 2014, IE 6 is no longer supported.
        /// https://github.com/SeleniumHQ/selenium/wiki/InternetExplorerDriver
        /// The standalone server executable must be downloaded from the Downloads page and placed in your PATH.
        /// NOTE: *** See the page above for Required Configuration ***
        /// This article is very useful - as it shows you haow to configure IE as specifid in above articale also
        /// http://www.joecolantonio.com/2012/07/31/getting-started-using-selenium-2-0-webdriver-for-ie-in-visual-studio-c/
        ///
        /// Chrome Driver
        /// https://github.com/SeleniumHQ/selenium/wiki/ChromeDriver
        /// http://www.joecolantonio.com/2013/01/18/selenium-webdriver-using-chrome-webdriver-in-visual-studio-c/
        /// https://sites.google.com/a/chromium.org/chromedriver/
        /// Needs to be placed somewhere on your system’s path in order for WebDriver to automatically discover it.
        /// </summary>
        public static IWebDriver CreateWebDriver(TestCaseSettings.eBrowser browser)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case TestCaseSettings.eBrowser.Chrome:
                    driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;

                case TestCaseSettings.eBrowser.InternetExplorer:
                    driver = new InternetExplorerDriver();
                    break;

                case TestCaseSettings.eBrowser.FireFox:
                    // https://developer.mozilla.org/en-US/docs/Mozilla/QA/Marionette/WebDriver#Downloading
                    //http://seleniumsimplified.com/2016/04/how-to-use-the-firefox-marionette-driver/

                    //https://developer.mozilla.org/en-US/docs/Mozilla/QA/Marionette/WebDriver/status
                    //https://github.com/mozilla/geckodriver
                    //https://github.com/mozilla/geckodriver/releases

                    //_driver = new MarionetteDriver();
                    //_driver = new FirefoxDriver(new FirefoxOptions());

                    
                    //DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
                    // Set Marionette on so the Grid will use this instead of normal FirefoxDriver
                    //capabilities.SetCapability("marionette", true);
                    //capabilities.SetCapability("capabilityName", "marionette");
                    //driver = new RemoteWebDriver(capabilities);

                    var options = new FirefoxOptions();
                    options.AddAdditionalCapability("capabilityName", "marionette");

                    driver = new FirefoxDriver(options);

                    break;

                case TestCaseSettings.eBrowser.PhantomJS:
                    driver = new PhantomJSDriver();
                    break;

                default:
                    throw new NotImplementedException("Case has not been implemented (you need to update the code) for " + browser.ToString());
            }

            driver.Manage().Window.Maximize();
            return driver;
        }


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
        /// <param name="driverType">This is case sensitive and must be AndroidDriver or IOSDriver</param>
        /// <param name="uri">The uri of the Appium node server</param>
        /// <param name="capabilities"></param>
        /// <returns></returns>
        public static IWebDriver CreateAppiumWebDriver(string driverType, string uri, DesiredCapabilities capabilities)
        {
            StringQA.ThrowIfNullOrWhiteSpace(driverType);
            StringQA.ThrowIfNullOrWhiteSpace(uri);
            ObjectQA.ThrowIfNull(capabilities);

            if (string.Equals("AndroidDriver", driverType, StringComparison.CurrentCultureIgnoreCase))
            {
                return new AndroidDriver<AppiumWebElement>(new Uri(uri), capabilities, TimeSpan.FromMinutes(10));
            }
            else if (string.Equals("IOSDriver", driverType, StringComparison.CurrentCultureIgnoreCase))
            {
                return new IOSDriver<AppiumWebElement>(new Uri(uri), capabilities, TimeSpan.FromMinutes(10));
            }

            throw new NotImplementedException("No logic has been implemented for appium driver type: " + driverType);

        }

    }
}
