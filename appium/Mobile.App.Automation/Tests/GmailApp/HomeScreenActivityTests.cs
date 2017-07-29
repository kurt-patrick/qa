using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.GmailApp;

namespace KPE.Mobile.App.Automation.Tests.GmailApp
{
    [TestFixtureSource("GalaxyS4")]
    class ConversationListTests : TestBaseGeneric<WelcomePage>
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ConversationListTests(string capabilities) 
            : base(capabilities) 
        {
        }

        [Test]
        public void GmailListviewTests()
        {
            var listPage = NavigateToConversationList();

        }

        private ConversationListPage NavigateToConversationList()
        {
            return
                _pageObject
                    .ClickGotIt()
                    .SwitchPageObject<EmailAddressSelectionPage>()
                    .ClickTakeMeToGmail()
                    .SwitchPageObject<ConversationListPage>()
                    .AssertIsLoaded();
        }

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.WelcomeScreenActivity();
        }

    }
}
