using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class ConversationListPage : ListViewWrapper
    {
        public ConversationListPage(AppiumDriver<IWebElement> driver) : base(driver, "//android.widget.ListView")
        {
        }

    }
}
