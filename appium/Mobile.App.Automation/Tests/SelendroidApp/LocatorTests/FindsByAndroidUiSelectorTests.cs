using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    class FindsByAndroidUiSelectorTests : SelendroidAppTestBaseGeneric<HomeScreenPage>
    {
        RegisterUserPage _registerUserPage = null;
        public FindsByAndroidUiSelectorTests(DesiredCapabilities caps) 
            : base(caps) 
        {
            OnTestSetupEventHandler += OnTestSetup;
        }

        void OnTestSetup()
        {
            _pageObject.Registration.Click();

            _registerUserPage = Get<RegisterUserPage>();

            Assert.IsTrue(_registerUserPage.IsLoaded());
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void ScrollingTests()
        {
            var uiScrollablePage = _registerUserPage.SwitchPageObject<FindsByAndroidUiScrollablePage>();

            // scroll to the last element
            uiScrollablePage.AssertScrollToBottomThenTop();

        }

    }
}
