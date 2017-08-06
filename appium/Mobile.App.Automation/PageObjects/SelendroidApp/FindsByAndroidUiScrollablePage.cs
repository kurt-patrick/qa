using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Support.PageObjects;
using KPE.Mobile.App.Automation.Helpers;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

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
            string btnRegisterUser = UiSelectorHelper.ResourceId("io.selendroid.testapp:id/btnRegisterUser");
            string txtUsername = UiSelectorHelper.ResourceId("io.selendroid.testapp:id/inputUsername");

            // Scroll down to the bottom element
            string selector = new UiScrollableHelper().ScrollIntoView(btnRegisterUser);
            Assert.IsTrue(IsVisible(MobileBy.AndroidUIAutomator(selector)), "Failed to find the register user button");

            // Scroll up to the top element
            selector = new UiScrollableHelper().ScrollIntoView(txtUsername);
            Assert.IsTrue(IsVisible(MobileBy.AndroidUIAutomator(selector)), "Failed to find the username input");

            // Scroll down and click the bottom element
            selector = new UiScrollableHelper().ScrollIntoView(btnRegisterUser);
            Click(MobileBy.AndroidUIAutomator(selector));

        }

        public override bool IsLoaded()
        {
            return IsVisible(_usernameEle);
        }


    }
}
