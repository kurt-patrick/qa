using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    internal class PinCodeTest : AutomationChallengeTestBase<PinCodePage>
    {
        const string ClickToEnterPin = "Click to enter the pin";

        public PinCodeTest(DesiredCapabilities capabilities) : base(capabilities)
        {
            OnTestSetupEventHandler += OnTestSetup;
        }

        void OnTestSetup()
        {
            _navigationDrawerPage.OpenDrawer().PinChallenge();
            Assert.AreEqual(ClickToEnterPin, _pageObject.PinEntered.Text(true));
        }

        [Test]
        public void PinSuccessTest()
        {
            string pinEntered = "";
            var pinList = _pageObject.GetPin();
            for(int index=0; index<pinList.Count-1; index++)
            {
                // Click the appropriate button
                _pageObject.ClickPinNumber(pinList[index]);

                // Assert the ui updated as expected
                pinEntered += pinList[index].ToString();
                Assert.AreEqual($"PIN entered: {pinEntered}", _pageObject.PinEntered.Text(true));
            }

            // Enter the last pin number and assert success
            _pageObject.ClickPinNumber(pinList.Last());
            Assert.AreEqual(Success, _pageObject.PinEntered.Text(true));
        }

        [Test]
        public void PinFailTest()
        {
            _pageObject.EnterPIN(new List<int> { 9, 9, 9, 9  });
            Assert.AreEqual(Fail, _pageObject.PinEntered.Text(true));
        }

    }
}
