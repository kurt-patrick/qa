using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    /// <summary>
    /// All of these locators are finding the same element however using different locator methods
    /// This has been done purely as an example of different ways of doing things
    /// https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html
    /// </summary>
    public class FindsByAndroidUiSelectorPage : PageBase
    {
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().className(\"android.widget.CheckBox\")")]
        private IWebElement _byClassName = null;

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"io.selendroid.testapp:id/input_adds_check_box\")")]
        private IWebElement _byResourceId = null;

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().text(\"I accept adds\")")]
        private IWebElement _byText = null;

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().textContains(\"accept adds\")")]
        private IWebElement _byTextContains = null;

        public FindsByAndroidUiSelectorPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return true;
        }

        public void AssertByClassName()
        {
            Assert.IsTrue(IsVisible(_byClassName), "Found element by className");
        }

        public void AssertByResourceId()
        {
            Assert.IsTrue(IsVisible(_byResourceId), "Found element by resourceId");
        }

        public void AssertByText()
        {
            Assert.IsTrue(IsVisible(_byText), "Found element by text");
        }

        public void AssertByTextContains()
        {
            Assert.IsTrue(IsVisible(_byTextContains), "Found element by textContains");
        }

    }
}
