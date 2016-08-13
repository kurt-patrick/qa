using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.Common
{
    public class TestFixtureConfig
    {
        #region enums
        public enum eSetting
        {
            BrowserName,
            BrowserVersion,
            DeviceName,
            DeviceOrientation,
            GridProvider,
            OS,
            OSVersion,
            Resolution
        }

        public enum eGridProvider
        {
            None,
            SauceLabs,
            BrowserStack
        }

        public enum eBrowser
        {
            NotSet,
            Chrome,
            Edge,
            FireFox,
            InternetExplorer
        }
        #endregion

        #region constants
        private const string ConfigurationFile = "testfixture.config";
        #endregion

        #region fields
        private static List<TestFixtureConfig> _testFixtureConfigs = null;
        private Dictionary<eSetting, string> _settings = new Dictionary<eSetting, string>();

        private static Dictionary<string, eBrowser> _browserMappings
            = new Dictionary<string, eBrowser>(StringComparer.CurrentCultureIgnoreCase)
            {
                { "chrome", eBrowser.Chrome },
                { "edge", eBrowser.Edge }, { "MicrosoftEdge", eBrowser.Edge },
                { "firefox", eBrowser.FireFox }, { "ff", eBrowser.FireFox },
                { "ie", eBrowser.InternetExplorer }, { "iexplorer", eBrowser.InternetExplorer }, 
                { "internet explorer", eBrowser.InternetExplorer },
            };
        #endregion

        #region properties
        public eBrowser Browser
        {
            get
            {
                string browser = GetSetting(eSetting.BrowserName);
                if (_browserMappings.ContainsKey(browser))
                {
                    return _browserMappings[browser];
                }
                return eBrowser.NotSet;
            }
        }

        public eGridProvider GridProvider
        {
            get
            {
                eGridProvider retVal = eGridProvider.None;
                Enum.TryParse(GetSetting(eSetting.GridProvider), true, out retVal);
                return retVal;
            }
        }

        public string BrowserName { get { return GetSetting(eSetting.BrowserName); } }
        public string BrowserVersion { get { return GetSetting(eSetting.BrowserVersion); } }
        public string DeviceName { get { return GetSetting(eSetting.DeviceName); } }
        public string DeviceOrientation { get { return GetSetting(eSetting.DeviceOrientation); } }
        public string OS { get { return GetSetting(eSetting.OS); } }
        public string OSVersion { get { return GetSetting(eSetting.OSVersion); } }
        public string Resolution { get { return GetSetting(eSetting.Resolution); } }
        #endregion

        #region constructors
        private TestFixtureConfig()
        {
        }

        private TestFixtureConfig(string line)
        {
            ParseConfigSettings(line);
        }
        #endregion

        #region methods
        private static TestFixtureConfig GetDefaultConfig()
        {
            var retVal = new TestFixtureConfig();
            retVal.AddConfig(eSetting.BrowserName, "Chrome");
            return retVal;
        }

        private string GetSetting(eSetting key)
        {
            if (_settings.ContainsKey(key))
            {
                return _settings[key];
            }
            return string.Empty;
        }

        private static List<TestFixtureConfig> ParseConfigsFromFile(string filename)
        {
            var retVal = new List<TestFixtureConfig>();

            string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Helpers.ReportHelper.LogToConsole("GetCurrentDirectory: " + currentDir);

            string path = System.IO.Path.Combine(currentDir, filename);
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    if(!string.IsNullOrWhiteSpace(line))
                    {
                        var config = new TestFixtureConfig(line);
                        if (config._settings.Count > 0)
                        {
                            retVal.Add(config);
                        }
                    }
                }
            }
            return retVal;
        }

        private void ParseConfigSettings(string line)
        {
            eSetting configKey = eSetting.BrowserName;
            var settingPair = line.Split(',');
            foreach (string kvp in settingPair)
            {
                var tmpArr = kvp.Split('=');
                if (tmpArr.Length >= 2)
                {
                    // NOTE: Not using TryParse, this way the user knows immediately if they have filled out the config incorrectly
                    configKey = (eSetting) Enum.Parse(typeof(eSetting), tmpArr[0].Trim(), true);
                    AddConfig(configKey, tmpArr[1]);
                }
            }
        }

        private TestFixtureConfig AddConfig(eSetting key, string value)
        {
            _settings[key] = value.Trim();
            return this;
        }

        /// <summary>
        /// Precedence and fall back
        /// 1. Looks for a default config file default.config
        /// 2. Returns a default config of Chrome
        /// </summary>
        /// <returns></returns>
        public static IEnumerable FixtureParms()
        {
            return _testFixtureConfigs ?? LoadTestFixtureConfigs();
        }

        private static List<TestFixtureConfig> LoadTestFixtureConfigs()
        {
            // Attempt to Load the test fixture configuration from the config file
            _testFixtureConfigs = ParseConfigsFromFile(ConfigurationFile);

            // If no configuration exists - use the default config of Chrome
            if (_testFixtureConfigs.Count == 0)
            {
                _testFixtureConfigs.Add(GetDefaultConfig());
            }

            return _testFixtureConfigs;
        }
        #endregion
    }
}
