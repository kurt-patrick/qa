﻿using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    class TouchGesturesActivityTests : SelendroidAppTestBaseGeneric<HomeScreenPage>
    {
        private TouchGesturesPage _touchGesturesPage = null;

        public TouchGesturesActivityTests(DesiredCapabilities caps) 
            : base(caps) 
        {
            OnTestSetupEventHandler += OnTestSetup;
        }

        /// <summary>
        /// Navigate to the gestures page before starting
        /// NOTE: We only want to run this code once as after the first time we are on the correct page
        /// </summary>
        void OnTestSetup()
        {
            if(_touchGesturesPage == null)
            {
                _pageObject.TouchActions.Click();
                _touchGesturesPage = Get<TouchGesturesPage>(true);
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
                .DoubleTap(550)
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

    }
}
