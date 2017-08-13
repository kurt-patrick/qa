using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.SelendroidApp
{
    public static class SelendroidAppCapabilities
    {
        public const string AppPackage = "io.selendroid.testapp";
        public const string HomeScreenActivity = ".HomeScreenActivity";
        public const string TouchGesturesActivity = ".TouchGesturesActivity";

        /// <summary>
        /// TODO: Load the list of drivers from the hdd
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCapabilities()
        {
            return new List<string>
            {
                new GalaxyS4Capabilities().GetCapabilitiesString(AppPackage, HomeScreenActivity)
            };
        }

    }
}
