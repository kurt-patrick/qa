using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class DialogPage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "android:id/message")]
        private IWebElement _message = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "android:id/progress_percent")]
        private IWebElement _progressPercent = null;

        public DialogPage(TestCaseSettings settings) : base(settings)
        {
        }

        public override bool IsLoaded()
        {
            return WaitForVisible(_message);
        }

        public DialogPage AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "Dialog is not loaded");
            return this;
        }

        public DialogPage AssertDialogIsClosed()
        {
            Assert.IsTrue(TryWaitForStaleOrHidden(_message), "Dialog is closed");
            return this;
        }

        public string GetProgressPercent()
        {
            return GetText(_progressPercent);
        }

    }
}
