using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    public static class ChecklistAppCapabilities
    {
        public const string AppPackage = "jakiganicsystems.simplestchecklist";
        public const string MainActivity = ".MainActivity";

        /// <summary>
        /// TODO: Load the list of drivers from the hdd
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCapabilities()
        {
            return new List<string>
            {
                new GalaxyS4Capabilities().GetCapabilitiesString(AppPackage, MainActivity)
            };
        }

    }
}
