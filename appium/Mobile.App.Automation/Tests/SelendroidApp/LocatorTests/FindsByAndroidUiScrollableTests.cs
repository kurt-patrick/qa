using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    class FindsByAndroidUiScrollableTests : SelendroidAppTestBaseGeneric<FindsByAndroidUiSelectorPage>
    {
        public FindsByAndroidUiScrollableTests(DesiredCapabilities caps) 
            : base(caps) 
        {
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void FindsByClassNameTest()
        {
            _pageObject.AssertByClassName();
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void FindsByResourceIDTest()
        {
            _pageObject.AssertByResourceId();
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void FindsByTextTest()
        {
            _pageObject.AssertByText();
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void FindsByTextContainsTest()
        {
            _pageObject.AssertByTextContains();
        }

    }
}
