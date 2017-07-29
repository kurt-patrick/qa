using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    class ConversationListPage : PageObjects.PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "conversation_list_view")]
        private IWebElement _listview = null;

        public ConversationListPage(TestCaseSettings settings) : base(settings)
        {
        }

        public override bool IsLoaded()
        {
            return IsVisible(_listview);
        }

        public ConversationListPage AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded());
            return this;
        }

        /// <summary>
        /// On first load of the activity the count will be (x) after a few moments the page can refresh and have (y); or
        /// User may delete a row etc ...
        /// </summary>
        /// <param name="count"></param>
        public ConversationListPage WaitForRowCount(int count)
        {
            var wait = new WebDriverWait(_driver, Common.Constants.DefaultTimeOutTimeSpan);
            wait.Until((IWebElement) => _listview.FindElements(MobileBy.XPath("//android.widget.ListView/*")).Count == count);
            return this;
        }

        public List<ConversationListRowPage> GetConversations()
        {
            var retVal = new List<ConversationListRowPage>();

            // Get all the child rows of the listview
            var children = _listview.FindElements(MobileBy.XPath("//android.widget.ListView/*"));
            for(int i=0; i<children.Count; i++)
            {
                retVal.Add(new ConversationListRowPage(_testCaseSettings, children[i], $"//android.widget.ListView/*[{i+1}]"));
            }
            return retVal;
        }

        public static int IndexOf(List<ConversationListRowPage> rows, List<string> hasText)
        {
            return rows.FindIndex(row => row.HasText(hasText));
        }

    }
}
