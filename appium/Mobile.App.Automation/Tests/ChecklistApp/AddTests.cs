using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    internal class AddTests : ChecklistTestBase
    {
        public AddTests(DesiredCapabilities capabilities) 
            : base(capabilities) 
        {
        }

        private EditItemPage ClickAddNewItem(out int rowCount)
        {
            rowCount = _pageObject.Checklist.GetRowCount();
            
            _pageObject.MenuBar.Add.Click();

            return new EditItemPage(_driver);
        }

        [Test]
        [Ignore(NUnit_Category)]
        public void AddItemTest()
        {
            //Eyes eyes = null;
            try
            {
                //eyes = GetEyes("AddItemTest");

                Assert.IsTrue(_pageObject.IsLoaded());

                //eyes.CheckWindow("List (Pre Add Item)");

                var newText = "NEW LINE " + RandomHelper.RandomString(7);
                var addPage = ClickAddNewItem(out int preCount);
                addPage.TxtEdit.PressKeys(newText);
                addPage.AddDoneButton.Click();

                _pageObject.Checklist.WaitForRowCount(preCount + 1);

                //eyes.CheckWindow("List (Post Add Item)");
                //eyes.Close();

                Assert.IsNotNull(_pageObject.Checklist.GetRow(newText), "Row not found: " + newText);

            }
            finally
            {
                //eyes?.AbortIfNotClosed();
            }

        }

        [Test]
        [Ignore(NUnit_Category)]
        public void ClickAddThenCancelTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            ClickAddNewItem(out int preCount)
                .CancelButton.Click();

            _pageObject.Checklist.WaitForRowCount(preCount);

        }

        [Test]
        [Ignore(NUnit_Category)]
        public void ClickAddEnterTextThenCancelTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            var newText = RandomHelper.RandomString(7);
            var editPage = ClickAddNewItem(out int preCount);
            editPage.TxtEdit.PressKeys(newText);
            editPage.CancelButton.Click();

            _pageObject.Checklist.WaitForRowCount(preCount);

            Assert.IsNull(_pageObject.Checklist.GetRow(newText), "Cancel row was found in main list");

        }


    }
}
