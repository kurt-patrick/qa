using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    [Ignore("todo: finish apk and tests")]
    internal class UserControlsTest : AutomationChallengeTestBase<UserControlsPage>
    {
        public UserControlsTest(DesiredCapabilities capabilities) : base(capabilities)
        {
            OnTestSetupEventHandler += OnTestSetup;
        }

        void OnTestSetup()
        {
            _navigationDrawerPage.OpenDrawer().ControlsChallenge();
            Assert.IsTrue(_pageObject.IsLoaded());
        }

        [Test]
        [Category(NUnit_Category)]
        public void AlertSuccessTest()
        {
            _pageObject.ValidateButton.Click();
            Assert.AreEqual("State: Fail", _pageObject.TxtValidate.Text());
        }

        [Test]
        [Category(NUnit_Category)]
        public void AlertFailTest()
        {
            _pageObject.ValidateButton.Click();
            Assert.AreEqual("State: Fail", _pageObject.TxtValidate.Text());
        }

    }
}
