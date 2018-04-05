using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KPE.Mobile.App.Automation.Configuration.Devices
{
    public static class DeviceFactory
    {
        private static List<Device> _allDevices = new List<Device>();

        public static List<Device> GetEnabledDevices()
        {
            return GetAllDevices().Where(device => !device.Disabled).ToList();
        }

        /// <summary>
        /// Returns all devices enabled and disabled
        /// </summary>
        /// <returns></returns>
        public static List<Device> GetAllDevices()
        {
            if(_allDevices.Count == 0)
            {
                var appDomain = System.AppDomain.CurrentDomain.GetAssemblies();

                var folderPath = Settings.Instance().DeviceCapabilitiesFolderPath;
                var filePaths = Directory.GetFiles(folderPath, "*.json");
                filePaths.ToList().ForEach(path =>
                {
                    _allDevices.Add(JsonConvert.DeserializeObject<Device>(File.ReadAllText(path)));
                });
            }
            return _allDevices;
        }

    }

    public class Device
    {
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; } = "";

        [JsonProperty("disabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool Disabled { get; set; } = false;

        [JsonProperty("useGrid", NullValueHandling = NullValueHandling.Ignore)]
        public bool UseGrid { get; set; } = false;

        [JsonProperty("capabilities", NullValueHandling = NullValueHandling.Ignore)]
        public List<Capability> Capabilities { get; set; } = new List<Capability>();
    }

}
