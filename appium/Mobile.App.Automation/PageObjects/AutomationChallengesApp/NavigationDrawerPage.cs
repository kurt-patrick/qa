using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using KPE.Mobile.App.Automation.Exceptions;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class NavigationDrawerPage : PageBase
    {
        By _drawerPanel = By.Id("nav_view");
        By _openNavButton = By.ClassName("android.widget.ImageButton");

        public NavigationDrawerPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public NavigationDrawerPage OpenDrawer()
        {
            bool isOpen = TryClickAndValidate(_openNavButton, () => IsOpen(), 5);
            if(!isOpen)
            {
                throw new InvalidStateException("Failed to open the drawer");
            }
            return this;
        }

        public bool IsOpen()
        {
            return IsVisible(_drawerPanel);
        }

        public bool IsClosed()
        {
            return IsVisible(_openNavButton);
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

        void ClickChallenge(string text)
        {
            string selector = Helpers.UiSelectorHelper.Text(text);
            Click(MobileBy.AndroidUIAutomator(selector));
        }

    }
}
