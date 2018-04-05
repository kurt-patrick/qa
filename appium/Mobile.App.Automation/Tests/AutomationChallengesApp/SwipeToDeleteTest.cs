using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    internal class SwipeToDeleteTest : AutomationChallengeTestBase<SwipeToDeletePage>
    {
        const string ZeroRowsSwiped = "0 rows swiped";

        public SwipeToDeleteTest(AppCapabilities caps) : base(caps)
        {
        }

        public override void TestSetup()
        {
            _navigationDrawerPage.OpenDrawer().SwipeChallenge();
            Assert.AreEqual(ZeroRowsSwiped, _pageObject.Actual);
        }

        [Test]
        public void SwipeLeftTest()
        {
            new List<int> { 1, 2, 3 }.ForEach(SwipeAndAssert);
        }

        void SwipeAndAssert(int index)
        {
            if(index <= 0 || index > 3)
            {
                throw new Exceptions.InvalidParameterException("index must be between 1 and 3");
            }

            // swipe the first swipe (left to right)
            _pageObject.SwipeFirstRow();

            // Assert the ui updated as expected
            if (index < 3)
            {
                Assert.AreEqual($"{index} of 3 rows swiped", _pageObject.Actual);
            }
            else
            {
                Assert.AreEqual(Success, _pageObject.Actual);
            }

        }

    }
}
