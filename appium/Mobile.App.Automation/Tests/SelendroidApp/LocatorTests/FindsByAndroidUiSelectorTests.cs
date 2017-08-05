using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    [TestFixtureSource("GalaxyS4")]
    class FindsByAndroidUiSelectorTests : TestBaseGeneric<HomeScreenPage>
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

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.HomeScreenActivity();
        }

    }
}
