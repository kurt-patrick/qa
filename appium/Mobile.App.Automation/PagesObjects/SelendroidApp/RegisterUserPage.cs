using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class RegisterUserPage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/inputUsername")]
        private IWebElement _usernameEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/inputEmail")]
        private IWebElement _emailEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/inputPassword")]
        private IWebElement _passwordEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/inputName")]
        private IWebElement _nameEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/input_preferedProgrammingLanguage")]
        private IWebElement _progLangEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/input_adds")]
        private IWebElement _acceptAddsEle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/btnRegisterUser")]
        private IWebElement _registerUserEle = null;

        public string Username
        {
            get => GetText(_usernameEle);
            set => SendKeys(_usernameEle, value);
        }

        public string Email
        {
            get => GetText(_emailEle);
            set => SendKeys(_emailEle, value);
        }

        public string Password
        {
            get => GetText(_passwordEle);
            set => SendKeys(_passwordEle, value);
        }

        public string Name
        {
            get => GetText(_nameEle);
            set => SendKeys(_nameEle, value);
        }

        public string ProgrammingLanguage
        {
            get => GetText(_progLangEle);
            set => SendKeys(_progLangEle, value);
        }

        public bool AcceptAdds
        {
            get => IsChecked(_acceptAddsEle);
            set => ToggleCheckBox(_acceptAddsEle, value);
        }

        public RegisterUserPage(TestCaseSettings settings) : base(settings)
        {
        }

        public RegisterUserPage ClickRegisterUser()
        {
            Click(_registerUserEle);
            return this;
        }

        public override bool IsLoaded()
        {
            return IsVisible(_acceptAddsEle, _emailEle, _nameEle, _passwordEle, _progLangEle, _registerUserEle, _usernameEle);
        }

        public RegisterUserPage AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "Register user page is not loaded");
            return this;
        }

    }
}
