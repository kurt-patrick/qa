using KPE.Mobile.App.Automation.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;

namespace KPE.Mobile.App.Automation.PageObjects.Wrappers
{
    public class MobileElementWrapper : IWebElementWrapper
    {
        protected readonly IWebElementWrapper _element;
        public MobileElementWrapper(AppiumDriver<IWebElement> driver, By locator)
        {
            QA.ObjectQA.ThrowIfNull(driver, nameof(driver));

            if(driver is AndroidDriver<IWebElement>)
            {
                _element = new AndroidElementWrapper(driver, locator);
            }
            else if (driver is IOSDriver<IWebElement>)
            {
                _element = new IOSElementWrapper(driver, locator);
            }
            else
            {
                throw new NotSupportedException("driver not supported: " + driver.ToString());
            }

        }

        public void Click() => _element.Click();
        public bool Displayed() => _element.Displayed();
        public bool NotDisplayed() => _element.NotDisplayed();
        public void PressKeys(string text) => PressKeys(text, true);
        public void PressKeys(string text, bool clearText)
        {
            _element.PressKeys(text, clearText);
            try
            {
                HideKeyboard();
            }
            catch
            {
            }
        }

        public bool IsChecked() => _element.IsChecked();
        public string Text() => _element.Text();
        public string Text(bool trim) => _element.Text(trim);
        public bool ToggleState(bool toggleOn) => _element.ToggleState(toggleOn);
        public override string ToString()
        {
            var retVal = Text();
            return retVal;
        }

        public IWebElementWrapper NativeWrapper() => _element;
        AppiumDriver<IWebElement> AppiumDriver()
        {
            return ((IWebDriverReference<AppiumDriver<IWebElement>>)_element).WebDriver();
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
            AppiumDriver().HideKeyboard();
        }

    }

    public class MobileElementDropDownWrapper : MobileElementWrapper
    {
        public MobileElementDropDownWrapper(AppiumDriver<IWebElement> driver, By locator) : base(driver, locator)
        {
        }

        public void SelectByText(string text)
        {
            var driver = ((IWebDriverReference<AppiumDriver<IWebElement>>)_element).WebDriver();
            var helper = new DropDownHelper(driver);
            var element = ((IWebElementReference)_element).Element();
            helper.SelectByText(element, text);
        }
    }

    class AndroidElementWrapper : ElementWrapper, IWebElementReference, IWebDriverReference<AppiumDriver<IWebElement>>
    {
        public AndroidElementWrapper(AppiumDriver<IWebElement> driver, By locator) : base(driver, locator)
        {
        }

        public override void PressKeys(string text) => PressKeys(text, true);
        public override void PressKeys(string text, bool clearText)
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
            PressKeys(element, text, clearText);
        }

        public IWebElement Element()
        {
            return WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
        }

        public AppiumDriver<IWebElement> WebDriver()
        {
            return _driver;
        }
    }

    class IOSElementWrapper : ElementWrapper, IWebElementReference, IWebDriverReference<AppiumDriver<IWebElement>>
    {
        public IOSElementWrapper(AppiumDriver<IWebElement> driver, By locator) : base(driver, locator)
        {
        }

        public override void PressKeys(string text) => PressKeys(text, true);
        public override void PressKeys(string text, bool clearText)
        {
            // Find element and scroll into view
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
            base.PressKeys(element, text, clearText);
        }

        public IWebElement Element()
        {
            return WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
        }

        public AppiumDriver<IWebElement> WebDriver()
        {
            return _driver;
        }
    }

    class ElementWrapper : PageBase, IWebElementWrapper
    {
        protected readonly By _locator;

        public ElementWrapper(AppiumDriver<IWebElement> driver, By locator) : this(driver)
        {
            QA.ObjectQA.ThrowIfNull(locator, nameof(locator));
            _locator = locator;
        }

        private ElementWrapper(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public void Click()
        {
            var element = WaitUntil(ExpectedConditions.ElementToBeClickable(_locator));
            element.Click();
        }

        public bool Displayed() => WaitUntil(ExpectedConditions.ElementIsVisible(_locator), true) != null;
        public bool NotDisplayed() => WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(_locator), true);

        public virtual void PressKeys(string text) => PressKeys(text, true);
        public virtual void PressKeys(string text, bool clearText)
        {
            var element = WaitUntil(ExpectedConditions.ElementToBeClickable(_locator));
            PressKeys(element, text, clearText);
        }

        protected void PressKeys(IWebElement element, string text, bool clearText)
        {
            if (clearText)
            {
                element.Clear();
            }
            element.SendKeys(text);
        }

        public virtual string Text() => Text(false);
        public string Text(bool trim)
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
            return (trim) ? element.Text.Trim() : element.Text;
        }

        public bool IsChecked()
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
            return "true".Equals(element.GetAttribute("checked"));
        }

        public virtual bool ToggleState(bool toggleOn)
        {
            // already in desired state return success
            if(InDesiredState())
            {
                return true;
            }

            // Click to toggle desired state
            Click();

            // return the elements state after clicking
            return InDesiredState();

            bool InDesiredState() => toggleOn == IsChecked();
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public Size Size()
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(_locator), true);
            return element.Size;
        }

        public AppiumDriver<IWebElement> Driver()
        {
            return _driver;
        }
    }

    public interface IWebElementReference
    {
        IWebElement Element();
    }

    public interface IWebDriverReference<T> where T : IWebDriver
    {
        T WebDriver();
    }

    public interface IWebElementWrapper
    {
        void Click();
        bool Displayed();
        bool NotDisplayed();
        string Text();
        string Text(bool trim);
        void PressKeys(string text);
        void PressKeys(string text, bool clearText);

        /// <summary>
        /// todo: move checkbox implemtnation into its own object
        /// </summary>
        /// <returns></returns>
        bool IsChecked();
        bool ToggleState(bool toggleOn);
    }
}
