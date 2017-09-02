using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    internal class AddTests : ChecklistTestBase
    {
        public AddTests(DriverCapabilities capabilities) 
            : base(capabilities) 
        {
        }

        private EditItemPage ClickAddNewItem(out int rowCount)
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            rowCount = _pageObject.Checklist.GetRowCount();

            return _pageObject.MenuBar.ClickAdd();
        }

        [Test]
        public void AddItemTest()
        {
            var newText = RandomHelper.RandomString(7);
            ClickAddNewItem(out int preCount)
                .EnterText(newText)
                .ClickAddDone();

            _pageObject.Checklist.WaitForRowCount(preCount + 1);

            Assert.IsNotNull(_pageObject.Checklist.GetRow(newText), "Row not found: " + newText);

        }

        [Test]
        public void ClickAddThenCancelTest()
        {
            ClickAddNewItem(out int preCount)
                .ClickCancel();

            //_pageObject.HideKeyboard();

            _pageObject.Checklist.WaitForRowCount(preCount);

        }

        [Test]
        public void ClickAddEnterTextThenCancelTest()
        {
            var newText = RandomHelper.RandomString(7);
            ClickAddNewItem(out int preCount)
                .EnterText(newText)
                .ClickCancel();

            _pageObject.Checklist.WaitForRowCount(preCount);

            Assert.IsNull(_pageObject.Checklist.GetRow(newText), "Cancel row was found in main list");

        }


    }
}
