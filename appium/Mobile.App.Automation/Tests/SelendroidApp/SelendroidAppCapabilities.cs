namespace KPE.Mobile.App.Automation.Tests.SelendroidApp
{
    public class SelendroidAppCapabilities : AppCapabilitiesBase
    {
        public const string HomeScreenActivity = ".HomeScreenActivity";
        public const string TouchGesturesActivity = ".TouchGesturesActivity";

        public static SelendroidAppCapabilities Instance { get; } = new SelendroidAppCapabilities();

        public SelendroidAppCapabilities() : base("io.selendroid.testapp", HomeScreenActivity)
        {
        }

    }
}
