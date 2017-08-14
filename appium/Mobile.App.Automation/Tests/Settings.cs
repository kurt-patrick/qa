using Newtonsoft.Json;
using System.IO;

namespace KPE.Mobile.App.Automation.Tests
{
    public class Settings
    {
        private static Settings _instance = null;
        public static Settings Instance()
        {
            if(_instance == null)
            {
                var assemblyFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
                var path = Path.Combine(assemblyFolder, "Tests", "Settings.json");

                try
                {
                    _instance = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(path));
                }
                catch (System.Exception ex)
                {
                }

            }
            return _instance;
        }

        private Settings()
        {
        }

        [JsonProperty("deviceCapabilitiesFolderPath", NullValueHandling = NullValueHandling.Ignore)]
        public string DeviceCapabilitiesFolderPath { get; set; } = "";

        [JsonProperty("appiumHub", NullValueHandling = NullValueHandling.Ignore)]
        public string AppiumHubUri { get; set; } = "http://127.0.0.1:4723/wd/hub";

    }
}
