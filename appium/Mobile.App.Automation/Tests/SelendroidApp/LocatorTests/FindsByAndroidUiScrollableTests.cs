using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    class FindsByAndroidUiScrollableTests : SelendroidAppTestBaseGeneric<FindsByAndroidUiSelectorPage>
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

    }
}
