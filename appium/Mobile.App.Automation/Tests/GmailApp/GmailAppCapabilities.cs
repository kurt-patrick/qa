namespace KPE.Mobile.App.Automation.Tests.GmailApp
{
    public class GmailAppCapabilities : AppCapabilitiesBase
    {
        public const string ConversationListActivityGmail = ".ConversationListActivityGmail";

        public GmailAppCapabilities() : base("com.google.android.gm", ConversationListActivityGmail)
        {
        }

        public static GmailAppCapabilities Instance { get; } = new GmailAppCapabilities();

    }
}
