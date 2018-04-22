using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class RegisterUserPage : PageBase
    {
        public MobileElementWrapper Username => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/inputUsername"));
        public MobileElementWrapper Email => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/inputEmail"));
        public MobileElementWrapper Password => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/inputPassword"));
        public MobileElementWrapper Name => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/inputName"));

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/input_preferedProgrammingLanguage")]
        private IWebElement _progLangEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/input_adds")]
        private IWebElement _acceptAddsEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/btnRegisterUser")]
        private IWebElement _registerUserEle = null;

        public string ProgrammingLanguage
        {
            get => GetText(_progLangEle);
            set => new DropDownHelper(_driver).SelectByText(_progLangEle, value);
        }

        public bool AcceptAdds
        {
            get => IsChecked(_acceptAddsEle);
            set => ToggleCheckBox(_acceptAddsEle, value);
        }

        public RegisterUserPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public RegisterUserPage ClickRegisterUser()
        {
            Click(_registerUserEle);
            return this;
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(Username, Email, Password);
        }

        public RegisterUserPage AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "Register user page is not loaded");
            return this;
        }

    }
}
