using KPE.Se.Common.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KPE.Se.Common
{
    public abstract class PageBase
    {
        protected string _baseUrl = null;
        protected readonly IWebDriver _driver = null;

        public PageBase(IWebDriver driver) 
            : this(driver, null)
        {
        }

        public PageBase(IWebDriver driver, string baseUrl)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(driver, "driver", "The web driver is null");
            this._driver = driver;
            this._baseUrl = baseUrl;
        }

        public abstract bool IsLoaded();

        protected void InitElementsWithRetrying(object page)
        {
            PageFactory.InitElements(page, new RetryingElementLocator(_driver, Common.TimeSpans.TimeOutDefault));
        }

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
            var element = FindVisibleElement(by, 0);
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
            QA.Utils.ObjectUtil.ThrowIfNull(element);
            return (trim) ? element.Text.Trim() : element.Text;
        }

        /// <summary>
        /// Returns the text of the element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>trimmed value of the element</returns>
        protected string GetTextIfElementExists(By by, int timeOut = Periods.TimeOutDefault, bool trim = true)
        {
            IWebElement element = null;
            if(ElementExists(by, out element, timeOut))
            {
                return GetText(element, trim);
            }
            return string.Empty;
        }

        /// <summary>
        /// Returns the text of the element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>trimmed value of the element</returns>
        protected string GetTextIfElementIsVisible(By by, int timeOut = Periods.TimeOutDefault, bool trim = true)
        {
            IWebElement element = null;
            if (ElementIsVisible(by, out element, timeOut))
            {
                return GetText(element, trim);
            }
            return string.Empty;
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
            if(!decimal.TryParse(text, System.Globalization.NumberStyles.Currency, CultureInfo.CurrentCulture, out retVal))
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
            var element = FindVisibleElement(by, 0);
            ClearText(element);
        }

        /// <summary>
        /// Clears the text of an elemnt e.g. an input tag
        /// </summary>
        /// <param name="element"></param>
        protected void ClearText(IWebElement element)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(element, "element", "The web element is null");
            element.Clear();
        }

        /// <summary>
        /// Simulates pressing the keyboard on an input
        /// </summary>
        /// <param name="by"></param>
        /// <param name="text"></param>
        protected void SendKeys(By by, string text)
        {
            SendKeys(by, text, false);
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
            var element = FindVisibleElement(by, 0);
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
            QA.Utils.ObjectUtil.ThrowIfNull(element, "element", "The web element is null");
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
        protected IWebElement FindElement(By by, int timeOut = Periods.TimeOutDefault)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(by, "by");
            return WaitUntil(ExpectedConditions.ElementExists(by), true, timeOut);
        }

        protected IWebElement FindVisibleElement(By by, int timeOut = Periods.TimeOutDefault)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(by, "by");
            return WaitUntil(ExpectedConditions.ElementIsVisible(by), true, timeOut);
        }

        private void ThrowIfNull(object obj, By by, string expectedCondition)
        {
            if (obj == null)
            {
                LogToConsole(string.Format("Fail: {0}: {1}", expectedCondition, by));
                throw new NoSuchElementException(by.ToString());
            }
        }

        private TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, bool throwEx, int timeOut = Periods.TimeOutDefault, IWebDriver driver = null)
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

        protected System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(By by, int timeOut = Periods.TimeOutDefault)
        {
            return WaitUntil(ExpectedConditions.PresenceOfAllElementsLocatedBy(by), true, timeOut);
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
            if(string.IsNullOrWhiteSpace(url) && string.IsNullOrWhiteSpace(_baseUrl))
            {
                throw new ArgumentException("A url must be provided and/or the BaseUrl needs to be set");
            }

            // Relative path has been provided however BaseUrl has not been set
            bool startsWithHttp = url != null && url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase);
            if (!startsWithHttp && string.IsNullOrWhiteSpace(_baseUrl))
            {
                throw new ArgumentException("The BaseUrl needs to be set");
            }

            string theUrl = startsWithHttp ? url : string.Concat(_baseUrl, url).Trim();

            _driver.Url = theUrl;
            _driver.Navigate();
        }

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element only needs to exist - it does not need to be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if the element exists</returns>
        protected bool ElementExists(By by, int timeOut = 10)
        {
            return WaitUntil(ExpectedConditions.ElementExists(by), false, timeOut) != null;
        }

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element only needs to exist - it does not need to be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if the element exists and an element reference</returns>
        protected bool ElementExists(By by, out IWebElement element, int timeOut = 10)
        {
            element = WaitUntil(ExpectedConditions.ElementExists(by), false, timeOut);
            return element != null;
        }

        protected bool ElementIsVisible(IWebElement element)
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
        protected bool ElementIsVisible(By by, int timeOut = 10)
        {
            return WaitUntil(ExpectedConditions.ElementIsVisible(by), false, timeOut) != null;
        }

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element must be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if visible and an element reference</returns>
        protected bool ElementIsVisible(By by, out IWebElement element, int timeOut = 10)
        {
            IWebElement ele = null;
            if (ElementExists(by, out ele, timeOut) && ele.Displayed)
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
        protected bool ElementIsStaleOrHidden(By by, int timeOut = 10)
        {
            return WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(by), false, timeOut);
        }

        /// <summary>
        /// Validates all the elements are visible
        /// </summary>
        /// <param name="list"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if all elements are visible</returns>
        protected bool AreElementsVisible(List<By> list, int timeOut = Periods.TimeOutDefault)
        {
            Func<By, bool> condition = (By by) =>
            {
                if (ElementIsVisible(by, timeOut))
                {
                    return true;
                }
                LogToConsole("Fail: Element is not visible: " + by.ToString());
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
        protected bool AreElementsVisible(params IWebElement[] list)
        {
            return (list != null) && (list.Count() >= 1) && (list.All(ele => ele != null && ele.Displayed));
        }

        /// <summary>
        /// Clicks on the element immediately
        /// </summary>
        /// <param name="element"></param>
        protected void PerformClick(IWebElement element)
        {
            element.Click();
        }

        /// <summary>
        /// Waits for the element to exist and once visible it is clicked
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        protected void PerformClick(By by, int timeOut = Periods.Five)
        {
            var ele = FindElement(by, timeOut);
            PerformClick(ele);
        }

        protected bool TryClickAndValidate(By by, Func<bool> condition, int timeOut = Periods.TimeOutDefault)
        {
            var element = FindVisibleElement(by, Periods.Five);
            return TryClickAndValidate(element, condition, timeOut);
        }

        protected bool TryClickAndValidate(IWebElement element, Func<bool> condition, int timeOut = Periods.TimeOutDefault)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(element);
            element.Click();
            return WaitHelper.TryWaitForCondition(condition, timeOut);
        }

        protected Actions MoveToElement(IWebElement element)
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

        protected Actions MoveToElement(By by)
        {
            try
            {
                var element = FindVisibleElement(by, Periods.Five);
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
            if (element.Selected == selected) {
                return true;
            }

            Func<bool> condition = () => {
                PerformClick(element);
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
            return WaitUntil(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(frameName), true, Periods.Five, driver);
        }


        protected static void LogToConsole(string text)
        {
            Console.WriteLine(string.Format("{0}: {1}", text, DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

        /// <summary>
        /// Uses JavaScript to determine if the image is broken or not
        /// http://stackoverflow.com/questions/16784534/find-broken-images-in-page-image-replace-by-another-image/
        /// http://elementalselenium.com/tips/67-broken-images
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if broken else false</returns>
        protected bool IsImageBroken(By by)
        {
            var element = FindElement(by);
            return JavaScriptHelper.IsImageBroken(_driver, element);
        }

        public SelectTagHelper SelectHelper(By by)
        {
            var ele = FindElement(by);
            return SelectHelper(ele);
        }

        public SelectTagHelper SelectHelper(IWebElement element)
        {
            return new SelectTagHelper(element);
        }

    }
}
