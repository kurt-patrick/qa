using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.Common.Helpers
{
    public static class DriverHelper
    {
        // Grid Provider System Env Keys
        private const string SauceLabs_Username = "SauceLabs_Username";
        private const string SauceLabs_Access_Key = "SauceLabs_Access_Key";
        private const string BrowserStack_Username = "BrowserStack_Username";
        private const string BrowserStack_Access_Key = "BrowserStack_Access_Key";

        /// <summary>
        /// Create a WebDriver or RemoteWebDriver based on config
        /// http://relevantcodes.com/using-nunit-to-execute-selenium-webdriver-tests/
        /// http://www.seleniumhq.org/docs/03_webdriver.jsp#driver-specifics-and-tradeoffs
        /// http://stackoverflow.com/questions/16276278/nunit-test-setup-method-with-argument
        /// </summary>
        /// <param name="config"></param>
        /// <returns>WebDriver or RemoteWebDriver depending on config</returns>
        public static IWebDriver CreateDriver(TestFixtureConfig config)
        {
            switch (config.GridProvider)
            {
                case TestFixtureConfig.eGridProvider.SauceLabs:
                    return CreateWebDriverForSauceLabs(config);

                case TestFixtureConfig.eGridProvider.BrowserStack:
                    return CreateWebDriverForBrowserStack(config);

                default:
                    return CreateWebDriverForLocalhost(config);
            }
        }

        /// <summary>
        /// Configure the driver for sauce labs grid
        /// https://wiki.saucelabs.com/display/DOCS/Platform+Configurator#/
        /// https://github.com/saucelabs-sample-test-frameworks/CSharp-Nunit-Selenium/blob/master/ParallelSelenium/ParallelSearchTests.cs
        /// </summary>
        private static IWebDriver CreateWebDriverForSauceLabs(TestFixtureConfig config)
        {
            DesiredCapabilities caps = new DesiredCapabilities();

            // Required
            SetGridCapability(caps, CapabilityType.BrowserName, config.BrowserName, true);
            SetGridCapability(caps, CapabilityType.Platform, config.OS, true);
            SetGridCapability(caps, "username", StringHelper.GetEnvVariable(SauceLabs_Username), true);
            SetGridCapability(caps, "accessKey", StringHelper.GetEnvVariable(SauceLabs_Access_Key), true);

            // Optional
            SetGridCapability(caps, CapabilityType.Version, config.BrowserVersion, false);
            SetGridCapability(caps, "deviceName", config.DeviceName, false);
            SetGridCapability(caps, "deviceOrientation", config.DeviceOrientation, false);
            SetGridCapability(caps, "name", TestContext.CurrentContext.Test.Name, false);

            return new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
        }

        /// <summary>
        /// Configure the driver for browser stack grid
        /// https://www.browserstack.com/automate/c-sharp
        /// https://www.browserstack.com/automate/capabilities
        /// </summary>
        private static IWebDriver CreateWebDriverForBrowserStack(TestFixtureConfig config)
        {
            DesiredCapabilities caps = new DesiredCapabilities();

            // Browser name
            string browserName = config.BrowserName;
            if (config.Browser == TestFixtureConfig.eBrowser.InternetExplorer)
            {
                browserName = "IE";
            }

            // OS and Version
            SetGridCapability(caps, "browser", browserName, true);
            SetGridCapability(caps, "os", config.OS, true);
            SetGridCapability(caps, "os_version", config.OSVersion, true);
            SetGridCapability(caps, "resolution", config.Resolution, true); //"1680x1050", true);
            SetGridCapability(caps, "browserstack.user", StringHelper.GetEnvVariable(BrowserStack_Username), true);
            SetGridCapability(caps, "browserstack.key", StringHelper.GetEnvVariable(BrowserStack_Access_Key), true);

            // Optional
            SetGridCapability(caps, "browser_version", config.BrowserVersion, false);
            SetGridCapability(caps, "deviceName", config.DeviceName, false);
            SetGridCapability(caps, "deviceOrientation", config.DeviceOrientation, false);
            SetGridCapability(caps, "name", TestContext.CurrentContext.Test.Name, false);

            return new RemoteWebDriver(new Uri("http://hub-cloud.browserstack.com/wd/hub/"), caps);
        }

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
        private static IWebDriver CreateWebDriverForLocalhost(TestFixtureConfig config)
        {
            IWebDriver driver = null;
            switch (config.Browser)
            {
                case TestFixtureConfig.eBrowser.InternetExplorer:
                    driver = new InternetExplorerDriver();
                    break;

                case TestFixtureConfig.eBrowser.FireFox:
                    //https://developer.mozilla.org/en-US/docs/Mozilla/QA/Marionette/WebDriver#Downloading
                    //http://seleniumsimplified.com/2016/04/how-to-use-the-firefox-marionette-driver/
                    //https://github.com/mozilla/geckodriver
                    //https://github.com/mozilla/geckodriver/releases
                    //https://developer.mozilla.org/en-US/Firefox/Releases/48
                    //https://www.mozilla.org/en-US/firefox/developer/
                    //http://stackoverflow.com/questions/37790417/selenium-firefox-marionette-driver
                    //Marionette support is best in Firefox 48 and onwards, although the more recent the Firefox version, 
                    //the more bug fixes and features. Firefox 47 is explicitly not supported.

                    // Set Marionette on so the Grid will use this instead of normal FirefoxDriver
                    //DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
                    //capabilities.SetCapability("marionette", true);
                    //_driver = new RemoteWebDriver(capabilities); 

                    //var options = new FirefoxOptions();
                    //options.AddAdditionalCapability("marionette", true);
                    //options.IsMarionette = true;

                    //driver = new FirefoxDriver(options);

                    string geckoDriverPath = @"D:\Installers\Selenium\WebDrivers\wires.exe";
                    Environment.SetEnvironmentVariable("webdriver.gecko.driver", geckoDriverPath);

                    string firefoxPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

                    // Dev edition runs much slower - it does work tho ...
                    //string firefoxDeveloperPath = @"C:\Program Files (x86)\Firefox Developer Edition\firefox.exe";

                    var driverService = FirefoxDriverService.CreateDefaultService();
                    driverService.FirefoxBinaryPath = firefoxPath;
                    driverService.HideCommandPromptWindow = true;
                    driverService.SuppressInitialDiagnosticInformation = true;

                    try
                    {
                        driver = new FirefoxDriver(driverService, new FirefoxOptions(), TimeSpan.FromMinutes(1));
                    }
                    catch (Exception ex)
                    {
                        string e = ex.ToString();
                    }

                    break;

                default:
                case TestFixtureConfig.eBrowser.Chrome:
                    driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;

            }
            driver.Manage().Window.Maximize();
            return driver;
        }

        /// <summary>
        /// Set the desired capability
        /// An exception will be thrown if an empty value is provided and it is required
        /// </summary>
        /// <param name="capability"></param>
        /// <param name="value"></param>
        /// <param name="valueRequired"></param>
        private static void SetGridCapability(DesiredCapabilities caps, string capability, string value, bool valueRequired)
        {
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(capability, "capability");
            if (valueRequired)
            {
                QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(value, string.Format("Capability ({0})", capability));
            }
            caps.SetCapability(capability, value ?? string.Empty);
        }

    }
}
