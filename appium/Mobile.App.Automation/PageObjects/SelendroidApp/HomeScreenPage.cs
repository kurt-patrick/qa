using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class HomeScreenPage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/startUserRegistration")]
        private IWebElement _btnUserRegistration = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/waitingButtonTest")]
        private IWebElement _btnShowProgressBar = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/input_adds_check_box")]
        private IWebElement _checkBox = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/my_text_field")]
        private IWebElement _myTextField = null;

        public override bool IsLoaded()
        {
            return IsVisible(_btnUserRegistration, _btnShowProgressBar, _checkBox, _myTextField);
        }

        public HomeScreenPage(TestCaseSettings settings) : base(settings)
        {
        }

        public void AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "HomeScreenActivityPage is not loaded");
        }

        public void AssertCheckBoxState(bool expected)
        {
            Assert.AreEqual(expected, IsChecked(_checkBox));
        }

        public void AssertCheckBoxText(string expected)
        {
            QA.ObjectQA.ThrowIfNull(expected);
            Assert.AreEqual(expected, GetText(_checkBox));
        }

        public void SetMyTextField(string text)
        {
            SendKeys(_myTextField, text);
        }

        public string GetMyTextField()
        {
            return GetText(_myTextField);
        }

        public void AssertTextField(string expected)
        {
            Assert.AreEqual(expected, GetMyTextField());
        }

        public bool ToggleCheckBox(bool check)
        {
            return ToggleCheckBox(_checkBox, check);
        }

        public HomeScreenPage ClickShowProgressBar()
        {
            Click(_btnShowProgressBar);
            return this;
        }

        public HomeScreenPage ClickUserRegistration()
        {
            Click(_btnUserRegistration);
            return this;
        }

    }
}
