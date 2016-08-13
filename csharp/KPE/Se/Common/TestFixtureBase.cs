using KPE.Se.Common;
using KPE.Se.Common.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KPE.Se.Common
{
    [Parallelizable()]
    [TestFixtureSource(typeof(TestFixtureConfig), "FixtureParms")]
    public abstract class TestFixtureBase
    {
        protected IWebDriver _driver = null;
        protected TestFixtureConfig _testFixtureConfig = null;

        public TestFixtureBase(TestFixtureConfig configuration)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(configuration);
            _testFixtureConfig = configuration;
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            LogToConsole("OneTimeSetup");
            LogToConsole("gridProvider: " + _testFixtureConfig.GridProvider.ToString());

            _driver = DriverHelper.CreateDriver(_testFixtureConfig);
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
            if (_testFixtureConfig.GridProvider == TestFixtureConfig.eGridProvider.SauceLabs)
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
        public void OneTimeTearDown()
        {
            LogToConsole("OneTimeTearDown");
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        protected void LogToConsole(string text)
        {
            Console.WriteLine(string.Format("{0}: ({1})", text, DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

        protected void LogDebugBrowserInfo()
        {
            LogToConsole(string.Format("Browser: ({0}) Grid Provider: ({1})", _testFixtureConfig.BrowserName, _testFixtureConfig.GridProvider.ToString()));
        }

    }//TestFixtureBase

}//namespace
