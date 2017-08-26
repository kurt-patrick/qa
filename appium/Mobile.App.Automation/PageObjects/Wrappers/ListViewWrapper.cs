using KPE.Mobile.App.Automation.QA;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.PageObjects.Wrappers
{
    public class ListViewWrapper : PageBase
    {
        private const string DefaultAndroidLocator = "//android.widget.ListView";

        private string _listViewXPath = null;
        private string _listViewRowsXPath = null;

        /// <summary>
        /// If parent element is of type ListView use this constructor
        /// </summary>
        /// <param name="driver"></param>
        public ListViewWrapper(AppiumDriver<IWebElement> driver) : this(driver, DefaultAndroidLocator)
        {
        }

        /// <summary>
        /// If parent element is other than ListView use this constructor
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="xPathAndroid"></param>
        public ListViewWrapper(AppiumDriver<IWebElement> driver, string xPathAndroid) : base(driver)
        {
            StringQA.ThrowIfNullOrWhiteSpace(xPathAndroid);

            // will look something like //android.widget.ListView
            _listViewXPath = xPathAndroid;

            // will look something like //android.widget.ListView/*
            _listViewRowsXPath = xPathAndroid + "/*";
        }

        public override bool IsLoaded()
        {
            return IsVisible(By.XPath(_listViewXPath));
        }

        public ListViewWrapper AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded());
            return this;
        }

        public ListViewWrapper WaitForRowCount(int count)
        {
            WaitUntil((args) => GetRowCount() == count);
            return this;
        }

        public int GetRowCount()
        {
            return GetVisibleChildRows().Count;
        }

        private List<IWebElement> GetVisibleChildRows()
        {
            return _driver.FindElementsByXPath(_listViewRowsXPath).Where(ele => ele.Displayed).ToList();
        }

        public List<ListViewRowWrapper> GetRows()
        {
            var retVal = new List<ListViewRowWrapper>();

            // The XPath Will look like //android.widget.ListView/*[1]
            var childList = GetVisibleChildRows();
            for (int i = 0; i < childList.Count; i++)
            {
                var xPath = string.Format("{0}[{1}]", _listViewRowsXPath, i + 1);
                retVal.Add(new ListViewRowWrapper(_driver, childList[i], xPath));
            }

            return retVal;
        }

        public static int IndexOf(List<ListViewRowWrapper> rows, List<string> hasText)
        {
            return rows.FindIndex(row => row.HasText(hasText));
        }

        public static ListViewRowWrapper GetRow(List<ListViewRowWrapper> rows, List<string> hasText)
        {
            int index = IndexOf(rows, hasText);
            return (index) >= 0 ? rows[index] : null;
        }

    }
}
