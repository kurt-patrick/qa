using KPE.Mobile.App.Automation.QA;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests
{
    public abstract class DriverCapabilitiesBase
    {
        private const string CapabilitiesFormatString = "device={0},deviceName={1},appPackage={2},appActivity={3}";

        public string Device { get; private set; } = "";
        public string DeviceName { get; private set; } = "";

        public DriverCapabilitiesBase(string device, string deviceName)
        {
            StringQA.ThrowIfNullOrWhiteSpace(device);
            StringQA.ThrowIfNullOrWhiteSpace(deviceName);
            Device = device;
            DeviceName = deviceName;
        }

        public string GetCapabilitiesString(string appPackage, string appActivity)
        {
            StringQA.ThrowIfNullOrWhiteSpace(appPackage);
            StringQA.ThrowIfNullOrWhiteSpace(appActivity);
            return string.Format(CapabilitiesFormatString, Device, DeviceName, appPackage, appActivity);
        }

        public List<string> GetCapabilitiesList(string appPackage, string appActivity)
        {
            return new List<string>() { GetCapabilitiesString(appPackage, appActivity) };
        }


    }
}
