using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    public class GalaxyS4Capabilities : DriverCapabilitiesBase
    {
        public const string MainActivity = ".MainActivity";

        private static GalaxyS4Capabilities Instance { get => new GalaxyS4Capabilities(); }

        private GalaxyS4Capabilities() : base("Android", "a710eaec", "jakiganicsystems.simplestchecklist")
        {
        }

        public static List<string> MainActivityCapabilites()
        {
            return Instance.GetCapabilitiesString(MainActivity);
        }

    }
}
