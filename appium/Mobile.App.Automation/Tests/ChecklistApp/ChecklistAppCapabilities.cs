namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    public class ChecklistAppCapabilities : AppCapabilitiesBase
    {
        public const string MainActivity = ".MainActivity";

        public static ChecklistAppCapabilities Instance { get; } = new ChecklistAppCapabilities();

        public ChecklistAppCapabilities() : base("jakiganicsystems.simplestchecklist", MainActivity)
        {
        }

    }
}
