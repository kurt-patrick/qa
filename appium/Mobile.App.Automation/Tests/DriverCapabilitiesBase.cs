using KPE.Mobile.App.Automation.QA;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests
{
    public abstract class DriverCapabilitiesBase
    {
        private const string CapabilitiesFormatString = "device={0},deviceName={1},appPackage={2},appActivity={3}";

        public string AppPackage { get; private set; } = null;
        public string Device { get; private set; } = null;
        public string DeviceName { get; private set; } = null;

        public DriverCapabilitiesBase(string device, string deviceName, string appPackage)
        {
            StringQA.ThrowIfNullOrWhiteSpace(device);
            StringQA.ThrowIfNullOrWhiteSpace(deviceName);
            StringQA.ThrowIfNullOrWhiteSpace(appPackage);
            Device = device;
            DeviceName = deviceName;
            AppPackage = appPackage;
        }

        public List<string> GetCapabilitiesString(string appActivity)
        {
            StringQA.ThrowIfNullOrWhiteSpace(appActivity);
            return new List<string>()
            {
                string.Format(CapabilitiesFormatString, Device, DeviceName, AppPackage, appActivity)
            };
        }


    }
}
