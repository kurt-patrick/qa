using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

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

        public DialogPage(AppiumDriver<IWebElement> driver) : base(driver)
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
