using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    /// <summary>
    /// Progress indicator control
    /// Orders can be in one of 5 states
    /// Summary, Sign in, Address, Shipping, Payment
    /// </summary>
    public abstract class OrderProgressBase : Common.PageBase
    {
        public enum eCurrentStep
        {
            Summary,
            SignIn,
            Address,
            Shipping,
            Payment
        }

        #region locators
        private const string XPathBase = "//ul[@id='order_step']";
        By _baseElementBy = By.XPath(XPathBase);
        By _currentStepBy = By.XPath(XPathBase + "/li[contains(@class, 'step_current')]/span");
        //By _proceedToCheckoutBy = By.XPath("//div[@class='button-container']/a[@title='Proceed to checkout']");
        #endregion

        protected eCurrentStep ExpectedCurrentStep { get; set; }
        public OrderProgressBase(IWebDriver driver, eCurrentStep step) : base(driver)
        {
            ExpectedCurrentStep = step;
        }

        protected abstract List<By> IsLoadedElements();
        protected abstract By ProceedToCheckoutBy();
        public virtual By ContinueShoppingBy()
        {
            return By.XPath("//a[@title='Continue shopping']");
        }

        public eCurrentStep GetCurrentStep()
        {
            string step = GetText(_currentStepBy).Replace(" ", "").Split('.').Last();

            // note: not using TryParse here as i want the code to throw an excecption on fail
            var retVal = eCurrentStep.Address;
            if (!Enum.TryParse<eCurrentStep>(step, true, out retVal))
            {
                var errMsg = "Failed to parse enum (eCurrentStep) from the text: " + step ?? "null";
                LogToConsole(errMsg);
                throw new ArgumentOutOfRangeException("step", errMsg);
            }
            return retVal;
        }

        public void ClickProceedToCheckout()
        {
            var by = ProceedToCheckoutBy();
            if(by == null)
            {
                throw new NotSupportedException("Proceed to checkout is not support in page: " + ExpectedCurrentStep.ToString());
            }
            PerformClick(by);
        }

        public void ClickContinueShopping()
        {
            PerformClick(ContinueShoppingBy());
        }

        public override bool IsLoaded()
        {
            //LogToConsole("OrderProgressBase.IsLoaded()");

            // generic elements that should exist for all pages
            var elements = new List<By> { _baseElementBy };
            if(ExpectedCurrentStep != eCurrentStep.SignIn)
            {
                elements.Add(ContinueShoppingBy());
            }

            if (ProceedToCheckoutBy() != null)
            {
                elements.Add(ProceedToCheckoutBy());
            }

            //LogToConsole("elements.Count: " + elements.Count.ToString());

            // specific elements required by child pages
            var childElements = IsLoadedElements();
            if (childElements != null)
            {
                childElements = childElements.Where(by => by != null).ToList();
                //LogToConsole("childElements.Count: " + childElements.Count.ToString());
                if (childElements.Count > 0)
                {
                    elements.AddRange(childElements);
                }
            }

            //LogToConsole("elements.Count: " + elements.Count.ToString());

            // Success is based on all expected elements existing and the Current Step matching the expected step
            return AreElementsVisible(elements) && GetCurrentStep() == ExpectedCurrentStep;
        }

    }
}
