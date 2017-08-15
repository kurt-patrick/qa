using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class EmailAddressSelectionPage : PageObjects.PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "action_done")]
        private IWebElement _takeMeToGmail = null;

        public EmailAddressSelectionPage(AppiumDriver<IWebElement> driver) : base(driver)
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
