using KPE.Mobile.App.Automation.Configuration.Devices;
using KPE.Mobile.App.Automation.QA;
using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.Configuration
{
    public class DriverCapabilities
    {
        public string AppPackage { get; private set; } = "";
        public string AppActivity { get; private set; } = "";

        public List<Capability> Capabilities { get; private set; } = new List<Capability>();

        public DriverCapabilities(string appPackage, string appActivity)
        {
            StringQA.ThrowIfNullOrWhiteSpace(appPackage);
            StringQA.ThrowIfNullOrWhiteSpace(appActivity);
            AppPackage = appPackage;
            AppActivity = appActivity;
            Capabilities.Add(new Capability("appPackage", appPackage));
            Capabilities.Add(new Capability("appActivity", appActivity));
        }

        public DriverCapabilities(string appPackage, string appActivity, Device device) : this(appPackage, appActivity)
        {
            ObjectQA.ThrowIfNull(device);
            ObjectQA.ThrowIfIEnumerableIsEmpty(device.Capabilities);
            Capabilities.AddRange(device.Capabilities);
        }

        /// <summary>
        /// Generate the capability string passed to the WebDriver
        /// </summary>
        /// <param name="device"></param>
        /// <returns>device={0},deviceName={1},appPackage={2},appActivity={3}</returns>
        private string BuildDeviceSpecificCapabilitiesString(Device device)
        {
            var retVal = new System.Text.StringBuilder();

            // Device specific capabilities
            device.Capabilities
                .ForEach(cap => retVal.Append($"{cap.Key}={cap.Value}, "));

            // App specific capabilities
            retVal.Append($"appPackage={AppPackage}, appActivity={AppActivity}");

            return retVal.ToString();
        }

        public DesiredCapabilities DesiredCapabilities()
        {
            var retVal = new DesiredCapabilities();
            Capabilities.ForEach(cap => retVal.SetCapability(cap.Key, cap.Value));
            return retVal;
        }

        /// <summary>
        /// For each enabled device file generate the capabilities string
        /// </summary>
        /// <returns></returns>
        /*
        public List<Device> EnabledDevicesCapabilitiesStrings()
        {
            var devices = DeviceFactory.GetEnabledDevices();
            if(devices.Count == 0)
            {
                throw new Exceptions.InvalidStateException("No enabled devices were found");
            }

            var retVal = new List<string>();
            devices.ForEach(device => retVal.Add(BuildDeviceSpecificCapabilitiesString(device)));
            return null;
        }
        */

        public static List<DriverCapabilities> ChecklistApp()
        {
            return CreateCapabilitesForApp("jakiganicsystems.simplestchecklist", ".MainActivity");
        }

        public static List<DriverCapabilities> SelendroidApp()
        {
            return CreateCapabilitesForApp("io.selendroid.testapp", ".HomeScreenActivity");
        }

        public static List<DriverCapabilities> GmailApp()
        {
            return CreateCapabilitesForApp("com.google.android.gm", ".ConversationListActivityGmail");
        }

        private static List<DriverCapabilities> CreateCapabilitesForApp(string appPackage, string appActivity)
        {
            var devices = DeviceFactory.GetEnabledDevices();
            if (devices.Count == 0)
            {
                throw new Exceptions.InvalidStateException("No enabled devices were found");
            }

            var retVal = new List<DriverCapabilities>();
            devices.ForEach(device => retVal.Add(new DriverCapabilities(appPackage, appActivity, device)));
            return retVal;
        }

        public string GetCapability(string key)
        {
            StringQA.ThrowIfNullOrWhiteSpace(key);
            var capability = Capabilities.First(cap => key.Equals(cap.Key));
            return capability.Value;
        }

        public bool HasCapability(string key)
        {
            StringQA.ThrowIfNullOrWhiteSpace(key);
            return Capabilities.Any(cap => key.Equals(cap.Key));
        }

    }

    public class Capability
    {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; } = "";

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; } = "";

        public Capability()
        {
        }

        public Capability(string key, string value)
        {
            StringQA.ThrowIfNullOrWhiteSpace(key);
            ObjectQA.ThrowIfNull(value);
            Key = key;
            Value = value;
        }

    }

}
