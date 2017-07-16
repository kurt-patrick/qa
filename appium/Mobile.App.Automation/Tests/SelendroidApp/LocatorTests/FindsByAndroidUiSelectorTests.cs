using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    [TestFixtureSource(DriverCapabilities.HomeScreenActivityOnGalaxyS4)]
    class FindsByAndroidUiSelectorTests : TestBaseGeneric<FindsByAndroidUiSelectorPage>
    {
        public FindsByAndroidUiSelectorTests(string capabilities) 
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

        public static List<string> HomeScreenActivityOnGalaxyS4Capabilities()
        {
            return DriverCapabilities.HomeScreenActivityOnGalaxyS4Capabilities();
        }

    }
}
