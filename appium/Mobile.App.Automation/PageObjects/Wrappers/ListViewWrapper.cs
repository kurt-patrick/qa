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

        protected string _listViewXPath = null;
        protected string _listViewRowsXPath = null;

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

        public virtual ListViewWrapper WaitForRowCount(int count)
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
            // NOTE: If the implicit wait is not set the performance is very slow e.g. 40 second wait when no rows exist
            _driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromMilliseconds(100);
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

        public static int IndexOf(List<ListViewRowWrapper> rows, params string[] text)
        {
            var list = text.ToList();
            return rows.FindIndex(row => row.HasText(list));
        }

        public static List<int> IndexesOf(List<ListViewRowWrapper> rows, params string[] text)
        {
            var list = text.ToList();
            var retVal = new List<int>();
            for (int index=0; index< rows.Count; index++)
            {
                if(rows[index].HasText(list))
                {
                    retVal.Add(index);
                }
            }
            return retVal;
        }

        public ListViewRowWrapper GetRow(params string[] text)
        {
            var rows = GetRows();
            return GetRow(rows, text);
        }

        public static ListViewRowWrapper GetRow(List<ListViewRowWrapper> rows, params string[] text)
        {
            int index = IndexOf(rows, text);
            return (index) >= 0 ? rows[index] : null;
        }

    }
}
