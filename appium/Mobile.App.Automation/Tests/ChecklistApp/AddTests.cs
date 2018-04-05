using Applitools.Appium;
using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    internal class AddTests : ChecklistTestBase
    {
        public AddTests(AppCapabilities capabilities) 
            : base(capabilities) 
        {
        }

        private EditItemPage ClickAddNewItem(out int rowCount)
        {
            rowCount = _pageObject.Checklist.GetRowCount();

            return _pageObject.MenuBar.ClickAdd();
        }

        [Test]
        public void AddItemTest()
        {
            Eyes eyes = null;
            try
            {
                eyes = GetEyes("AddItemTest");

                Assert.IsTrue(_pageObject.IsLoaded());

                eyes.CheckWindow("List (Pre Add Item)");

                var newText = "NEW LINE " + RandomHelper.RandomString(7);
                ClickAddNewItem(out int preCount)
                    .EnterText(newText)
                    .ClickAddDone();

                _pageObject.Checklist.WaitForRowCount(preCount + 1);

                eyes.CheckWindow("List (Post Add Item)");
                eyes.Close();

                Assert.IsNotNull(_pageObject.Checklist.GetRow(newText), "Row not found: " + newText);

            }
            finally
            {
                eyes?.AbortIfNotClosed();
            }

        }

        [Test]
        public void ClickAddThenCancelTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            ClickAddNewItem(out int preCount)
                .ClickCancel();

            //_pageObject.HideKeyboard();

            _pageObject.Checklist.WaitForRowCount(preCount);

        }

        [Test]
        public void ClickAddEnterTextThenCancelTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            var newText = RandomHelper.RandomString(7);
            ClickAddNewItem(out int preCount)
                .EnterText(newText)
                .ClickCancel();

            _pageObject.Checklist.WaitForRowCount(preCount);

            Assert.IsNull(_pageObject.Checklist.GetRow(newText), "Cancel row was found in main list");

        }


    }
}
