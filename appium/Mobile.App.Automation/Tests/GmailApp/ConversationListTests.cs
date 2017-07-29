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

            var convos = listPage.WaitForRowCount(7).GetConversations();

            Assert.AreEqual(7, convos.Count);

            var index = ConversationListPage.IndexOf(convos, new List<string> { "Social", "25 New" });
            Assert.AreNotEqual(-1, index);

            convos[index].TapRow();

            System.Threading.Thread.Sleep(5000);
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
