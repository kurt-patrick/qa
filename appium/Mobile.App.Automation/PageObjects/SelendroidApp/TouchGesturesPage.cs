﻿using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class TouchGesturesPage : PageBase
    {
        // Always visible elements
        MobileElementWrapper LastGesture => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/last_gesture_text_view"));
        MobileElementWrapper GestureType => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/gesture_type_text_view"));
        MobileElementWrapper ScaleFactor => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/scale_factor_text_view"));
        MobileElementWrapper ParentElement => new MobileElementWrapper(_driver, By.Id("io.selendroid.testapp:id/LinearLayout1"));

        // Sometimes visible elements
        MobileElementWrapper XCoordsLocator => new MobileElementWrapper(_driver, By.Id("text_view3"));
        MobileElementWrapper YCoordsLocator => new MobileElementWrapper(_driver, By.Id("text_view4"));

        public TouchGesturesPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public List<double?> GetCoords()
        {
            var retVal = new List<double?>();
            retVal.Add(GetXCoords());
            retVal.Add(GetYCoords());
            return retVal;
        }

        public double? GetXCoords()
        {
            return GetCoords(XCoordsLocator, "vx: (.+) pps");
        }

        public double? GetYCoords()
        {
            return GetCoords(YCoordsLocator, "vy: (.+) pps");
        }

        private double? GetCoords(MobileElementWrapper element, string pattern)
        {
            double? retVal = null;
            string text = element.Text(true);
            if(Regex.IsMatch(text, pattern))
            {
                var matches = Regex.Match(text, pattern);
                var groupValue = matches.Groups[1].Value;
                retVal = double.Parse(groupValue);
            }
            return retVal;
        }

        /// <summary>
        /// if <paramref name="scrollUp"/> is true then the y coords should be a negative value
        /// if <paramref name="scrollUp"/> is false then the y coords should be a positive value        
        /// </summary>
        /// <param name="scrollUp"></param>
        public void AssertYCoords(bool scrollUp)
        {
            var coords = GetYCoords();
            Assert.IsTrue(coords.HasValue, "Y Coords not found");
            if (scrollUp)
            {
                Assert.IsTrue(coords.Value < 1, $"Expected negative value ({coords.Value})");
            }
            else
            {
                Assert.IsTrue(coords.Value > -1, $"Expected positive value ({coords.Value})");
            }
        }

        /// <summary>
        /// if <paramref name="scrollLeft"/> is true then the y coords should be less than 1 or negative
        /// if <paramref name="scrollLeft"/> is false then the y coords should be a positive value        
        /// </summary>
        /// <param name="scrollLeft"></param>
        public void AssertXCoords(bool scrollLeft)
        {
            var coords = GetXCoords();
            Assert.IsTrue(coords.HasValue, "X Coords not found");
            if (scrollLeft)
            {
                Assert.IsTrue(coords.Value < 1, $"Expected negative value ({coords.Value})");
            }
            else
            {
                Assert.IsTrue(coords.Value > -1, $"Expected positive value ({coords.Value})");
            }
        }

        public TouchGesturesPage AssertGestureText(string expected)
        {
            Assert.AreEqual(expected, GestureType.Text(true));
            return this;
        }

        public TouchGesturesPage SingleTap()
        {
            var element = ((IWebElementReference)ParentElement.NativeWrapper()).Element();
            TouchAction().Tap(element).Perform();
            return this;
        }

        public TouchGesturesPage DoubleTap(long ms)
        {
            var windowSize = _driver.Manage().Window.Size;

            int xPos = (int)(0.5 * windowSize.Width);
            int yPos = (int)(0.5 * windowSize.Height);

            // NOTE: Douple tap is flaky and only works around 30-40% of the time regardless of ms
            // I have tested waits ranging from 50 to 1000ms and the best result were between 400-550ms
            Func<bool> condition = () =>
            {
                TouchAction()
                    .Tap(xPos, yPos, 2)
                    .Wait(ms)
                    .Perform();

                return "ON DOUBLE TAP EVENT".Equals(GestureType.Text(true));
            };

            WaitUntil((arg) => condition());

            return this;
        }

        public TouchGesturesPage PressAndHold(long ms)
        {
            var element = ((IWebElementReference)ParentElement.NativeWrapper()).Element();

            TouchAction().Press(element)
                .Wait(ms)
                .Release()
                .Perform();

            return this;
        }

        public TouchGesturesPage FlickVertical(bool up)
        {
            var windowSize = _driver.Manage().Window.Size;

            int xPos = (int)(0.5 * windowSize.Width);
            int yBottom = (int)(0.75 * windowSize.Height);
            int yTop = (int)(0.25 * windowSize.Height);

            return (up) ? 
                Flick(xPos, yBottom, xPos, yTop, 1000) : 
                Flick(xPos, yTop, xPos, yBottom, 1000);
        }

        public TouchGesturesPage FlickHorizontal(bool left)
        {
            var windowSize = _driver.Manage().Window.Size;

            int yPos = (int)(0.5 * windowSize.Height);
            int xLeft = (int)(0.25 * windowSize.Width);
            int xRight = (int)(0.75 * windowSize.Width);

            return (left) ?
                Flick(yPos, xRight, yPos, xLeft, 1000) :
                Flick(yPos, xLeft, yPos, xRight, 1000);
        }

        /// <summary>
        /// The lower <paramref name="ms"/> is the faster the flick will be and the greater the distance it will travel
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        public TouchGesturesPage Flick(double startX, double startY, double endX, double endY, long ms)
        {
            TouchAction()
                .Press(startX, startY)
                .Wait(ms)
                .MoveTo(endX, endY)
                .Release()
                .Perform();
            return this;
        }

        public TouchGesturesPage FlickParentElement()
        {
            var element = ((IWebElementReference)ParentElement.NativeWrapper()).Element();
            var size = element.Size;

            TouchAction()
                .Press(element, size.Width * 0.25, size.Height * 0.25)
                .Wait(500)
                .MoveTo(size.Width * 0.75, size.Height * 0.75)
                .Release()
                .Perform();

            return this;
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(ParentElement, LastGesture, GestureType, ScaleFactor);
        }

        public TouchGesturesPage AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "TouchGesturesPage is not loaded");
            return this;
        }

    }
}
