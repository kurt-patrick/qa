using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class HomeScreenPage : PageBase
    {
        public MobileElementWrapper CheckBox => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/input_adds_check_box"));
        public MobileElementWrapper ProgressButton => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/waitingButtonTest"));
        public MobileElementWrapper Registration => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/startUserRegistration"));
        public MobileElementWrapper TextField => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/my_text_field"));
        public MobileElementWrapper TouchActions => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/touchTest"));

        public HomeScreenPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(Registration, ProgressButton, CheckBox, TextField, TouchActions);
        }
    }
}
