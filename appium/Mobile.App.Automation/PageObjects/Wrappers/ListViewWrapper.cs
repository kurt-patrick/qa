using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.QA;

namespace KPE.Mobile.App.Automation.PageObjects.Wrappers
{
    abstract class ListViewWrapper : PageBase
    {
        private string _listViewXPath = null;
        private string _listViewRowsXPath = null;

        /// <summary>
        /// I have only added android here, however you would also do the same for iOS
        /// </summary>
        public ListViewWrapper(TestCaseSettings settings, string xPathAndroid) : base(settings)
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
            return _driver.FindElementsByXPath(_listViewRowsXPath).Count;
        }

        public List<ListViewRowWrapper> GetRows()
        {
            var retVal = new List<ListViewRowWrapper>();

            // Get all the child rows of the listview
            string xPath = null;
            var children = _driver.FindElementsByXPath(_listViewRowsXPath);
            for(int i=0; i<children.Count; i++)
            {
                // will look something like //android.widget.ListView/*
                xPath = string.Format("{0}[{1}]", _listViewRowsXPath, i + 1);
                retVal.Add(new ListViewRowWrapper(_testCaseSettings, children[i], xPath));
            }
            return retVal;
        }

        public static int IndexOf(List<ListViewRowWrapper> rows, List<string> hasText)
        {
            return rows.FindIndex(row => row.HasText(hasText));
        }

    }
}
