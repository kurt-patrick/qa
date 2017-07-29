using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class ConversationListPage : PageObjects.PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "conversation_list_view")]
        private IWebElement _listview = null;

        public ConversationListPage(TestCaseSettings settings) : base(settings)
        {
        }

        public override bool IsLoaded()
        {
            return IsVisible(_listview);
        }

        public ConversationListPage AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded());
            return this;
        }

    }
}
