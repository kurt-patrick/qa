using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.QA;
using KPE.Mobile.App.Automation.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace KPE.Mobile.App.Automation.Common
{
    public class TestCaseSettings
    {
        public const string Capabilities_Device = "device";
        public const string Capabilities_DeviceName = "deviceName";

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// This is the unprocessed TestFixtureSource entry e.g. browser=chrome, remote=grid-local
        /// </summary>
        private string _testFixtureData = "";

        private int _timeOut = 20;

        Dictionary<string, string> _testFixtureDataMap = new Dictionary<string, string>();

        public TestCaseSettings(string testFixtureData)
        {
            StringQA.ThrowIfNullOrWhiteSpace(testFixtureData);

            _testFixtureData = testFixtureData;

            ParseCapabilitiesFromTestFixtureData();
        }

        public int DefaultTimeOut
        {
            get { return _timeOut; }
            set 
            { 
                if(value < 1)
                {
                    throw new ArgumentOutOfRangeException("value", "Must be > 0");
                }
                _timeOut = value; 
            }
        }

        private void ParseCapabilitiesFromTestFixtureData()
        {
            // TestFixtureSource values are provided as comma delimited
            var lines = _testFixtureData.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            // Build up the collection of capabilities values
            _testFixtureDataMap.Clear();
            foreach (string line in lines)
            {
                var capability = line.Split('=');
                if (capability.Length == 2 && !string.IsNullOrWhiteSpace(capability[0]))
                {
                    _testFixtureDataMap[capability[0].Trim()] = capability[1].Trim();
                }
            }

        }

        public DesiredCapabilities GetDesiredCapabilities()
        {
            DesiredCapabilities retVal = new DesiredCapabilities();

            foreach (string capability in _testFixtureDataMap.Keys)
            {
                retVal.SetCapability(capability, _testFixtureDataMap[capability]);
            }

            return retVal;
        }

        public bool ContainsKey(string key)
        {
            StringQA.ThrowIfNullOrWhiteSpace(key);
            return _testFixtureDataMap.ContainsKey(key);
        }

        public string GetCapability(string key)
        {
            return ContainsKey(key) ? _testFixtureDataMap[key] : "";
        }

        private AppiumDriver<IWebElement> _driver = null;

        /// <summary>
        /// Creates the web driver based on the the test fixture data provided
        /// </summary>
        /// <returns>Web driver object based on the the test fixture data</returns>
        public AppiumDriver<IWebElement> GetWebDriver()
        {
            return _driver ?? CreateWebDriver();
        }

        private AppiumDriver<IWebElement> CreateWebDriver()
        {
            try
            {
                // Appium (AndroidDriver, IOSDriver)
                return _driver = CreateAppiumWebDriver();

            }
            catch (Exception ex)
            {
                string errorMsg = string.Format(
                        "Failed to create Driver. Capabilities: [{0}]",
                        _testFixtureData);
                _log.Error(errorMsg, ex);
                throw;
            }
        }

        /// <summary>
        /// Appium Driver
        /// - AndroidDriver
        /// - IOSDriver
        /// </summary>
        /// <returns></returns>
        private AppiumDriver<IWebElement> CreateAppiumWebDriver()
        {
            // Get the driver type (android, iOS) from the test fixture source
            // Get the Appium Node Server URL from the Settings file
            string device = GetCapability(Capabilities_Device);
            string deviceName = GetCapability(Capabilities_DeviceName);
            string appiumServerUrl = Settings.Instance().AppiumHubUri;

            var capabilities = GetDesiredCapabilities();
            return DriverHelper.CreateAppiumWebDriver(device, appiumServerUrl, capabilities);

        }

    }
}
