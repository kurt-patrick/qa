﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KPE.Mobile.App.Automation.Tests.Capabilities
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
                // TODO: Get path from settings file
                var filePaths = Directory.GetFiles(@"D:\github\qa\appium\Mobile.App.Automation\Tests\Capabilities", "*.json");
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

        [JsonProperty("capabilities", NullValueHandling = NullValueHandling.Ignore)]
        public List<Capability> Capabilities { get; set; } = new List<Capability>();

    }

    public class Capability
    {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; } = "";

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; } = "";
    }
}
