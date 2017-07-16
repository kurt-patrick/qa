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

        public override bool IsLoaded()
        {
            return IsVisible(_myTextField, _acceptsAdds);
        }

        public HomeScreenActivityPage(TestCaseSettings settings) : base(settings)
        {
            this.InitElements(this);
        }

        public void AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "HomeScreenActivityPage is not loaded");
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

    }
}
