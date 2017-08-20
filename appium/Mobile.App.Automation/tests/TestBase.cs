using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using System;

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
        /// <param name="caps"></param>
        public TestBase(DriverCapabilities caps)
        {
            AppiumLocalServiceHelper
                .Build(caps, out _appiumLocalService)
                .Start()
                .WaitForIsRunning(10)
                .AssertIsRunning();

            _driver = DriverHelper.CreateAppiumWebDriver(caps, _appiumLocalService.ServiceUrl);
        }

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
        }

        [SetUp]
        public virtual void TestSetup() { }

        [TearDown]
        public void TestTearDown()
        {
            LogToConsole("TestTearDown");
            TearDown();
        }

        public virtual void TearDown()
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

