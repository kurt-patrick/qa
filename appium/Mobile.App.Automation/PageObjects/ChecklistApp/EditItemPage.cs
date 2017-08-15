﻿using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    class EditItemPage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "edit_edittext")]
        private IWebElement _txtEdit = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "edit_cancel")]
        private IWebElement _cancel = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "edit_add_back")]
        private IWebElement _add = null;

        public EditItemPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsVisible(_txtEdit, _cancel, _add);
        }

        public EditItemPage AssertLoaded()
        {
            Assert.IsTrue(IsLoaded());
            return this;
        }

        public EditItemPage AssertText(string expected)
        {
            Assert.AreEqual(expected, GetText(_txtEdit));
            return this;
        }

        public EditItemPage EnterText(string text)
        {
            SendKeys(_txtEdit, text);
            return this;
        }

        public void ClickAddDone()
        {
            Click(_add);
        }

        public void ClickCancel()
        {
            Click(_cancel);
        }

    }
}
