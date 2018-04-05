using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    internal class UserControlsTest : AutomationChallengeTestBase<UserControlsPage>
    {
        public UserControlsTest(AppCapabilities caps) : base(caps)
        {
        }

        public override void TestSetup()
        {
            _navigationDrawerPage.OpenDrawer().ControlsChallenge();
            Assert.IsTrue(_pageObject.IsLoaded());
        }

        [Test]
        public void AlertSuccessTest()
        {
            _pageObject.ClickValidate();
            Assert.AreEqual(Fail, _pageObject.ValidateText);
        }

        [Test]
        public void AlertFailTest()
        {
            _pageObject.ClickValidate();
            Assert.AreEqual(Fail, _pageObject.ValidateText);
        }

    }
}
