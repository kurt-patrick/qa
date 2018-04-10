using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    class MenuBarPage : PageBase
    {
        public MobileElementWrapper Add => new MobileElementWrapper(_driver, By.Id("menu_add"));
        public MobileElementWrapper Delete => new MobileElementWrapper(_driver, By.Id("menu_clear_completed"));

        public MenuBarPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(Add, Delete);
        }

    }
}
