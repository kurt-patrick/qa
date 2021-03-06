﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using au.com.kleenheat.se.helpers;
using au.com.kleenheat.se.qa;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Drawing;
using au.com.kleenheat.se.tests;
using au.com.kleenheat.se.common;

namespace au.com.kleenheat.se.pages
{
    public abstract class PageBase
    {
        protected readonly IWebDriver _driver = null;
        protected TestCaseSettings _testCaseSettings = null;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PageBase(TestCaseSettings settings)
        {
            ObjectQA.ThrowIfNull(settings);

            _driver = settings.GetWebDriver();
            _testCaseSettings = settings;
        }

        public abstract bool IsLoaded();

        //protected void InitElementsWithRetrying(object page)
        //{
        //    //PageFactory.InitElements(page, new RetryingElementLocator(_driver, TimeOutDefault));
        //}

        /// <summary>
        /// Returns the text of the element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>trimmed value of the element</returns>
        protected string GetText(By by)
        {
            return GetText(by, true);
        }

        /// <summary>
        /// Returns the text of the element
        /// </summary>
        /// <param name="by"></param>
        /// <param name="trim"></param>
        /// <returns>value of the element with option to be trimmed</returns>
        protected string GetText(By by, bool trim)
        {
            var element = FindVisibleElement(by);
            return GetText(element, trim);
        }

        /// <summary>
        /// Returns the text of the element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="trim"></param>
        /// <returns>value of the element with option to be trimmed</returns>
        protected string GetText(IWebElement element, bool trim)
        {
            ObjectQA.ThrowIfNull(element);
            return (trim) ? element.Text.Trim() : element.Text;
        }


        /// <summary>
        /// Returns the currency value of a string with currency char stripped out
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        protected decimal GetCurrency(By by)
        {
            decimal retVal = 0;
            string text = GetText(by, true);
            // strip the currecy symbol out $
            if (!decimal.TryParse(text, System.Globalization.NumberStyles.Currency, CultureInfo.CurrentCulture, out retVal))
            {
                throw new ArgumentException(string.Format("Failed to parse ({0}) to a decimal", text));
            }
            return retVal;
        }

        /// <summary>
        /// Clears the text of an elemnt e.g. an input tag
        /// </summary>
        /// <param name="by"></param>
        protected void ClearText(By by)
        {
            var element = FindVisibleElement(by);
            ClearText(element);
        }

        /// <summary>
        /// Clears the text of an elemnt e.g. an input tag
        /// </summary>
        /// <param name="element"></param>
        protected void ClearText(IWebElement element)
        {
            ObjectQA.ThrowIfNull(element, "element", "The web element is null");
            element.Clear();
        }

        /// <summary>
        /// Simulates pressing the keyboard on an input
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        protected void SendKeys(By by, string text)
        {
            SendKeys(by, text, true);
        }

        /// <summary>
        /// Simulates pressing the keyboard on an input
        /// If clear is true the inputs contents will be cleared
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        /// <param name="clear"></param>
        protected void SendKeys(By by, string text, bool clear)
        {
            var element = FindVisibleElement(by);
            SendKeys(element, text, clear);
        }

        /// <summary>
        /// Simulates pressing the keyboard on an input
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        protected void SendKeys(IWebElement element, string text)
        {
            SendKeys(element, text, false);
        }

        /// <summary>
        /// Simulates pressing the keyboard on an input
        /// If clear is true the inputs contents will be cleared
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <param name="clear"></param>
        protected void SendKeys(IWebElement element, string text, bool clear)
        {
            ObjectQA.ThrowIfNull(element, "element", "The web element is null");
            if (clear)
            {
                ClearText(element);
            }
            element.SendKeys(text);
        }

        /// <summary>
        /// Returns a web element based on the locator
        /// Waits for the the element to exist
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>A web element or throws an exception if not found</returns>
        protected IWebElement FindElement(By by)
        {
            ObjectQA.ThrowIfNull(by, "by");
            try
            {
                return WaitUntil(ExpectedConditions.ElementExists(by));
            }
            catch (Exception)
            {
                _log.Error("Failed to FindElement " + by.ToString());
                throw;
            }
        }

        protected IWebElement FindVisibleElement(By by)
        {
            ObjectQA.ThrowIfNull(by, "by");
            try
            {
                return WaitUntil(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception)
            {
                _log.Error("Failed to FindVisibleElement " + by.ToString());
                throw;
            }
        }


        private TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition)
        {
            return WaitUntil(condition, true, Constants.DefaultTimeOut, null);
        }

        private TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, bool throwEx)
        {
            return WaitUntil(condition, throwEx, Constants.DefaultTimeOut, null);
        }

        private TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, bool throwEx, int timeOut, IWebDriver driver)
        {
            try
            {
                var wait = new WebDriverWait(driver ?? _driver, TimeSpan.FromSeconds(timeOut));
                return wait.Until(condition);
            }
            catch
            {
                if (throwEx)
                {
                    throw;
                }
            }
            return default(TResult);
        }

        protected System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return WaitUntil(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        }

        /// <summary>
        /// Call this method if the page object has set the full url to the page in the constructor
        /// </summary>
        public virtual void NavigateTo()
        {
            NavigateTo(null);
        }

        /// <summary>
        /// Naviagate to the url provided
        /// </summary>
        /// <param name="url"></param>
        protected void NavigateTo(string url)
        {
            // Neither URL is set
            string baseUrl = _testCaseSettings.BaseUrl;
            if (string.IsNullOrWhiteSpace(url) && string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("A url must be provided and/or Settings.BaseUrl needs to be set");
            }

            // Relative path has been provided however BaseUrl has not been set
            bool startsWithHttp = url != null && url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase);
            if (!startsWithHttp && string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("Settings.BaseUrl needs to be set");
            }

            string theUrl = startsWithHttp ? url : string.Concat(baseUrl, url).Trim();

            try
            {
                _log.Debug("Url = " + theUrl);
                _driver.Url = theUrl;
                _driver.Navigate();
            }
            catch (Exception)
            {
                _log.Error("Failed to navigate to url: " + theUrl);
                throw;
            }

        }

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element only needs to exist - it does not need to be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if the element exists</returns>
        protected bool Exists(By by)
        {
            return WaitUntil(ExpectedConditions.ElementExists(by), false) != null;
        }

        /// <summary>
        /// Validates all the elements exist (doesn't check for displayed)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if all elements exist</returns>
        protected bool Exists(params By[] list)
        {
            Func<By, bool> condition = (By by) =>
            {
                if (Exists(by))
                {
                    return true;
                }
                _log.Debug("Element does not exist: " + by.ToString());
                return false;
            };

            return list.All(condition);
        }

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element only needs to exist - it does not need to be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if the element exists and an element reference</returns>
        protected bool Exists(By by, out IWebElement element)
        {
            element = WaitUntil(ExpectedConditions.ElementExists(by), false);
            return element != null;
        }

        protected bool IsVisible(IWebElement element)
        {
            return element != null && element.Displayed;
        }

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element must be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if visible</returns>
        protected bool IsVisible(By by)
        {
            return WaitUntil(ExpectedConditions.ElementIsVisible(by), false) != null;
        }

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element must be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if visible and an element reference</returns>
        protected bool IsVisible(By by, out IWebElement element)
        {
            IWebElement ele = null;
            if (Exists(by, out ele) && ele.Displayed)
            {
                element = ele;
                return true;
            }

            // fail
            element = null;
            return false;
        }

        /// <summary>
        /// Waits for the element to be hidden or stale
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        protected bool ElementIsStaleOrHidden(By by)
        {
            return WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(by), false);
        }

        /// <summary>
        /// Validates all the elements are visible
        /// </summary>
        /// <param name="list"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if all elements are visible</returns>
        protected bool IsVisible(params By[] list)
        {
            Func<By, bool> condition = (By by) =>
            {
                if (IsVisible(by))
                {
                    return true;
                }
                _log.Debug("Element is not visible or does not exist: " + by.ToString());
                return false;
            };

            return list.All(condition);
        }

        /// <summary>
        /// Validates all the elements are visible
        /// </summary>
        /// <param name="list"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if all elements are visible</returns>
        protected bool IsVisible(params IWebElement[] list)
        {
            ObjectQA.ThrowIfIEnumerableIsEmpty(list);
            return list.All(ele => ele != null && ele.Displayed);
        }

        /// <summary>
        /// Clicks on the element immediately
        /// </summary>
        /// <param name="element"></param>
        protected void Click(IWebElement element)
        {
            element.Click();
        }

        /// <summary>
        /// Waits for the element to exist and once visible it is clicked
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        protected void Click(By by)
        {
            var ele = FindElement(by);
            MoveToElement(ele);
            Click(ele);
        }

        protected bool TryClickAndValidate(By by, Func<bool> condition)
        {
            var element = FindVisibleElement(by);
            return TryClickAndValidate(element, condition, _testCaseSettings.DefaultTimeOut);
        }

        protected bool TryClickAndValidate(IWebElement element, Func<bool> condition, int timeOut)
        {
            ObjectQA.ThrowIfNull(element);
            element.Click();
            return WaitHelper.TryWaitForCondition(condition, timeOut);
        }

        protected Actions MoveToElement(IWebElement element)
        {
            if(_testCaseSettings.GetBrowser() != TestCaseSettings.eBrowser.FireFox)
            {
                Actions action = new Actions(_driver);
                try
                {
                    action.MoveToElement(element).Perform();
                    return action;
                }
                catch
                {
                    throw;
                }
            }
            return null;
        }

        protected Actions MoveToElement(By by)
        {
            try
            {
                var element = FindVisibleElement(by);
                return MoveToElement(element);
            }
            catch
            {
                throw;
            }
        }

        protected bool ToggleCheckBox(By by, bool selected)
        {
            var element = FindElement(by);
            WaitForMovingElementToBeStationary(element);
            MoveToElement(element);
            if (element.Selected == selected)
            {
                return true;
            }

            Func<bool> condition = () =>
            {
                Click(element);
                return element.Selected;
            };

            return WaitHelper.TryWaitForCondition(condition);
        }

        protected bool IsCheckBoxSelected(By by)
        {
            var element = FindElement(by);
            return element.Selected;
        }

        /// <summary>
        /// Tries to switch to the frame specified
        /// </summary>
        /// <param name="frameName"></param>
        /// <returns>true if the frame exists</returns>
        protected bool DoesFrameExist(string frameName, IWebDriver driver = null)
        {
            try
            {
                SwitchToFrame(frameName, driver);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to switch to the frame specified
        /// </summary>
        /// <param name="frameName"></param>
        /// <param name="driver">optional param, if null default driver will be used</param>
        /// <returns>true if the frame exists</returns>
        protected IWebDriver SwitchToFrame(string frameName, IWebDriver driver = null)
        {
            return WaitUntil(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameName), true, _testCaseSettings.DefaultTimeOut, driver);
        }

        protected SelectTagHelper SelectHelper(By by)
        {
            var ele = FindElement(by);
            return SelectHelper(ele);
        }

        protected SelectTagHelper SelectHelper(IWebElement element)
        {
            return new SelectTagHelper(element);
        }

        protected void WaitForMovingElementToBeStationary(IWebElement element)
        {
            bool rv = false;
            int sameCount = 0;
            Point lastLocation = element.Location;
            Point currentLocation = Point.Empty;
            Func<bool> condition = () =>
            {
                rv = false;
                currentLocation = element.Location;
                if (currentLocation == lastLocation)
                {
                    sameCount += 1;
                    rv = sameCount >= 2;
                }
                else
                {
                    lastLocation = currentLocation;
                }
                return rv;
            };
            WaitHelper.TryWaitForCondition(condition);
        }

    }
}