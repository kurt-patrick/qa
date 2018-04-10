using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    internal class MainPage : PageBase
    {
        MobileElementWrapper ListView => new MobileElementWrapper(_driver, By.Id("main_listview"));
        MobileElementWrapper MainMenuBar => new MobileElementWrapper(_driver, By.Id("main_menu_bar"));

        public ListViewWrapper Checklist => new ListViewWrapper(_driver);
        public MenuBarPage MenuBar => new MenuBarPage(_driver);

        public MainPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(ListView, MainMenuBar);
        }
    }
}
