using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    internal class AlertDialogTest : AutomationChallengeTestBase<AlertDialogPage>
    {
        public AlertDialogTest(DesiredCapabilities capabilities) : base(capabilities)
        {
        }

        public override void TestSetup()
        {
            _navigationDrawerPage.OpenDrawer().AlertChallenge();
            Assert.IsTrue(_pageObject.IsLoaded());
        }

        [Test]
        public void AlertSuccessTest()
        {
            var answer = _pageObject.IsAnswerCorrect();
            _pageObject.AnswerQuestion(answer);
            Assert.AreEqual(Success, _pageObject.Actual);
        }

        [Test]
        public void AlertFailTest()
        {
            var answer = _pageObject.IsAnswerCorrect();
            _pageObject.AnswerQuestion(!answer);
            Assert.AreEqual(Fail, _pageObject.Actual);
        }

    }
}
