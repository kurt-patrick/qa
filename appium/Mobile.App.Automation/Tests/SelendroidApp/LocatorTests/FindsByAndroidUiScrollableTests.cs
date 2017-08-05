using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    [TestFixtureSource("GalaxyS4")]
    class FindsByAndroidUiScrollableTests : TestBaseGeneric<FindsByAndroidUiSelectorPage>
    {
        public FindsByAndroidUiScrollableTests(string capabilities) 
            : base(capabilities) 
        {
        }

        [Test]
        public void FindsByClassNameTest()
        {
            _pageObject.AssertByClassName();
        }

        [Test]
        public void FindsByResourceIDTest()
        {
            _pageObject.AssertByResourceId();
        }

        [Test]
        public void FindsByTextTest()
        {
            _pageObject.AssertByText();
        }

        [Test]
        public void FindsByTextContainsTest()
        {
            _pageObject.AssertByTextContains();
        }

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.HomeScreenActivity();
        }

    }
}
