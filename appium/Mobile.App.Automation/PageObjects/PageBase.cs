using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using KPE.Mobile.App.Automation.QA;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace KPE.Mobile.App.Automation.PageObjects
{
    public abstract class PageBase
    {
        protected readonly AppiumDriver<IWebElement> _driver = null;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PageBase(AppiumDriver<IWebElement> driver)
        {
            ObjectQA.ThrowIfNull(driver);

            _driver = driver;

            /*
            TimeOutDuration implicitWait = new TimeOutDuration(TimeSpan.FromMilliseconds(Settings.Instance().ImplicitWait));
            PageFactory.InitElements(_driver, this, new AppiumPageObjectMemberDecorator(implicitWait));
            */
        }

        protected TouchAction TouchAction()
        {
            return new TouchAction(_driver);
        }

        protected MultiAction MultiAction()
        {
            return new MultiAction(_driver);
        }

        public abstract bool IsLoaded();

        public T SwitchPageObject<T>() where T : PageBase
        {
            return PageObjectFactory.Create<T>(_driver);
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

        /// <summary>
        /// Looks for the element specified using the wait specified
        /// The element must be visible for success
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        /// <returns>true if visible</returns>
        protected bool IsVisible(By by) => WaitUntil(ExpectedConditions.ElementIsVisible(by), false) != null;
        protected bool IsNotVisible(By by) => WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(by), false);

        protected bool IsDisplayed(params IWebElementWrapper[] list)
        {
            ObjectQA.ThrowIfIEnumerableIsEmpty(list);
            return list.All(ele => ele.Displayed());
        }

        /// <summary>
        /// Waits for the element to exist and once visible it is clicked
        /// </summary>
        /// <param name="by"></param>
        /// <param name="timeOut"></param>
        protected void Click(By by)
        {
            var ele = WaitUntil(ExpectedConditions.ElementToBeClickable(by), true);
            ele.Click();
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