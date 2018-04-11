using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using System;

namespace KPE.Mobile.App.Automation.PageObjects.Wrappers
{
    public class MobileElementWrapper : IWebElementWrapper
    {
        readonly IWebElementWrapper _element;
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
        public void PressKeys(string text) => _element.PressKeys(text);
        public void PressKeys(string text, bool clearText) => _element.PressKeys(text, clearText);
        public bool IsChecked() => _element.IsChecked();
        public string Text() => _element.Text();
        public string Text(bool trim) => _element.Text(trim);
        public bool ToggleState(bool toggleOn) => _element.ToggleState(toggleOn);
        public override string ToString()
        {
            var retVal = Text();
            return retVal;
        }

    }

    class AndroidElementWrapper : ElementWrapper
    {
        public AndroidElementWrapper(AppiumDriver<IWebElement> driver, By locator) : base(driver, locator)
        {
        }
    }

    class IOSElementWrapper : ElementWrapper
    {
        public IOSElementWrapper(AppiumDriver<IWebElement> driver, By locator) : base(driver, locator)
        {
        }
    }

    class ElementWrapper : PageBase, IWebElementWrapper
    {
        readonly By _locator;

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
            base.SendKeys(element, text, clearText);
        }

        public virtual string Text() => Text(false);
        public string Text(bool trim)
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
            return GetText(element, trim);
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
