using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    internal class AddTests : ChecklistTestBase
    {
        public AddTests(DriverCapabilities capabilities) 
            : base(capabilities) 
        {
        }

        private int AssertLoadedAndClickAdd()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            int preCount = _pageObject.Checklist.GetRowCount();

            _pageObject.MenuBar.ClickAdd();

            return preCount;
        }

        [Test]
        public void AddItemTest()
        {
            int preCount = AssertLoadedAndClickAdd();

            var newText = StringHelper.RandomString(7);
            _pageObject
                .SwitchPageObject<EditItemPage>()
                .EnterText(newText)
                .ClickAddDone();

            _pageObject.Checklist.WaitForRowCount(preCount + 1);

            Assert.AreEqual(preCount + 1, _pageObject.Checklist.GetRowCount());

            var rows = _pageObject.Checklist.GetRows();
            int indexOf = ListViewWrapper.IndexOf(rows, newText);

            Assert.AreNotEqual(-1, indexOf);

        }

        [Test]
        public void ClickAddThenCancelTest()
        {
            int preCount = AssertLoadedAndClickAdd();

            var newText = StringHelper.RandomString(7);
            _pageObject
                .SwitchPageObject<EditItemPage>()
                .ClickCancel();

            _pageObject.HideKeyboard();

            _pageObject.Checklist.WaitForRowCount(preCount);

            Assert.AreEqual(preCount, _pageObject.Checklist.GetRowCount());

        }

        [Test]
        public void ClickAddEnterTextThenCancelTest()
        {
            int preCount = AssertLoadedAndClickAdd();

            var newText = StringHelper.RandomString(7);
            _pageObject
                .SwitchPageObject<EditItemPage>()
                .EnterText(newText)
                .ClickCancel();

            _pageObject.Checklist.WaitForRowCount(preCount);

            var rows = _pageObject.Checklist.GetRows();
            Assert.AreEqual(preCount, rows.Count);

            Assert.IsNull(ListViewWrapper.GetRow(rows, newText), "Cancel row was found in main list");

        }


    }
}
