using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Tests.SelendroidApp
{
    public static class DriverCapabilities
    {
        public const string HomeScreenActivityOnGalaxyS4 = "HomeScreenActivityOnGalaxyS4Capabilities";

        public static List<string> HomeScreenActivityOnGalaxyS4Capabilities()
        {
            return new List<string>()
            {
                "device=Android,deviceName=a710eaec,appPackage=io.selendroid.testapp,appActivity=.HomeScreenActivity"
            };
        }


    }
}
