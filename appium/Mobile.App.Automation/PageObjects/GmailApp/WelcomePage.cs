using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class WelcomePage : PageObjects.PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "welcome_tour_got_it")]
        private IWebElement _gotIt = null;

        public WelcomePage(TestCaseSettings settings) : base(settings)
        {
        }

        public WelcomePage ClickGotIt()
        {
            Click(_gotIt);
            return this;
        }

        public override bool IsLoaded()
        {
            return IsVisible(_gotIt);
        }
    }
}
