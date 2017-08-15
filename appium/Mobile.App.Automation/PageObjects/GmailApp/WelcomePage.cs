using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class WelcomePage : PageObjects.PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "welcome_tour_got_it")]
        private IWebElement _gotIt = null;

        public WelcomePage(AppiumDriver<IWebElement> driver) : base(driver)
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
