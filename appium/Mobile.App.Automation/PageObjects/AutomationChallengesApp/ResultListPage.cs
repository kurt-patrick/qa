using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class ResultListPage : ListViewWrapper
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtActual")]
        private IWebElement _txtActual = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtExpected")]
        private IWebElement _txtExpected = null;

        public string Actual => _txtActual.Text.Trim();

        public ResultListPage(AppiumDriver<IWebElement> driver) : base(driver, "//android.support.v7.widget.RecyclerView")
        {
        }

        public string[] GetExpected()
        {
            var array = _txtExpected.Text.Trim().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if(array.Length != 4)
            {
                throw new Exceptions.InvalidStateException("The expected value should contain 4 values");
            }
            return array;
        }

        public override bool IsLoaded()
        {
            return IsVisible(_txtActual, _txtExpected);
        }
    }
}
