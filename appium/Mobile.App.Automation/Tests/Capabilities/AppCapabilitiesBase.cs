using KPE.Mobile.App.Automation.QA;
using KPE.Mobile.App.Automation.Tests.Capabilities;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests
{
    public abstract class AppCapabilitiesBase
    {
        public string AppPackage { get; private set; } = "";
        public string AppActivity { get; private set; } = "";

        public AppCapabilitiesBase(string appPackage, string appActivity)
        {
            StringQA.ThrowIfNullOrWhiteSpace(appPackage);
            StringQA.ThrowIfNullOrWhiteSpace(appActivity);
            AppPackage = appPackage;
            AppActivity = appActivity;
        }

        /// <summary>
        /// Generate the capability string passed to the WebDriver
        /// </summary>
        /// <param name="device"></param>
        /// <returns>device={0},deviceName={1},appPackage={2},appActivity={3}</returns>
        private string GetCapabilitiesString(Device device)
        {
            var retVal = new System.Text.StringBuilder();

            // Device specific capabilities
            device.Capabilities
                .ForEach(cap => retVal.Append($"{cap.Key}={cap.Value}, "));

            // App specific capabilities
            retVal.Append($"appPackage={AppPackage}, appActivity={AppActivity}");

            return retVal.ToString();
        }

        /// <summary>
        /// For each enabled device file generate the capabilities string
        /// </summary>
        /// <returns></returns>
        public List<string> GetCapabilitiesList()
        {
            var devices = DeviceFactory.GetEnabledDevices();
            if(devices.Count == 0)
            {
                throw new Exceptions.InvalidStateException("No enabled devices were found");
            }

            var retVal = new List<string>();
            devices.ForEach(device => retVal.Add(GetCapabilitiesString(device)));
            return retVal;
        }


    }
}
