using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.PageObjects;
using KPE.Mobile.App.Automation.Helpers;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    /// <summary>
    /// All of these locators are finding the same element however using different locator methods
    /// This has been done purely as an example of different ways of doing things
    /// https://developer.android.com/reference/android/support/test/uiautomator/UiScrollable.html
    /// </summary>
    public class FindsByAndroidUiScrollablePage : PageBase
    {
        /// <summary>
        /// First input box at the top
        /// </summary>
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "inputUsername")]
        private IWebElement _usernameEle = null;

        public FindsByAndroidUiScrollablePage(TestCaseSettings settings) : base(settings)
        {
        }

        public void AssertScrollToBottomThenTop()
        {
            // Scroll to the bottom of the element
            string uiSelectorScrollView = UiSelectorHelper.ClassName("android.widget.ScrollView");
            string uiSelectorElement = UiSelectorHelper.ResourceId("io.selendroid.testapp:id/btnRegisterUser");
            string selector = string.Format("new UiScrollable({0}).scrollIntoView({1})", uiSelectorScrollView, uiSelectorElement);

            Assert.IsTrue(IsVisible(MobileBy.AndroidUIAutomator(selector)), "Failed to find the register user button");

            // Scroll to the top element
            uiSelectorElement = UiSelectorHelper.ResourceId("io.selendroid.testapp:id/inputUsername");
            selector = string.Format("new UiScrollable({0}).scrollIntoView({1})", uiSelectorScrollView, uiSelectorElement);

            Assert.IsTrue(IsVisible(MobileBy.AndroidUIAutomator(selector)), "Failed to find the username input");

        }

        public override bool IsLoaded()
        {
            return IsVisible(_usernameEle);
        }


    }
}
