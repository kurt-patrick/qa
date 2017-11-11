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
        By _openNavButton = By.TagName("android.widget.ImageButton");

        public NavigationDrawerPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public NavigationDrawerPage OpenDrawer()
        {
            if(!IsOpen())
            {
                var element = FindVisibleElement(_openNavButton);
                bool isOpen = TryClickAndValidate(element, () => IsOpen(), 5);
                if(!isOpen)
                {
                    throw new InvalidStateException("Failed to open the drawer");
                }
            }
            return this;
        }

        public bool IsOpen()
        {
            return _driver.FindElements(_drawerPanel).Any(ele => ele.Displayed);
        }


        public void PinCode()
        {
            throw new NotImplementedException();
        }

        public void ResultsList()
        {
            throw new NotImplementedException();
        }

        public void SwipeToDelete()
        {
            throw new NotImplementedException();
        }

    }
}
