using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.GmailApp;
using System.Linq;

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
            var listView = NavigateToConversationList();

            var convos = listView.AssertIsLoaded().WaitForRowCount(7).GetRows();

            Assert.AreEqual(7, convos.Count);

            // Find a row with specific text and tap on it to load into detail
            /*
            var index = ConversationListPage.IndexOf(convos, new List<string> { "Social", "25 New" });
            Assert.AreNotEqual(-1, index);
            convos[index].TapRow();
            */

            Assert.AreEqual(7, convos.Count(row => row.Contains("e")));
            Assert.AreEqual(1, convos.Count(row => row.Contains("Invoice")));
            Assert.AreEqual(0, convos.Count(row => row.Contains("fail")));


        }

        private ConversationListPage NavigateToConversationList()
        {
            return
                _pageObject
                    .ClickGotIt()
                    .SwitchPageObject<EmailAddressSelectionPage>()
                    .ClickTakeMeToGmail()
                    .SwitchPageObject<ConversationListPage>();
        }

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.WelcomeScreenCapabilities();
        }

    }
}
