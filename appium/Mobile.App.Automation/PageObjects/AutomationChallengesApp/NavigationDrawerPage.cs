using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class NavigationDrawerPage : PageBase
    {
        public MobileElementWrapper DrawerPanel => new MobileElementWrapper(_driver, By.Id("nav_view"));
        public MobileElementWrapper OpenNavigationButton => new MobileElementWrapper(_driver, By.ClassName("android.widget.ImageButton"));

        public NavigationDrawerPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public NavigationDrawerPage OpenDrawer()
        {
            OpenNavigationButton.Click();
            DrawerPanel.Displayed();
            return this;
        }

        public void PinChallenge()
        {
            ClickChallenge("Pin code");
        }

        public void ListChallenge()
        {
            ClickChallenge("Results list");
        }

        public void SwipeChallenge()
        {
            ClickChallenge("Swipe to delete");
        }

        public void AlertChallenge()
        {
            ClickChallenge("Alert dialog");
        }

        public void ControlsChallenge()
        {
            ClickChallenge("Controls");
        }

        void ClickChallenge(string text)
        {
            string selector = Helpers.UiSelectorHelper.Text(text);
            Click(MobileBy.AndroidUIAutomator(selector));
        }

    }
}
