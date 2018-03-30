﻿using Newtonsoft.Json;
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

        [JsonProperty("defaultTimeOut", NullValueHandling = NullValueHandling.Ignore)]
        public int DefaultTimeOut { get; set; } = 20;

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
