using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.GmailApp
{
    public static class GmailAppCapabilities
    {
        public const string AppPackage = "com.google.android.gm";
        public const string ConversationListActivityGmail = ".ConversationListActivityGmail";

        /// <summary>
        /// TODO: Load the list of drivers from the hdd
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCapabilities()
        {
            return new List<string>
            {
                new GalaxyS4Capabilities().GetCapabilitiesString(AppPackage, ConversationListActivityGmail)
            };
        }

    }
}
