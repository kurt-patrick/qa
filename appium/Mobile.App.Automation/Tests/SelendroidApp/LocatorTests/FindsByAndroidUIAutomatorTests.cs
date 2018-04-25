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
        [Ignore(NUnit_Category)]
        public void FindsByClassNameTest()
        {
            Assert.IsTrue(_pageObject.ByClassName.Displayed(), "Found element by ClassName");
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void FindsByIDTest()
        {
            Assert.IsTrue(_pageObject.ByID.Displayed(), "Found element by ID");
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void FindsByXPathTest()
        {
            Assert.IsTrue(_pageObject.ByXPath.Displayed(), "Found element by XPath");
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void FindsByXPathGenericTest()
        {
            Assert.IsTrue(_pageObject.ByXPathGeneric.Displayed(), "Found element by XPath Generic");
        }

    }
}
