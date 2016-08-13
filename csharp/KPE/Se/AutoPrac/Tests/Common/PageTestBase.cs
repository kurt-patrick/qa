using KPE.Se.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.Tests.Common
{
    [TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_FireFox, PageTestBase.OS_Windows_10)]
    [TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_IE, PageTestBase.OS_Windows_10)]
    [TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_Chrome, PageTestBase.OS_Windows_10)]
    //[TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_FireFox, PageTestBase.OS_Windows_8_1)]
    //[TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_IE, PageTestBase.OS_Windows_8_1)]
    //[TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_Chrome, PageTestBase.OS_Windows_8_1)]
    //[TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_FireFox, PageTestBase.OS_Windows_7)]
    //[TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_IE, PageTestBase.OS_Windows_7)]
    //[TestFixture(GridProvider_SauceLabs, PageTestBase.Browser_Chrome, PageTestBase.OS_Windows_7)]
    //[TestFixture(PageTestBase.Browser_Chrome)]
    //[TestFixture(PageTestBase.Browser_FireFox)]
    //[TestFixture(PageTestBase.Browser_IE)]
    public abstract class PageTestBase
    {
        // Browser / WebDriver Type
        private const string Browser_Chrome = "Chrome";
        private const string Browser_FireFox = "Firefox";
        private const string Browser_IE = "Internet Explorer";
        private const string Browser_MicrosoftEdge = "MicrosoftEdge";

        // OS
        private const string OS_Windows_10 = "Windows 10";
        private const string OS_Windows_8_1 = "Windows 8.1";
        private const string OS_Windows_8 = "Windows 8";
        private const string OS_Windows_7 = "Windows 7";
        private const string OS_Windows_XP = "Windows XP";
        private const string OS_Windows_Linux = "Linux";

        // SauceLabs credentials
        private const string SauceLabs_Username = "c3074723";
        private const string SauceLabs_Access_Key = "ec51db5f-6376-47c4-aa21-311e21026bc4";

        // Grid Providers
        private const string GridProvider_None = "none";
        private const string GridProvider_SauceLabs = "saucelabs";
        private const string GridProvider_BrowserStack = "browserstack";

        private enum eConfigSetting
        {
            BrowserName,
            BrowserVersion,
            OperatingSystem,
            GridProvider,
            DeviceName,
            DeviceOrientation,
            Username,
            AccessKey,
        }

        protected IWebDriver _driver = null;
        private Dictionary<eConfigSetting, string> _configSetting = new Dictionary<eConfigSetting, string>();

        public PageTestBase()
        {
        }

        /// <summary>
        /// Use this constructor if you want to run tests locally
        /// </summary>
        /// <param name="browserName"></param>
        public PageTestBase(string browserName)
            : this(GridProvider_None, browserName, string.Empty, string.Empty, string.Empty, string.Empty)
        {

        }

        public PageTestBase(string gridProvider, string browserName, string os) 
            : this(gridProvider, browserName, string.Empty, os, string.Empty, string.Empty)
        {
            
        }

        public PageTestBase(string gridProvider, string browserName, string browserVersion, string os, string deviceName, string deviceOrientation)
        {
            SetConfigSetting(eConfigSetting.GridProvider, gridProvider);
            SetConfigSetting(eConfigSetting.BrowserName, browserName);
            SetConfigSetting(eConfigSetting.BrowserVersion, browserVersion);
            SetConfigSetting(eConfigSetting.OperatingSystem, os);
            SetConfigSetting(eConfigSetting.DeviceName, deviceName);
            SetConfigSetting(eConfigSetting.DeviceOrientation, deviceOrientation);

            // Set usernames/passwords/accesskeys/etc...
            // This could all be moved to the command line ...
            switch (GetConfigSetting(eConfigSetting.GridProvider))
            {
                case GridProvider_SauceLabs:
                    SetConfigSetting(eConfigSetting.Username, SauceLabs_Username);
                    SetConfigSetting(eConfigSetting.AccessKey, SauceLabs_Access_Key);
                    break;

                case GridProvider_BrowserStack:
                break;

                default:
                break;

            }

            
        }

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            //http://relevantcodes.com/using-nunit-to-execute-selenium-webdriver-tests/
            //http://www.seleniumhq.org/docs/03_webdriver.jsp#driver-specifics-and-tradeoffs

            //http://stackoverflow.com/questions/16276278/nunit-test-setup-method-with-argument

            var gridProvider = GetConfigSetting(eConfigSetting.GridProvider);

            LogToConsole("TestFixtureSetup");
            LogToConsole("gridProvider: " + gridProvider);
            switch (gridProvider)
            {
                case GridProvider_SauceLabs:
                    ConfigureForSauceLabs();
                    break;

                case GridProvider_BrowserStack:
                    ConfigureForBrowserStack();
                    break;

                default:
                    ConfigureForLocalDriver();
                    break;
            }

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
        private void ConfigureForLocalDriver()
        {
            string browserName = GetConfigSetting(eConfigSetting.BrowserName);
            switch (browserName)
            {
                case Browser_Chrome:
                    _driver = new OpenQA.Selenium.Chrome.ChromeDriver();
                    break;

                case Browser_IE:
                    _driver = new InternetExplorerDriver();
                    break;

                case Browser_FireFox:
                default:
                    _driver = new FirefoxDriver();
                    break;

            }
            _driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Configure the driver for sauce labs grid
        /// https://wiki.saucelabs.com/display/DOCS/Platform+Configurator#/
        /// https://github.com/saucelabs-sample-test-frameworks/CSharp-Nunit-Selenium/blob/master/ParallelSelenium/ParallelSearchTests.cs
        /// </summary>
        private void ConfigureForSauceLabs()
        {
            DesiredCapabilities caps = new DesiredCapabilities();

            // Required
            SetGridCapability(caps, CapabilityType.BrowserName, GetConfigSetting(eConfigSetting.BrowserName), true);
            SetGridCapability(caps, CapabilityType.Platform, GetConfigSetting(eConfigSetting.OperatingSystem), true);
            SetGridCapability(caps, "username", GetConfigSetting(eConfigSetting.Username), true);
            SetGridCapability(caps, "accessKey", GetConfigSetting(eConfigSetting.AccessKey), true);

            // Optional
            SetGridCapability(caps, CapabilityType.Version, GetConfigSetting(eConfigSetting.BrowserVersion), false);
            SetGridCapability(caps, "deviceName", GetConfigSetting(eConfigSetting.DeviceName), false);
            SetGridCapability(caps, "deviceOrientation", GetConfigSetting(eConfigSetting.DeviceOrientation), false);
            SetGridCapability(caps, "name", TestContext.CurrentContext.Test.Name, false);


            _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"), caps, TimeSpan.FromSeconds(600));
        }

        private void ConfigureForBrowserStack()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gets the grid capability from the dictionary else returns string empty
        /// </summary>
        /// <param name="desiredCapabilities"></param>
        /// <param name="capability"></param>
        /// <param name="value"></param>
        /// <returns>If exists in dict, the value, else, empty string</returns>
        private string GetConfigSetting(eConfigSetting capability, bool throwIfNotExists = false)
        {
            string retVal = null;
            if(_configSetting.ContainsKey(capability))
            {
                retVal = _configSetting[capability];
            }
            else
            {
                if(throwIfNotExists)
                {
                    throw new ArgumentOutOfRangeException("Capability key not set: " + capability.ToString());
                }
            }
            return retVal ?? string.Empty;
        }

        private void SetConfigSetting(eConfigSetting capability, string value)
        {
            _configSetting[capability] = value ?? string.Empty;
        }

        /// <summary>
        /// Set the desired capability
        /// An exception will be thrown if an empty value is provided and it is required
        /// </summary>
        /// <param name="capability"></param>
        /// <param name="value"></param>
        /// <param name="valueRequired"></param>
        private void SetGridCapability(DesiredCapabilities caps, string capability, string value, bool valueRequired)
        {
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(capability);
            if (valueRequired)
            {
                QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(value);
            }
            caps.SetCapability(capability, value ?? string.Empty);
        }

        [SetUp]
        public abstract void TestSetup();

        [TearDown]
        public void TestTearDown()
        {
            LogToConsole("TestTearDown");
            NotifySaucelabsOfTestResult();
            TearDown();
        }

        private void NotifySaucelabsOfTestResult()
        {
            if (GetConfigSetting(eConfigSetting.GridProvider).Equals(GridProvider_SauceLabs))
            {
                bool passed = TestContext.CurrentContext.Result.FailCount == 0;
                try
                {
                    // Logs the result to Sauce Labs
                    ((IJavaScriptExecutor)_driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
                }
                catch
                {
                }
            }
        }

        public abstract void TearDown();

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            LogToConsole("TestFixtureTearDown");
            if (_driver != null)
            {
                _driver.Close();
                _driver.Dispose();
            }
        }

        protected void LogToConsole(string text)
        {
            Console.WriteLine(string.Format("{0}: {1}", text, DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

    }
}
