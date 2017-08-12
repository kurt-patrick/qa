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
        public void DoubleTapTest()
        {
            _touchGesturesPage
                .DoubleTap(500)
                .AssertGestureText("ON DOUBLE TAP EVENT");
        }

        [Test]
        public void FlickUpTest()
        {
            _touchGesturesPage
                .FlickVertical(true)
                .AssertGestureText("FLICK")
                .AssertYCoords(true);
        }

        [Test]
        public void FlickDownTest()
        {
            _touchGesturesPage
                .FlickVertical(false)
                .AssertGestureText("FLICK")
                .AssertYCoords(false);
        }

        [Test]
        public void FlickLeftTest()
        {
            _touchGesturesPage
                .FlickHorizontal(true)
                .AssertGestureText("FLICK")
                .AssertXCoords(true);
        }

        [Test]
        public void FlickRightTest()
        {
            _touchGesturesPage
                .FlickHorizontal(false)
                .AssertGestureText("FLICK")
                .AssertXCoords(false);
        }

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.HomeScreenCapabilites();
        }

    }
}
