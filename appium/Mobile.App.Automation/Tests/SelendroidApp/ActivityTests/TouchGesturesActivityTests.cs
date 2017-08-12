using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    [TestFixtureSource("GalaxyS4")]
    class TouchGesturesActivityTests : TestBaseGeneric<HomeScreenPage>
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private TouchGesturesPage _touchGesturesPage = null;

        public TouchGesturesActivityTests(string capabilities) 
            : base(capabilities) 
        {
        }

        /// <summary>
        /// Navigate to the gestures page before starting
        /// NOTE: We only want to run this code once as after the first time we are on the correct page
        /// </summary>
        public override void TestSetup()
        {
            if(_touchGesturesPage == null)
            {
                _touchGesturesPage = _pageObject.ClickTouchActions();
                Assert.AreEqual(_touchGesturesPage.GetAndroidDriver().CurrentActivity, GalaxyS4Capabilities.TouchGesturesActivity);
                Assert.IsTrue(_touchGesturesPage.IsLoaded(), "Activity is not loaded");
            }
        }

        [Test]
        public void SingleTapTest()
        {
            _touchGesturesPage
                .SingleTap()
                .AssertGestureText("SINGLE TAP CONFIRMED");
        }

        [Test]
        public void ShortPressTest()
        {
            _touchGesturesPage
                .PressAndHold(500)
                .AssertGestureText("SINGLE TAP CONFIRMED");
        }

        [Test]
        public void LongPressTest()
        {
            _touchGesturesPage
                .PressAndHold(2000)
                .AssertGestureText("LONG PRESS");
        }

        [Test]
        [Ignore("not working yet")]
        public void DoubleTapTest()
        {
            _touchGesturesPage
                .DoubleTap()
                .AssertGestureText("DOUBLE TAP EVENT");
        }

        [Test]
        public void FlickUpTest()
        {
            _touchGesturesPage
                .FlickUp(60)
                .AssertGestureText("FLICK")
                .AssertYCoords(true);
        }

        [Test]
        public void FlickDownTest()
        {
            _touchGesturesPage
                .FlickDown()
                .AssertGestureText("FLICK")
                .AssertYCoords(false);
        }

        [Test]
        public void Canvastest()
        {
            _touchGesturesPage.ClickCanvas();

            const int X = 100;

            var windowSize = _driver.Manage().Window.Size;

            // up
            Repeat(() => _touchGesturesPage.Flick(X, windowSize.Height - 100, X, 100, 1000));

            // down
            Repeat(() => _touchGesturesPage.Flick(X + 100, 200, X + 100, windowSize.Height - 200, 700));

            // left
            Repeat(() => _touchGesturesPage.Flick(X, 500, windowSize.Width - X, 500, 400));

            // right
            Repeat(() => _touchGesturesPage.Flick(windowSize.Width - X, 600, X, 600, 1000));

        }

        private void Repeat(Action condition)
        {
            for(int i=1; i<=3; i++)
            {
                condition();
            }
        }

        public void ScrollTest()
        {
        }

        public void PinchTest()
        {
        }

        public void ZoomTest()
        {
        }


        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.HomeScreenCapabilites();
        }

    }
}
