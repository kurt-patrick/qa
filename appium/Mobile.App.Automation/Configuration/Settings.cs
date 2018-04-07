using Newtonsoft.Json;
using System.IO;

namespace KPE.Mobile.App.Automation.Configuration
{
    public class Settings
    {
        private static Settings _instance = null;

        private Settings()
        {
        }

        [JsonProperty("appiumHubUri", NullValueHandling = NullValueHandling.Ignore)]
        public string GridHubUri { get; set; } = "";

        [JsonProperty("commandTimeOut", NullValueHandling = NullValueHandling.Ignore)]
        public int CommandTimeOut { get; set; } = 30;

        [JsonProperty("deviceCapabilitiesFolderPath", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceCapabilitiesFolderPath { get; set; } = "";

        /// <summary>
        /// Implicit Wait to use for elements in ms
        /// </summary>
        [JsonProperty("implicitWait", NullValueHandling = NullValueHandling.Ignore)]
        public int ImplicitWait { get; set; } = 100;

        [JsonProperty("useGrid", NullValueHandling = NullValueHandling.Ignore)]
        public bool UseGrid { get; set; } = false;

        /// <summary>
        /// Timeout to use for WebDriverWait in seconds
        /// </summary>
        [JsonProperty("webDriverWaitTimeOut", NullValueHandling = NullValueHandling.Ignore)]
        public int WebDriverWaitTimeOut { get; set; } = 60;

        public static Settings Instance()
        {
            if (_instance == null)
            {
                var path = Path.Combine(AssemblyFolderPath(), "Configuration", "Settings.json");
                _instance = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
            }
            return _instance;
        }

        public static string AssemblyFolderPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
        }

    }
}
