using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.SelendroidApp
{
    public class GalaxyS4Capabilities : DriverCapabilitiesBase
    {
        public const string HomeScreenActivity = ".HomeScreenActivity";
        public const string TouchGesturesActivity = ".TouchGesturesActivity";

        public static GalaxyS4Capabilities Instance { get => new GalaxyS4Capabilities(); }

        private GalaxyS4Capabilities() : base("Android", "a710eaec", "io.selendroid.testapp")
        {
        }

        public static List<string> HomeScreenCapabilites()
        {
            return Instance.GetCapabilitiesString(HomeScreenActivity);
        }

    }
}
