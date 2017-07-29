using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.GmailApp
{
    public class GalaxyS4Capabilities : DriverCapabilitiesBase
    {
        private static GalaxyS4Capabilities Instance { get => new GalaxyS4Capabilities(); }

        private GalaxyS4Capabilities() : base("Android", "a710eaec", "com.google.android.gm")
        {
        }

        public static List<string> WelcomeScreenActivity()
        {
            return Instance.GetCapabilitiesString(".ConversationListActivityGmail");
        }

    }
}
