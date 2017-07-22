using KPE.Mobile.App.Automation.QA;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests
{
    public abstract class DriverCapabilitiesBase
    {
        private const string CapabilitiesFormatString = "device={0},deviceName={1},appPackage={2},appActivity={3}";

        private string _device = null;
        private string _deviceName = null;
        private string _appPackage = null;

        public DriverCapabilitiesBase(string device, string deviceName, string appPackage)
        {
            StringQA.ThrowIfNullOrWhiteSpace(device);
            StringQA.ThrowIfNullOrWhiteSpace(deviceName);
            StringQA.ThrowIfNullOrWhiteSpace(appPackage);
            _device = device;
            _deviceName = deviceName;
            _appPackage = appPackage;
        }

        public List<string> GetCapabilitiesString(string appActivity)
        {
            StringQA.ThrowIfNullOrWhiteSpace(appActivity);
            return new List<string>()
            {
                string.Format(CapabilitiesFormatString, _device, _deviceName, _appPackage, appActivity)
            };
        }


    }
}
