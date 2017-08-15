using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    class ChecklistPage : ListViewWrapper
    {
        public ChecklistPage(AppiumDriver<IWebElement> driver) : base(driver, "//android.widget.ListView")
        {
        }

    }
}
