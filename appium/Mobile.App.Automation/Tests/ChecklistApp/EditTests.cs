using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Linq;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    internal class EditTests : ChecklistTestBase
    {
        public EditTests(DesiredCapabilities capabilities) 
            : base(capabilities) 
        {
        }

        [Test]
        public void EditItemTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());

            var rows = _pageObject.Checklist.GetRows();
            int preCount = rows.Count;

            Assert.AreNotEqual(0, preCount);

            // Pick a random row
            var row = RandomHelper.RandomObject<ListViewRowWrapper>(rows);

            string newText = RandomHelper.RandomString(8);
            string originalText = row.TextCache().First();

            // Put row into edit mode
            row.TapRow<EditItemPage>()
                .AssertLoaded()
                .AssertText(originalText)
                .EnterText(newText)
                .ClickAddDone();

            _pageObject.Checklist.WaitForRowCount(preCount);

            // Get the new list of rows
            rows = _pageObject.Checklist.GetRows();

            // assert the updated text is visible
            int indexOf = ListViewWrapper.IndexOf(rows, newText);
            Assert.AreNotEqual(-1, indexOf);

            // asert the original text is not displayed
            indexOf = ListViewWrapper.IndexOf(rows, originalText);
            Assert.AreEqual(-1, indexOf);

        }

    }
}
