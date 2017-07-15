using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.QA;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KPE.Mobile.App.Automation.Common
{
    public class TestCaseSettings
    {
        public const string Capabilities_Remote = "remote";
        public const string Capabilities_Device = "device";
        public const string Capabilities_DeviceName = "deviceName";
        //public const string Capabilities_AppiumDriver = "appiumDriver";
        //public const string Capabilities_AppiumVersion = "appiumVersion";
        //public const string Capabilities_AppiumServer = "appiumServer";

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public enum eKeyType
        {
            TestFixtureData = 0,
            NUnitParam = 1
        }

        public enum eDriverType
        {
            WebDriver,
            AppiumDriver,
            RemoteWebDriver
        }

        /// <summary>
        /// These are the parameters provided on the command line as part of the nunit3-console call e.g. (--p or --params)
        /// </summary>
        private Dictionary<string, string> _nunitParams = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        /// <summary>
        /// This is the unprocessed TestFixtureSource entry e.g. browser=chrome, remote=grid-local
        /// </summary>
        private string _testFixtureData = "";

        /// <summary>
        /// The is the processed TestFixtureSource entry split out into key value pair
        /// </summary>
        private Dictionary<string, string> _testFixtureDataMap = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

        private int _timeOut = 20;
        private string _baseUrl = null;
        private Dictionary<string, int> _timeOuts = new Dictionary<string, int>();
        private Dictionary<string, string> _urlMap = new Dictionary<string, string>();

       
        public TestCaseSettings(string testFixtureData)
        {
            StringQA.ThrowIfNullOrWhiteSpace(testFixtureData);

            _testFixtureData = testFixtureData;

            ParseCapabilitiesFromTestFixtureData();
            GetNUnitParams();
            LoadUrlsFromSettingFile();

            ApplySettingsFromNUnitParams();

            // Needs refactoring
            LoadAdditionalCapabilitesFromSettingsFile();

            // Precedence is, apply settings from NUnit command line, then, override with test case specific


        }

        private void LoadUrlsFromSettingFile()
        {
            var xmlDoc = LoadSettingsXmlDocument();

            XmlNodeList nodes = xmlDoc.SelectNodes("//urls/url");
            foreach (XmlNode node in nodes)
            {
                string key = GetXmlNodeAttribute(node, "key", "");
                _urlMap[key] = node.InnerText ?? "";
            }

            // There should be at least 1 url added to the settings file
            ObjectQA.ThrowIfIEnumerableIsEmpty(_urlMap);

            // Determine base url (Precedence: First match against 'default' ELSE first entry in dictionary)
            var defaultNode = xmlDoc.SelectSingleNode("//urls/url[@default='true']");
            if(defaultNode != null)
            {
                _baseUrl = _urlMap[GetXmlNodeAttribute(defaultNode, "key", "")];
            }
            else
            {
                _baseUrl = _urlMap[_urlMap.Keys.First()];
            }

        }

        private XmlDocument LoadSettingsXmlDocument()
        {
            string filePath = "settings.xml";
            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(EnvironmentHelper.GetBinFolderPath(), filePath);
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Cannot find settings file: " + filePath);
                }
            }

            // Load default settings file
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            return xmlDoc;
        }

        public Dictionary<string, string> GetCapabilitiesFromSettingsFile(string key)
        {
            StringQA.ThrowIfNullOrWhiteSpace(key);

            var xmlDoc = LoadSettingsXmlDocument();
            var retVal = new Dictionary<string, string>();
            string xpath = string.Format("//driverCapabilities/capabilities[@key='{0}']/capability", key);

            var nodes = xmlDoc.SelectNodes(xpath);
            foreach (XmlNode node in nodes)
            {
                string capName = GetXmlNodeAttribute(node, "name", "");
                string capValue = node.InnerText;
                bool isEnvVar = 
                    string.Equals(
                    "true", 
                    GetXmlNodeAttribute(node, "isEnvVar", "false"), 
                    StringComparison.CurrentCultureIgnoreCase);

                if (isEnvVar)
                {
                    capValue = EnvironmentHelper.GetEnvVariable(capValue);
                }

                // Throw an exception if value is not provided
                StringQA.ThrowIfNullOrWhiteSpace(capName);
                StringQA.ThrowIfNullOrWhiteSpace(capValue);

                retVal[capName] = capValue;
                
            }

            return retVal;
        }

        private string GetXmlNodeAttribute(XmlNode node, string name, string defaultValue)
        {
            ObjectQA.ThrowIfNull(node);
            ObjectQA.ThrowIfNull(defaultValue);

            string retVal = null;
            XmlAttribute attribute = node.Attributes[name];

            if(attribute != null) 
            {
                retVal = attribute.InnerText;
            }
            return retVal ?? defaultValue;

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

        public string GetAppiumUrl(string deviceName)
        {
            StringQA.ThrowIfNullOrWhiteSpace(deviceName);

            // Query the settings file for the specific appium/AndroidDriver or appium/IOSDriver URL
            var xmlDoc = LoadSettingsXmlDocument();
            var node = xmlDoc.SelectSingleNode(string.Format("//urls/appium/url[@key='{0}']", deviceName));
            if(node != null)
            {
                return node.InnerText;
            }

            // Fallback to the default
            return GetUrlFromKey("appium");
        }

        public string GetUrlFromKey(string key)
        {
            ObjectQA.ThrowIfIEnumerableDoesNotContainValue(_urlMap.Keys.ToList(), key);
            return _urlMap[key];
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set 
            {
                StringQA.ThrowIfNullOrWhiteSpace(value);

                if(_urlMap.ContainsKey(value))
                {
                    // set the url using the name/key from the settings file
                    _baseUrl = _urlMap[value];
                    _log.Debug(string.Format("BaseUrl (set) key := [{0}] url := [{1}]", value, _baseUrl));
                }
                else
                {
                    // An explicit url has been provided
                    _baseUrl = value;
                    _log.Debug("BaseUrl (set): " + _baseUrl);
                }

            }
        }

        public Dictionary<string, string> NUnitParams
        {
            get { return _nunitParams; }
            set { _nunitParams = value; }
        }

        private void GetNUnitParams()
        {
            // Parse the NUnit parameters passed via the command line
            foreach (string key in TestContext.Parameters.Names)
            {
                // Refactor with Add (which then handles the url stuff below)
                _nunitParams[key] = TestContext.Parameters.Get(key);
            }
        }

        private void ApplySettingsFromNUnitParams()
        {
            // Update Settings.BaseUrl if it has been provided on the command line
            string baseUrl = GetValueOrDefault(TestCaseSettings.eKeyType.NUnitParam, "url", "");
            if (!string.IsNullOrWhiteSpace(baseUrl))
            {
                BaseUrl = baseUrl;
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

        private void LoadAdditionalCapabilitesFromSettingsFile()
        {
            // Add additional capabilities from the Settings file
            // - this includes username and access keys for Hosted Grids e.g. SauceLabs and BrowserStack
            // - any additional capabilites that are to be added

            /*
             *  SELF NOTE: Probably want to refactor this to work not only for Remote drivers, but for all drivers based on key
             */
            var remoteDriverKey = GetCapability(Capabilities_Remote);
            if (remoteDriverKey.Length > 0)
            {
                var settingsFileCapabilities = GetCapabilitiesFromSettingsFile(remoteDriverKey);
                foreach (string key in settingsFileCapabilities.Keys)
                {
                    _testFixtureDataMap[key] = settingsFileCapabilities[key];
                }
            }
        }

        public eDriverType GetDriverType()
        {
            return eDriverType.AppiumDriver;
            /*
            if (_testFixtureDataMap.ContainsKey(Capabilities_Remote))
            {
                return eDriverType.RemoteWebDriver;
            }

            if (_testFixtureDataMap.ContainsKey(Capabilities_AppiumVersion))
            {
                return eDriverType.AppiumDriver;
            }

            return eDriverType.WebDriver;
            */
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

        public bool IsRemoteDriver()
        {
            return !string.IsNullOrWhiteSpace(GetCapability(Capabilities_Remote));
        }

        public string GetCapability(string key)
        {
            return GetValueOrDefault(eKeyType.TestFixtureData, key, "");
        }

        public string GetValueOrDefault(eKeyType keyType, string key, string defaultValue)
        {
            var dict = (keyType == eKeyType.TestFixtureData) ? _testFixtureDataMap : _nunitParams;
            return GetValueOrDefault(dict, key, defaultValue);
        }

        private string GetValueOrDefault(Dictionary<string, string> dict, string key, string defaultValue)
        {
            ObjectQA.ThrowIfNull(key);
            ObjectQA.ThrowIfNull(defaultValue);

            if(dict.ContainsKey(key))
            {
                return dict[key];
            }

            return defaultValue;
        }

        private IWebDriver _driver = null;

        /// <summary>
        /// Creates the web driver based on the the test fixture data provided
        /// </summary>
        /// <returns>Web driver object based on the the test fixture data</returns>
        public IWebDriver GetWebDriver()
        {
            return _driver ?? CreateWebDriver();
        }

        private IWebDriver CreateWebDriver()
        {
            try
            {
                // RemoteWebDriver (SauceLabs, BrowserStack, Selenium Grid Local)
                // Appium (AndroidDriver, IOSDriver)
                return _driver = IsRemoteDriver() ? CreateRemoteWebDriver() : CreateAppiumWebDriver();

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
        /// Remote Web Driver
        /// - SauceLabs
        /// - BrowserStack
        /// - Selenium Grid running on the local network
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateRemoteWebDriver()
        {
            var capabilities = GetDesiredCapabilities();
            string uri = GetUrlFromKey(GetCapability(Capabilities_Remote));
            return DriverHelper.CreateRemoteWebDriver(uri, capabilities);
        }

        /// <summary>
        /// Appium Driver
        /// - AndroidDriver
        /// - IOSDriver
        /// </summary>
        /// <returns></returns>
        private IWebDriver CreateAppiumWebDriver()
        {
            // Get the driver type (android, iOS) from the test fixture source
            // Get the Appium Node Server URL from the Settings file
            string device = GetValueOrDefault(TestCaseSettings.eKeyType.TestFixtureData, TestCaseSettings.Capabilities_Device, "");
            string deviceName = GetValueOrDefault(TestCaseSettings.eKeyType.TestFixtureData, TestCaseSettings.Capabilities_DeviceName, "");
            string appiumServerUrl = GetAppiumUrl(deviceName);

            var capabilities = GetDesiredCapabilities();
            return DriverHelper.CreateAppiumWebDriver(device, appiumServerUrl, capabilities);

        }

    }
}
