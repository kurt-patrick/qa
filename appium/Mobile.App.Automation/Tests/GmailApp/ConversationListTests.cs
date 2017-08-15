using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.GmailApp;
using System.Linq;
using KPE.Mobile.App.Automation.Configuration;

namespace KPE.Mobile.App.Automation.Tests.GmailApp
{
    [TestFixtureSource(TestFixtureSourceName)]
    class ConversationListTests : TestBaseGeneric<WelcomePage>
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ConversationListTests(DriverCapabilities caps) 
            : base(caps) 
        {
        }

        [Test]
        [Ignore("just cos")]
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

        public static List<DriverCapabilities> CapabilitiesList()
        {
            return DriverCapabilities.GmailApp();
        }

    }
}
