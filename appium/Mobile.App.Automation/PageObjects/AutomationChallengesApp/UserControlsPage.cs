using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class UserControlsPage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "toggleButton2")]
        private IWebElement _toggleButton = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtToggle")]
        private IWebElement _txtToggle = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "checkBox2")]
        private IWebElement _checkBox = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtCheck")]
        private IWebElement _txtCheck = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "switch2")]
        private IWebElement _switch = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtSwitch")]
        private IWebElement _txtSwitch = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "spinner")]
        private IWebElement _spinner = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtSpinner")]
        private IWebElement _txtSpinner = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtRadioGroup")]
        private IWebElement _txtRadioGroup = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "radioOne")]
        private IWebElement _radioOne = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "radioTwo")]
        private IWebElement _radioTwo = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "radioThree")]
        private IWebElement _radioThree = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "btnToast")]
        private IWebElement _btnToast = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtValidate")]
        private IWebElement _txtValidate = null;

        public string ToggleText => _txtToggle.Text.Trim();
        public string CheckBoxText => _txtCheck.Text.Trim();
        public string SwitchText => _txtSwitch.Text.Trim();
        public string SpinnerText => _txtSpinner.Text.Trim();
        public string RadioGroupText => _txtRadioGroup.Text.Trim();
        public string ValidateText => _txtValidate.Text.Trim();

        public UserControlsPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public UserControlsPage ToggleToggleButton(bool on)
        {
            if(_toggleButton.Selected != on)
            {
                _toggleButton.Click();
            }
            return this;
        }

        public UserControlsPage ToggleCheckBox(bool on)
        {
            if (_checkBox.Selected != on)
            {
                _checkBox.Click();
            }
            return this;
        }

        public override bool IsLoaded()
        {
            return _checkBox.Displayed && _switch.Displayed && _toggleButton.Displayed;
        }

        public UserControlsPage ClickValidate()
        {
            _btnToast.Click();
            return this;
        }


    }
}
