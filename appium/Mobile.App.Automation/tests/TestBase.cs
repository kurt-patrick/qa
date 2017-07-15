using KPE.Mobile.App.Automation.Common;
using KPE.Mobile.App.Automation.Exceptions;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.QA;
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

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
// This will cause log4net to look for a configuration file
// called ConsoleApp.exe.config in the application base
// directory (i.e. the directory containing ConsoleApp.exe)

namespace KPE.Mobile.App.Automation.Tests
{

    [TestFixtureSource("GetTestFixtureSourceData")]
    public abstract class TestBase
    {
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
            // - It extract the NUnit params from the command line
            // - It Applies the additional driver capabilities from the settings file
            // - It Applies the URL to from the settings file
            _testCaseSettings = new TestCaseSettings(testFixtureData);
            _driver = _testCaseSettings.GetWebDriver();

        }

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            //_driver = _testCaseSettings.GetWebDriver();
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
            string remoteKey = _testCaseSettings.GetCapability(TestCaseSettings.Capabilities_Remote);
            bool isSauceLabs = string.Equals("saucelabs", remoteKey, StringComparison.CurrentCultureIgnoreCase);
            if (isSauceLabs)
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Data for at least 1 test and driver combination otherwise an exception is raised</returns>
        public static IEnumerable<string> GetTestFixtureSourceData()
        {
            var retVal = new List<string>();

            const string TFS_KEY = "tfs";

            try
            {
                // If a config was specified from the command line, use that instead of defaults
                string path = "tfs.defaults.txt";
                if (TestContext.Parameters.Exists(TFS_KEY))
                {
                    path = TestContext.Parameters.Get(TFS_KEY);
                    _log.Info("Using test fixture source from command line: " + path);
                }

                // all whitespace lines, empty lines, and, lines starting with "//" will be ignored
                retVal =
                    EnvironmentHelper.GetFileContentsAsList(path)
                    .Where(line => !string.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith("//"))
                    .ToList();

                // Verify at least 1 web driver configuration is specified
                if(retVal.Count == 0)
                {
                    throw new InvalidStateException("At least 1 test fixture source value is required");
                }

            }
            catch (Exception ex)
            {
                _log.Error(ex);
                throw;
            }

            return retVal;
        }

    }
}

