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

        [JsonProperty("deviceCapabilitiesFolderPath", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceCapabilitiesFolderPath { get; set; } = "";

        [JsonProperty("appiumHub", NullValueHandling = NullValueHandling.Ignore)]
        public string AppiumHubUri { get; set; } = "http://127.0.0.1:4723/wd/hub";

        [JsonProperty("defaultTimeOut", NullValueHandling = NullValueHandling.Ignore)]
        public int DefaultTimeOut { get; set; } = 20;

        public static Settings Instance()
        {
            if (_instance == null)
            {
                var assemblyFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
                var path = Path.Combine(assemblyFolder, "Configuration", "Settings.json");
                _instance = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
            }
            return _instance;
        }

    }
}
