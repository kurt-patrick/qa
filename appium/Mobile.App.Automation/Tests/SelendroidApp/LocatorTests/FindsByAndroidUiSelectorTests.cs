using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    class FindsByAndroidUiSelectorTests : SelendroidAppTestBaseGeneric<HomeScreenPage>
    {
        RegisterUserPage _registerUserPage = null;
        public FindsByAndroidUiSelectorTests(string capabilities) 
            : base(capabilities) 
        {
        }

        public override void TestSetup()
        {
            _registerUserPage =
                _pageObject
                    .ClickUserRegistration()
                    .SwitchPageObject<RegisterUserPage>();
            _registerUserPage.AssertIsLoaded();
        }

        [Test]
        public void ScrollingTests()
        {
            var uiScrollablePage = _registerUserPage.SwitchPageObject<FindsByAndroidUiScrollablePage>();

            // scroll to the last element
            uiScrollablePage.AssertScrollToBottomThenTop();

        }

    }
}
