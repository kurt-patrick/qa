using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPE.Mobile.App.Automation.Configuration;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    internal class PinCodeTest : AutomationChallengeTestBase<PinCodePage>
    {
        public PinCodeTest(DriverCapabilities caps) : base(caps)
        {
        }

        public override void Setup()
        {
            _navigationDrawerPage.OpenDrawer();
            _navigationDrawerPage.PinCode();
            Assert.AreEqual("Click to enter the pin", _pageObject.PinEntered);
        }

        [Test]
        public void SuccessTest()
        {
            string pinEntered = "";
            var pinList = _pageObject.GetPin();
            for(int index=0; index<pinList.Count-1; index++)
            {
                // Click the appropriate button
                _pageObject.ClickPinNumber(pinList[index]);

                // Assert the ui updated as expected
                pinEntered += pinList[index].ToString();
                Assert.AreEqual($"PIN entered: {pinEntered}", _pageObject.PinEntered);
            }

            // Enter the last pin number and assert success
            _pageObject.ClickPinNumber(pinList.Last());
            Assert.AreEqual(Success, _pageObject.PinEntered);
        }

        [Test]
        public void FailTest()
        {
            _pageObject.EnterPIN(new List<int> { 9, 9, 9, 9, 9, 9 });
            Assert.AreEqual(Fail, _pageObject.PinEntered);
        }

    }
}
