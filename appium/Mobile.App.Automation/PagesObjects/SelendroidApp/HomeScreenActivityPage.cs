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

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class HomeScreenActivityPage : PageBase
    {
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/my_text_field")]
        private IWebElement _myTextField = null;

        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/input_adds_check_box")]
        private IWebElement _acceptsAdds = null;

        [FindsByAndroidUIAutomator(ClassName = "android.widget.CheckBox")]
        private IWebElement _acceptsAddsByClass = null;

        [FindsByAndroidUIAutomator(XPath = "//android.widget.CheckBox[@text='I accept adds']")]
        private IWebElement _acceptsAddsByXPath = null;

        [FindsByAndroidUIAutomator(XPath = "//*[@text='I accept adds']")]
        private IWebElement _acceptsAddsByXPath02 = null;

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"io.selendroid.testapp:id/input_adds_check_box\")", Priority = 1)]
        [FindsByAndroidUIAutomator(ID = "input_adds_check_box", Priority = 2)]
        private IWebElement _acceptsAddsByAndroidUIAutomator = null;

        public override bool IsLoaded()
        {
            return IsVisible(_myTextField, _acceptsAdds, _acceptsAddsByClass, _acceptsAddsByXPath, _acceptsAddsByXPath02, _acceptsAddsByAndroidUIAutomator);
        }

        public void AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "HomeScreenActivityPage is not loaded");
        }

        public HomeScreenActivityPage(TestCaseSettings settings) : base(settings)
        {
            this.InitElementsWithRetrying(this);
        }

        public void SetMyTextField(string text)
        {
            SendKeys(_myTextField, text);
        }

        public string GetMyTextField()
        {
            return GetText(_myTextField);
        }

    }
}
