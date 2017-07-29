using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class EmailAddressSelectionPage : PageObjects.PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "action_done")]
        private IWebElement _takeMeToGmail = null;

        public EmailAddressSelectionPage(TestCaseSettings settings) : base(settings)
        {
        }

        public EmailAddressSelectionPage ClickTakeMeToGmail()
        {
            Click(_takeMeToGmail);
            return this;
        }

        public override bool IsLoaded()
        {
            return IsVisible(_takeMeToGmail);
        }
    }
}
