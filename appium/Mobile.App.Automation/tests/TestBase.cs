using KPE.Mobile.App.Automation.Common;
using NUnit.Framework;
using OpenQA.Selenium;
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

        protected IWebDriver _driver = null;
        protected TestCaseSettings _testCaseSettings = null;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// For each entry in TestFixtureSource this constructor will be called once per line
        /// </summary>
        /// <param name="testFixtureData"></param>
        public TestBase(string testFixtureData)
        {
            _log.Info("testFixtureData: " + testFixtureData);

            // *** This object is very important, because:
            // - It parses the TestFixtureSource data that specifies which driver to use
            // - It Applies the additional driver capabilities from the settings file
            // - It Applies the URL to from the settings file
            _testCaseSettings = new TestCaseSettings(testFixtureData);
            _driver = _testCaseSettings.GetWebDriver();

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
                // Warning: The driver.quit statement is required, otherwise the test continues to execute, leading to a timeout.
                // https://www.browserstack.com/automate/c-sharp
                _driver.Quit();
                _driver.Dispose();
            }
        }

        protected static void LogToConsole(string text)
        {
            Console.WriteLine(string.Format("{0}: {1}", text, DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

    }
}

