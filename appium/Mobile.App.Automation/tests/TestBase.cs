using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Configuration.Devices;
using KPE.Mobile.App.Automation.Exceptions;
using KPE.Mobile.App.Automation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using System;
using System.Linq;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
// This will cause log4net to look for a configuration file
// called ConsoleApp.exe.config in the application base
// directory (i.e. the directory containing ConsoleApp.exe)

namespace KPE.Mobile.App.Automation.Tests
{
    public abstract class TestBase
    {
        public const string TestFixtureSourceName = "CapabilitiesList";

        private AppiumLocalService _appiumLocalService = null;
        protected AppiumDriver<IWebElement> _driver = null;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// For each entry in TestFixtureSource this constructor will be called once per line
        /// </summary>
        /// <param name="appCapabilities"></param>
        public TestBase(AppCapabilities appCapabilities)
        {
            var deviceName = appCapabilities.GetCapability("deviceName");

            // Check the android device is running
            InvalidStateException.ThrowIfFalse(
                ProcessHelper.IsAndroidDeviceRunning(deviceName), 
                "Android device is not running. deviceName=" + deviceName);

            if (appCapabilities.Device.UseGrid)
            {
                //_driver = DriverHelper.CreateAppiumWebDriver(appCapabilities, new Uri("http://0.0.0.0:4723/wd/hub"));
                _driver = DriverHelper.CreateAppiumWebDriver(appCapabilities.DesiredCapabilities(), new Uri(Settings.Instance().GridHubUri));
            }
            else
            {
                // Start the appium local service
                _appiumLocalService =
                    AppiumLocalServiceBuilder
                        .Build(appCapabilities.DesiredCapabilities())
                        .Start()
                        .AssertIsRunning(10);

                // Create the web driver for the specified device/emulator with desired caps
                _driver = DriverHelper.CreateAppiumWebDriver(appCapabilities.DesiredCapabilities(), _appiumLocalService.ServiceUrl);
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1);
        }

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
        }

        [SetUp]
        public virtual void TestSetup() { }

        [TearDown]
        public virtual void TestTearDown()
        {
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            //LogToConsole("TestContext.Parameters.Count: ");
            if (_driver != null)
            {
                // NOTE: The driver.quit statement is required, otherwise the test continues to execute, leading to a timeout.
                // https://www.browserstack.com/automate/c-sharp
                _driver.Quit();
            }

            ObjectHelper.TryDispose(_driver);
            ObjectHelper.TryDispose(_appiumLocalService);
        }

        protected static void LogToConsole(string text)
        {
            Console.WriteLine(string.Format("{0}: {1}", text, DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

    }
}

