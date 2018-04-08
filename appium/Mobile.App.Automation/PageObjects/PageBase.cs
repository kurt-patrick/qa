using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.QA;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
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

            TimeOutDuration implicitWait = new TimeOutDuration(TimeSpan.FromMilliseconds(Settings.Instance().ImplicitWait));
            PageFactory.InitElements(_driver, this, new AppiumPageObjectMemberDecorator(implicitWait));
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
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(by), true);
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
                default:
                    throw new NotImplementedException(_driverType.ToString());
            }
        }

        public T HideKeyboard<T>() where T : PageBase
        {
            HideKeyboard();
            return SwitchPageObject<T>();
        }

        /// <summary>
        /// Clears the text of an elemnt e.g. an input tag
        /// </summary>
        /// <param name="by"></param>
        protected void ClearText(By by)
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(by), true);
            ClearText(element);
        }

        /// <summary>
        /// Clears the text of an elemnt e.g. an input tag
        /// </summary>
        /// <param name="element"></param>
        protected void ClearText(IWebElement element)
        {
            ObjectQA.ThrowIfNull(element, nameof(element));
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
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(by), true);
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
        /// If clear is true the inputs contents will be cleared
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        /// <param name="clear"></param>
        protected void SendKeys(IWebElement element, string text, bool clear)
        {
            ObjectQA.ThrowIfNull(element, nameof(element));
            if (clear) { ClearText(element); }
            element.SendKeys(text);
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition)
        {
            return WaitUntil(condition, true, Settings.Instance().WebDriverWaitTimeOut, null);
        }

        protected TResult WaitUntil<TResult>(Func<IWebDriver, TResult> condition, bool throwEx)
        {
            return WaitUntil(condition, throwEx, Settings.Instance().WebDriverWaitTimeOut, null);
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
        protected bool IsVisible(By by) => WaitUntil(ExpectedConditions.ElementIsVisible(by), false) != null;
        protected bool IsNotVisible(By by) => WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(by), false);

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
            var ele = WaitUntil(ExpectedConditions.ElementToBeClickable(by), true);
            Click(ele);
        }

        protected bool TryClickAndValidate(By by, Func<bool> condition)
        {
            return TryClickAndValidate(by, condition, Settings.Instance().WebDriverWaitTimeOut);
        }

        protected bool TryClickAndValidate(By by, Func<bool> condition, int timeOut)
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(by), true);
            return TryClickAndValidate(element, condition, timeOut);
        }

        protected bool TryClickAndValidate(IWebElement element, Func<bool> condition, int timeOut)
        {
            ObjectQA.ThrowIfNull(element);
            element.Click();
            return WaitUntil((arg) => condition(), false, timeOut, _driver);
        }

        public bool IsChecked(IWebElement element)
        {
            ObjectQA.ThrowIfNull(element);
            return "true".Equals(element.GetAttribute("checked"));
        }

        protected bool ToggleCheckBox(IWebElement element, bool check)
        {
            // if the element is in the desired state return with success
            if (check == IsChecked(element))
            {
                return true;
            }

            // Wait until the element is clickable
            WaitUntil(ExpectedConditions.ElementToBeClickable(element), true);

            // Click the element and confirm that the state is as expected
            bool success =
                WaitUntil((arg) => {
                    element.Click();
                    return IsChecked(element) == check;
                }, false);

            return success;
        }

    }
}