using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    /// <summary>
    /// All of these locators are finding the same element however using different locator methods
    /// This has been done purely as an example of different ways of doing things
    /// https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html
    /// </summary>
    public class FindsByAndroidUiSelectorPage : PageBase
    {
        MobileElementWrapper ByClassName => new MobileElementWrapper(_driver, MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.widget.CheckBox\")"));
        MobileElementWrapper ByResourceId => new MobileElementWrapper(_driver, MobileBy.AndroidUIAutomator("new UiSelector().resourceId(\"io.selendroid.testapp:id/input_adds_check_box\")"));
        MobileElementWrapper ByText => new MobileElementWrapper(_driver, MobileBy.AndroidUIAutomator("new UiSelector().text(\"I accept adds\")"));
        MobileElementWrapper ByTextContains => new MobileElementWrapper(_driver, MobileBy.AndroidUIAutomator("new UiSelector().textContains(\"accept adds\")"));

        public FindsByAndroidUiSelectorPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return true;
        }

        public void AssertByClassName()
        {
            Assert.IsTrue(IsDisplayed(ByClassName), "Found element by className");
        }

        public void AssertByResourceId()
        {
            Assert.IsTrue(IsDisplayed(ByResourceId), "Found element by resourceId");
        }

        public void AssertByText()
        {
            Assert.IsTrue(IsDisplayed(ByText), "Found element by text");
        }

        public void AssertByTextContains()
        {
            Assert.IsTrue(IsDisplayed(ByTextContains), "Found element by textContains");
        }

    }
}
