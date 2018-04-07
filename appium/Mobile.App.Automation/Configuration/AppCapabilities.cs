using KPE.Mobile.App.Automation.Configuration.Devices;
using KPE.Mobile.App.Automation.QA;
using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.Configuration
{
    public class AppCapabilities
    {
        public string AppPackage { get; private set; } = "";
        public string AppActivity { get; private set; } = "";
        public Device Device { get; private set; } = null;

        public List<Capability> Capabilities { get; private set; } = new List<Capability>();

        AppCapabilities(string appPackage, string appActivity)
        {
            StringQA.ThrowIfNullOrWhiteSpace(appPackage);
            StringQA.ThrowIfNullOrWhiteSpace(appActivity);
            AppPackage = appPackage;
            AppActivity = appActivity;
            Capabilities.Add(new Capability("appPackage", appPackage));
            Capabilities.Add(new Capability("appActivity", appActivity));
        }

        AppCapabilities(string appPackage, string appActivity, Device device) : this(appPackage, appActivity)
        {
            ObjectQA.ThrowIfNull(device);
            ObjectQA.ThrowIfIEnumerableIsEmpty(device.Capabilities);
            Device = device;
            Capabilities.AddRange(Device.Capabilities);
        }

        DesiredCapabilities DesiredCapabilities()
        {
            var retVal = new DesiredCapabilities();
            Capabilities.ForEach(cap => retVal.SetCapability(cap.Key, cap.Value));
            return retVal;
        }

        public static List<DesiredCapabilities> AutomationChallengeApp()
        {
            return BuildDesiredCapabilitesList("com.android.kpe.automationchallenge", ".MainActivity");
        }

        public static List<DesiredCapabilities> ChecklistApp()
        {
            return BuildDesiredCapabilitesList("jakiganicsystems.simplestchecklist", ".MainActivity");
        }

        public static List<DesiredCapabilities> SelendroidApp()
        {
            return BuildDesiredCapabilitesList("io.selendroid.testapp", ".HomeScreenActivity");
        }

        public static List<DesiredCapabilities> GmailApp()
        {
            return BuildDesiredCapabilitesList("com.google.android.gm", ".ConversationListActivityGmail");
        }

        private static List<DesiredCapabilities> BuildDesiredCapabilitesList(string appPackage, string appActivity)
        {
            var devices = DeviceFactory.GetEnabledDevices();
            if (devices.Count == 0)
            {
                throw new Exceptions.InvalidStateException("No enabled devices were found");
            }

            var retVal = new List<DesiredCapabilities>();
            devices.ForEach(device => retVal.Add(new AppCapabilities(appPackage, appActivity, device).DesiredCapabilities()));
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
