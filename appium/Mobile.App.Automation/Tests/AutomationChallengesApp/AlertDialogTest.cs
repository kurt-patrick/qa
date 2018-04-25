using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    internal class AlertDialogTest : AutomationChallengeTestBase<AlertDialogPage>
    {
        public AlertDialogTest(DesiredCapabilities capabilities) : base(capabilities)
        {
            OnTestSetupEventHandler += OnTestSetup;
        }

        void OnTestSetup()
        {
            _navigationDrawerPage.OpenDrawer().AlertChallenge();
            Assert.IsTrue(_pageObject.IsLoaded());
        }

        [Test]
        [Category(NUnit_Category)]
        public void AlertSuccessTest()
        {
            var answer = _pageObject.IsAnswerCorrect();
            _pageObject.AnswerQuestion(answer);
            Assert.IsTrue(_pageObject.Actual.Displayed());
            Assert.AreEqual(Success, _pageObject.Actual.Text());
        }

        [Test]
        [Category(NUnit_Category)]
        public void AlertFailTest()
        {
            var answer = _pageObject.IsAnswerCorrect();
            _pageObject.AnswerQuestion(!answer);
            Assert.IsTrue(_pageObject.Actual.Displayed());
            Assert.AreEqual(Fail, _pageObject.Actual.Text());
        }

    }
}
