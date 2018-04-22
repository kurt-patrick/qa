using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Configuration.Devices;
using KPE.Mobile.App.Automation.Exceptions;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
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

        protected Action OnTestSetupEventHandler { get; set; }
        private AppiumLocalService _appiumLocalService = null;
        protected AppiumDriver<IWebElement> _driver = null;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// For each entry in TestFixtureSource this constructor will be called once per line
        /// </summary>
        /// <param name="capabilities"></param>
        public TestBase(DesiredCapabilities capabilities)
        {
            var deviceName = capabilities.GetCapability("deviceName").ToString();

            // Check the android device is running
            InvalidStateException.ThrowIfFalse(
                ProcessHelper.IsAndroidDeviceRunning(deviceName),
                "Android device is not running. deviceName=" + deviceName);

            // Start an appium local service
            if (false == Settings.Instance().UseGrid)
            {
                _appiumLocalService =
                    AppiumLocalServiceBuilder
                        .Build(capabilities)
                        .Start()
                        .AssertIsRunning(10);
            }

            // Point to the grid else point directly to appium server
            var uri = (Settings.Instance().UseGrid) ? new Uri(Settings.Instance().GridHubUri) : _appiumLocalService.ServiceUrl;

            // Create the web driver for the specified device/emulator with desired caps
            _driver = DriverHelper.CreateAppiumWebDriver(capabilities, uri);
        }


        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
        }

        [SetUp]
        public void TestSetup()
        {
            // Put the app into a clean state
            _driver.CloseApp();
            _driver.LaunchApp();

            // Call the test setup at the fixture level
            OnTestSetupEventHandler?.Invoke();
        }

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

        protected T Get<T>() where T : PageBase
        {
            return Get<T>(false);
        }

        protected T Get<T>(bool assertLoaded) where T : PageBase
        {
            var obj = PageObjectFactory.Create<T>(_driver);
            Assert.IsTrue(obj.IsLoaded());
            return obj;
        }

    }
}

