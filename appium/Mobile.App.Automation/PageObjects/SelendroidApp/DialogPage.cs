using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class DialogPage : PageBase
    {
        readonly By _messageLocator = By.Id("android:id/message");

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "android:id/progress_percent")]
        private IWebElement _progressPercent = null;

        public DialogPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsVisible(_messageLocator);
        }

        public DialogPage AssertDialogIsClosed()
        {
            Assert.IsTrue(IsNotVisible(_messageLocator), "Dialog is closed");
            return this;
        }

        public string GetProgressPercent()
        {
            return GetText(_progressPercent);
        }

    }
}
