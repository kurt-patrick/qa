using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    internal class DeleteTests : ChecklistTestBase
    {
        public DeleteTests(DesiredCapabilities capabilities) 
            : base(capabilities) 
        {
        }

        private List<ListViewRowWrapper> AddItem()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            var rows = _pageObject.Checklist.GetRows();
            int preCount = rows.Count;

            _pageObject.MenuBar.Add.Click();
            var editPage = new EditItemPage(_driver);
            editPage.TxtEdit.PressKeys(RandomHelper.RandomString(6));
            editPage.AddDoneButton.Click();

            _pageObject.Checklist.WaitForRowCount(preCount + 1);

            return _pageObject.Checklist.GetRows();
        }

        [Test]
        public void DeleteSingleItemTest()
        {
            var rows = AddItem();
            int preCount = rows.Count;

            Assert.AreNotEqual(0, preCount);

            // Select a random row to be deleted
            int rowIndex = RandomHelper.RandomIndex(rows);

            var row = rows[rowIndex];
            string rowText = row.TextCache().First();
            row.ToggleCheckBox(true);

            // click delete
            _pageObject.MenuBar.Delete.Click();

            // wait for row count to decrease by 1
            _pageObject.Checklist.WaitForRowCount(preCount - 1);

            // update the row objects
            rows = _pageObject.Checklist.GetRows();

            Assert.AreEqual(preCount - 1, rows.Count);

            // assert the delete row does not exist
            row = ListViewWrapper.GetRow(rows, rowText);
            Assert.IsNull(row, "The deleted row still exists");

        }

        [Test]
        public void DeleteAllItemsTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            var rows = _pageObject.Checklist.GetRows();
            int preCount = rows.Count;

            Assert.AreNotEqual(0, preCount);

            rows.ForEach(row => row.ToggleCheckBox(true));

            // click delete
            _pageObject.MenuBar.Delete.Click();

            // wait for row count to decrease to 0
            _pageObject.Checklist.WaitForRowCount(0);

            Assert.AreEqual(0, _pageObject.Checklist.GetRowCount());

        }

        [Test]
        public void ClickDeleteWithNoItemsSelectedTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            int preCount = _pageObject.Checklist.GetRowCount();

            Assert.AreNotEqual(0, preCount);

            // click delete
            _pageObject.MenuBar.Delete.Click();

            // Wait for row count to remain same
            _pageObject.Checklist.WaitForRowCount(preCount);

            // update the row objects
            Assert.AreEqual(preCount, _pageObject.Checklist.GetRowCount());

        }

    }
}
