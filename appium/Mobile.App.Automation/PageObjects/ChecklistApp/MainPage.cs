using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    class MainPage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "main_listview")]
        private IWebElement _listView = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "main_menu_bar")]
        private IWebElement _menuBar = null;

        public ChecklistPage Checklist { get; private set; }
        public MenuBarPage MenuBar { get; private set; }

        public MainPage(TestCaseSettings settings) : base(settings)
        {
            Checklist = new ChecklistPage(settings);
            MenuBar = new MenuBarPage(settings);
        }

        public override bool IsLoaded()
        {
            return IsVisible(_listView, _menuBar);
        }
    }
}
