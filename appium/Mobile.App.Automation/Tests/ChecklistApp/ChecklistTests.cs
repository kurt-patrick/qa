using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    [TestFixtureSource(TestFixtureSourceName)]
    class ChecklistTests : TestBaseGeneric<MainPage>
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ChecklistTests(string capabilities) 
            : base(capabilities) 
        {
        }

        [Test]
        public void AddItemTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            int preCount = _pageObject.Checklist.GetRowCount();

            _pageObject.MenuBar.ClickAdd();

            var newText = StringHelper.RandomString(7);
            _pageObject
                .SwitchPageObject<EditItemPage>()
                .EnterText(newText)
                .ClickAddDone();

            _pageObject.Checklist.WaitForRowCount(preCount + 1);

            Assert.AreEqual(preCount + 1, _pageObject.Checklist.GetRowCount());

            var rows = _pageObject.Checklist.GetRows();
            int indexOf = ListViewWrapper.IndexOf(rows, new List<string> { newText });

            Assert.AreNotEqual(-1, indexOf);

        }

        [Test]
        public void EditItemTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            var rows = _pageObject.Checklist.GetRows();
            int preCount = rows.Count;

            Assert.AreNotEqual(0, preCount);

            // randomly put a row into edit mode
            ListViewRowWrapper row = rows[RandomHelper.RandomIndex(rows)];
            string originalText = row.TextCache().First();
            row.TapRow();

            var newText = StringHelper.RandomString(8);
            _pageObject
                .SwitchPageObject<EditItemPage>()
                .AssertLoaded()
                .AssertText(originalText)
                .EnterText(newText)
                .ClickAddDone();

            _pageObject.Checklist.WaitForRowCount(preCount);

            Assert.AreEqual(preCount, _pageObject.Checklist.GetRowCount());

            rows = _pageObject.Checklist.GetRows();

            // assert the updated text is visible
            int indexOf = ListViewWrapper.IndexOf(rows, new List<string> { newText });
            Assert.AreNotEqual(-1, indexOf);

            // asert the original text is not displayed
            indexOf = ListViewWrapper.IndexOf(rows, new List<string> { originalText });
            Assert.AreEqual(-1, indexOf);

        }

        public static List<string> CapabilitiesList()
        {
            return ChecklistAppCapabilities.GetCapabilities();
        }

    }
}
