using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using NUnit.Framework;
using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    public class TouchGesturesPage : PageBase
    {
        // Always visible elements
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/last_gesture_text_view")]
        private IWebElement _lastGesture = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/gesture_type_text_view")]
        private IWebElement _gestureType = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/scale_factor_text_view")]
        private IWebElement _scaleFactor = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/LinearLayout1")]
        private IWebElement _parentElement = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "io.selendroid.testapp:id/canvas_button")]
        private IWebElement _canvas = null;

        // Sometimes visible elements
        private By _xCoordsLocator = By.Id("text_view3");   // vx: -259.7958 pps
        private By _yCoordsLocator = By.Id("text_view4");   // vy: 19.668333 pps

        public TouchGesturesPage(TestCaseSettings settings) : base(settings)
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
            return GetCoords(_xCoordsLocator, "vx: (.+) pps");
        }

        public double? GetYCoords()
        {
            return GetCoords(_yCoordsLocator, "vy: (.+) pps");
        }

        private double? GetCoords(By locator, string pattern)
        {
            double? retVal = null;
            if(IsVisible(locator, out IWebElement element))
            {
                string text = GetText(element, true);
                if(Regex.IsMatch(text, pattern))
                {
                    var matches = Regex.Match(text, pattern);
                    var groupValue = matches.Groups[1].Value;
                    retVal = double.Parse(groupValue);
                }
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
            var yCoord = GetYCoords();
            Assert.IsTrue(yCoord.HasValue, "Y Coords not found");
            if (scrollUp)
            {
                Assert.IsTrue(yCoord.Value < 0, $"Expected negative value ({yCoord.Value})");
            }
            else
            {
                Assert.IsTrue(yCoord.Value > 0, $"Expected positive value ({yCoord.Value})");
            }

        }

        public TouchGesturesPage AssertGestureText(string expected)
        {
            Assert.AreEqual(expected, GetText(_gestureType, true));
            return this;
        }

        public TouchGesturesPage SingleTap()
        {
            GetTouchAction()
                .Tap(_parentElement)
                .Perform();
            return this;
        }

        public TouchGesturesPage DoubleTap()
        {
            var parentElement = _parentElement;
            GetMultiAction()
                .Add(GetTouchAction().Press(parentElement).Release())
                .Add(GetTouchAction().Press(parentElement).Release())
                .Perform();
            return this;
        }

        public TouchGesturesPage PressAndHold(long ms)
        {
            GetTouchAction()
                .Press(_parentElement)
                .Wait(ms)
                .Release()
                .Perform();
            return this;
        }

        public TouchGesturesPage FlickUp(int perc)
        {
            var screenSize = _driver.Manage().Window.Size;
            int endY = screenSize.Height - (int)(screenSize.Height * ((double)perc / 100));
            int x = 100;

            return Flick(x, screenSize.Height, x, endY, 500);
        }

        public TouchGesturesPage FlickDown()
        {
            return Flick(100, 100, 100, 1000, 500);
        }

        public TouchGesturesPage Flick(double startX, double startY, double endX, double endY, long flickMs)
        {
            var screenSize = _driver.Manage().Window.Size;

            GetTouchAction()
                .Press(startX, startY)
                .Wait(flickMs)
                .MoveTo(endX, endY)
                .Release()
                .Perform();
            return this;
        }

        public TouchGesturesPage FlickParentElement()
        {
            var size = _parentElement.Size;

            GetTouchAction()
                .Press(_parentElement, size.Width * 0.25, size.Height * 0.25)
                .Wait(500)
                .MoveTo(size.Width * 0.75, size.Height * 0.75)
                .Release()
                .Perform();
            return this;
        }

        public TouchGesturesPage Scroll()
        {
            return this;
        }

        public TouchGesturesPage Pinch()
        {
            return this;
        }

        public TouchGesturesPage Zoom()
        {
            return this;
        }

        public override bool IsLoaded()
        {
            return IsVisible(_parentElement, _lastGesture, _gestureType, _scaleFactor);
        }

        public void ClickCanvas()
        {
            Click(_canvas);
        }

        public TouchGesturesPage AssertIsLoaded()
        {
            Assert.IsTrue(IsLoaded(), "TouchGesturesPage is not loaded");
            return this;
        }

    }
}
