using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium;

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

        public MenuBarPage(TestCaseSettings settings) : base(settings)
        {
        }

        public override bool IsLoaded()
        {
            return IsVisible(_delete, _add);
        }

        public void ClickAdd()
        {
            Click(_add);
        }

        public void ClickDelete()
        {
            Click(_delete);
        }

    }
}
