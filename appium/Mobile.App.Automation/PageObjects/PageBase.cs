using KPE.Mobile.App.Automation.Common;
using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.QA;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Globalization;
using System.Linq;

namespace KPE.Mobile.App.Automation.PageObjects
{
    public abstract class PageBase
    {
        protected enum DriverType
        {
            NotSet,
            AndroidDriver,
            IOSDriver
        }

        protected readonly AppiumDriver<IWebElement> _driver = null;
        protected readonly DriverType _driverType = DriverType.NotSet;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PageBase(AppiumDriver<IWebElement> driver)
        {
            ObjectQA.ThrowIfNull(driver);

            _driver = driver;
            _driverType = GetDriverType(_driver);

            PageFactory.InitElements(_driver, this, new AppiumPageObjectMemberDecorator(Constants.DefaultTimeOutDuration));
        }

        protected TouchAction GetTouchAction()
        {
            return new TouchAction(_driver);
        }

        protected MultiAction GetMultiAction()
        {
            return new MultiAction(_driver);
        }

        /// <summary>
        /// Returns an instance of the AndroidDriver
        /// If conversion fails an execption will be thrown
        /// </summary>
        /// <returns></returns>
        public AndroidDriver<IWebElement> GetAndroidDriver()
        {
            AndroidDriver<IWebElement> retVal = _driver as AndroidDriver<IWebElement>;
            return retVal ?? 
                throw new InvalidCastException("Could not convert the current WebDriver into AndroidDriver<IWebElement>");
        }

        public abstract bool IsLoaded();

        private static DriverType GetDriverType(AppiumDriver<IWebElement> driver)
        {
            if(driver is AndroidDriver<IWebElement>)
            {
                return DriverType.AndroidDriver;
            }

            if (driver is IOSDriver<IWebElement>)
            {
                return DriverType.IOSDriver;
            }

            throw new NotImplementedException("Logic is not implemented for driver type" + driver.GetType().ToString());
        }

        /// <summary>
        /// Returns the text of the element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>trimmed value of the element</returns>
        protected string GetText(By by)
        {
            return GetText(by, false);
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
        protected string GetText(IWebElement element)
        {
            return GetText(element, false);
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

        public T SwitchPageObject<T>() where T : PageBase
        {
            return PageObjectFactory.Create<T>(_driver);
        }

        /// <summary>
        /// (Android) Hide keyboard techniques
        /// 1. never show keyboard
        /// https://discuss.appium.io/t/can-we-hide-android-soft-keyboard/6956/5
        /// 2. driver.navigate.back() 
        /// 3. AndroidDriver.HideKeyboard()
        /// https://github.com/appium/appium/issues/4452
        /// 4. Six different methods
        /// http://aksahu.blogspot.com.au/2015/10/hide-soft-keyboard-in-android.html
        /// </summary>
        public void HideKeyboard()
        {
            switch (_driverType)
            {
                case DriverType.AndroidDriver:
                    ((AndroidDriver<IWebElement>)_driver).HideKeyboard();
                    break;
                case DriverType.IOSDriver:
                    throw new NotImplementedException("todo");
                    //break;
                default:
                    throw new NotImplementedException(_driverType.ToString());
                    //break;
            }
        }

        public T HideKeyboard<T>() where T : PageBase
        {
            HideKeyboard();
            return SwitchPageObject<T>();
        }

        /// <summary>
        /// Returns the currency value of a string with currency char stripped out
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        protected decimal GetCurrency(By by)
        {
            string text = GetText(by, true);
            // strip the currecy symbol out $
            if (!decimal.TryParse(text, System.Globalization.NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal retVal))
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
            SendKeys(element, text, true);
            try 
            {
                HideKeyboard();
            }
            catch { }
        }

        /// <summary>
        /// Simulates pressing the keyboard on an input
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        protected void SetImmediateValue(IWebElement element, string text, bool clear, bool hideKeyboard)
        {
            AppiumWebElement appiumWebElement = element as AppiumWebElement;
            if(appiumWebElement == null)
            {
                if (element is IWrapsElement wrapsElement)
                {
                    appiumWebElement = wrapsElement.WrappedElement as AppiumWebElement;
                }
            }

            if (appiumWebElement != null)
            {
                if (clear) { appiumWebElement.Clear(); }

                appiumWebElement.SetImmediateValue(text);

                if(hideKeyboard) { TryHelper.TryCatch(() => HideKeyboard()); }
            }

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

        protected bool WaitForVisible(IWebElement element)
        {
            return TryHelper.TryWaitForCondition(() => element != null && element.Displayed, Common.Constants.DefaultTimeOut);
        }

        protected void WaitForTextToBePresentInElement(IWebElement element, string text)
        {
            ObjectQA.ThrowIfNull(text);
            WaitUntil(ExpectedConditions.TextToBePresentInElement(element, text));
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition)
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
            if (Exists(by, out IWebElement ele) && ele.Displayed)
            {
                element = ele;
                return true;
            }

            // fail
            element = null;
            return false;
        }

        protected bool TryWaitForStaleOrHidden(IWebElement element)
        {
            return TryHelper.TryWaitForCondition(() => element == null || element.Displayed == false);
        }

        /// <summary>
        /// Validates all the elements are visible
        /// </summary>
        /// <param name="list"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if all elements are visible</returns>
        protected bool IsVisible(params By[] list)
        {
            ObjectQA.ThrowIfIEnumerableIsEmpty(list);
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
            return list.All(ele => IsVisible(ele));
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
            Click(ele);
        }

        protected bool TryClickAndValidate(By by, Func<bool> condition)
        {
            var element = FindVisibleElement(by);
            return TryClickAndValidate(element, condition, Settings.Instance().DefaultTimeOut);
        }

        protected bool TryClickAndValidate(IWebElement element, Func<bool> condition, int timeOut)
        {
            ObjectQA.ThrowIfNull(element);
            element.Click();
            return TryHelper.TryWaitForCondition(condition, timeOut);
        }

        public bool IsChecked(IWebElement element)
        {
            ObjectQA.ThrowIfNull(element);
            return "true".Equals(element.GetAttribute("checked"));
        }

        protected bool ToggleCheckBox(IWebElement element, bool check)
        {
            if (check == IsChecked(element))
            {
                return true;
            }

            Func<bool> condition = () =>
            {
                element.Click();
                return IsChecked(element) == check;
            };

            return TryHelper.TryWaitForCondition(condition);
        }

    }
}