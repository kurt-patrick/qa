using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class DialogPage : PageBase
    {
        public MobileElementWrapper Message => new MobileElementWrapper(_driver, By.Id("message"));
        public MobileElementWrapper ProgressBar => new MobileElementWrapper(_driver, By.Id("progress_percent"));

        public DialogPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(Message);
        }

    }
}
