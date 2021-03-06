﻿using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    internal class ResultListTest : AutomationChallengeTestBase<ResultListPage>
    {
        const string ZeroRowsClicked = "0 rows clicked";

        public ResultListTest(DesiredCapabilities capabilities) : base(capabilities)
        {
            OnTestSetupEventHandler += OnTestSetup;
        }

        void OnTestSetup()
        {
            _navigationDrawerPage.OpenDrawer().ListChallenge();
            var actualToString = _pageObject.Actual.ToString();
            var actualText = _pageObject.Actual.Text();
            bool areEqual = ZeroRowsClicked.Equals(actualToString);
            Assert.AreEqual(ZeroRowsClicked, actualToString);
        }

        [Test]
        [Category(NUnit_Category)]
        public void ResultsSuccessTest()
        {
            var rows = _pageObject.GetRows();
            var expectedNumbers = _pageObject.GetExpected();

            var indexes = ListViewWrapper.IndexesOf(rows, expectedNumbers);
            Assert.AreNotEqual(0, indexes);

            indexes.ForEach(index => rows[index].TapRow());

            Assert.AreEqual(Success, _pageObject.Actual.Text());
        }

        [Test]
        [Category(NUnit_Category)]
        public void ResultsFailTest()
        {
            var rows = _pageObject.GetRows();
            Assert.AreNotSame(0, rows.Count);

            for (int index=0; index<rows.Count; index++)
            {
                rows[index].TapRow();
                if(Fail.Equals(_pageObject.Actual.Text()))
                {
                    break;
                }
            }
            Assert.AreEqual(Fail, _pageObject.Actual.Text());
        }

    }
}
