using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    [TestFixtureSource("GalaxyS4")]
    class FindsByAndroidUIAutomatorTests : TestBaseGeneric<FindsByAndroidUIAutomatorPage>
    {
        public FindsByAndroidUIAutomatorTests(string capabilities) 
            : base(capabilities) 
        {
        }

        [Test]
        public void FindsByClassNameTest()
        {
            _pageObject.AssertByClassName();
        }

        [Test]
        public void FindsByIDTest()
        {
            _pageObject.AssertByID();
        }

        [Test]
        public void FindsByXPathTest()
        {
            _pageObject.AssertByXPath();
        }

        [Test]
        public void FindsByXPathGenericTest()
        {
            _pageObject.AssertByXPathGeneric();
        }

        [Test]
        public void FindsBySecondPriorityTest()
        {
            _pageObject.AssertSecondPriorityElementTest();
        }

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.HomeScreenActivity();
        }

    }
}
