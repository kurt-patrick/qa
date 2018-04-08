using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Text() => _element.Text();

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
            PressKeys(text, clearText);
        }

        public virtual string Text()
        {
            var element = WaitUntil(ExpectedConditions.ElementIsVisible(_locator));
            return GetText(element);
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

    }

    interface IWebElementWrapper
    {
        void Click();
        bool Displayed();
        bool NotDisplayed();
        string Text();
        void PressKeys(string text);
        void PressKeys(string text, bool clearText);
    }
}
