using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    class MenuBarPage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "menu_clear_completed")]
        private IWebElement _delete = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "menu_add")]
        private IWebElement _add = null;

        public MenuBarPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsVisible(_delete, _add);
        }

        public EditItemPage ClickAdd()
        {
            Click(_add);
            return new EditItemPage(_driver);
        }

        public void ClickDelete()
        {
            Click(_delete);
        }

    }
}
