using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    /// <summary>
    /// All of these locators are finding the same element however using a different locator method
    /// This has been done purely as an example of different ways of doing things
    /// </summary>
    public class FindsByAndroidUIAutomatorPage : PageBase
    {
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/input_adds_check_box")]
        private IWebElement _byID = null;

        [FindsByAndroidUIAutomator(ClassName = "android.widget.CheckBox")]
        private IWebElement _byClassName = null;

        [FindsByAndroidUIAutomator(XPath = "//android.widget.CheckBox[@text='I accept adds']")]
        private IWebElement _byXPath = null;

        [FindsByAndroidUIAutomator(XPath = "//*[@text='I accept adds']")]
        private IWebElement _byXPathGeneric = null;

        public FindsByAndroidUIAutomatorPage(TestCaseSettings settings) : base(settings)
        {
        }

        public override bool IsLoaded()
        {
            return true;
        }

        public void AssertByID()
        {
            Assert.IsTrue(IsVisible(_byID), "Found element by ID");
        }

        public void AssertByClassName()
        {
            Assert.IsTrue(IsVisible(_byClassName), "Found element by ClassName");
        }

        public void AssertByXPath()
        {
            Assert.IsTrue(IsVisible(_byXPath), "Found element by XPath");
        }

        public void AssertByXPathGeneric()
        {
            Assert.IsTrue(IsVisible(_byXPathGeneric), "Found element by XPath Generic");
        }

    }
}
