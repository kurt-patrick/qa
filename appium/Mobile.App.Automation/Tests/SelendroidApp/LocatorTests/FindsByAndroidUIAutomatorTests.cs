using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.LocatorTests
{
    class FindsByAndroidUIAutomatorTests : SelendroidAppTestBaseGeneric<FindsByAndroidUIAutomatorPage>
    {
        public FindsByAndroidUIAutomatorTests(DesiredCapabilities caps) 
            : base(caps) 
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

    }
}
