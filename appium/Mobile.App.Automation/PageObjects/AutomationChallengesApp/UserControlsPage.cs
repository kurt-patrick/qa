using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class UserControlsPage : PageBase
    {
        public MobileElementWrapper CheckBox => new MobileElementWrapper(_driver, By.Id("checkBox2"));
        public MobileElementWrapper RadioOne => new MobileElementWrapper(_driver, By.Id("radioOne"));
        public MobileElementWrapper RadioTwo => new MobileElementWrapper(_driver, By.Id("radioTwo"));
        public MobileElementWrapper RadioThree => new MobileElementWrapper(_driver, By.Id("radioThree"));
        public MobileElementWrapper ToggleButton => new MobileElementWrapper(_driver, By.Id("toggleButton2"));
        public MobileElementWrapper TxtCheck => new MobileElementWrapper(_driver, By.Id("txtCheck"));
        public MobileElementWrapper TxtRadioGroup => new MobileElementWrapper(_driver, By.Id("txtRadioGroup"));
        public MobileElementWrapper TxtSpinner => new MobileElementWrapper(_driver, By.Id("txtSpinner"));
        public MobileElementWrapper TxtSwitch => new MobileElementWrapper(_driver, By.Id("txtSwitch"));
        public MobileElementWrapper TxtToggle => new MobileElementWrapper(_driver, By.Id("txtToggle"));
        public MobileElementWrapper TxtValidate => new MobileElementWrapper(_driver, By.Id("txtValidate"));
        public MobileElementWrapper Spinner => new MobileElementWrapper(_driver, By.Id("spinner"));
        public MobileElementWrapper Switch => new MobileElementWrapper(_driver, By.Id("switch2"));
        public MobileElementWrapper ValidateButton => new MobileElementWrapper(_driver, By.Id("btnToast"));

        public UserControlsPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(CheckBox, Switch, ToggleButton);
        }

    }
}
