using KPE.Se.Common.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class OrderPaymentPage : OrderProgressBase
    {
        // (Step 1) initial payment page elements 
        By _step1_pageHeadingH1Tag = By.ClassName("page-heading");
        By _step1_cartSummaryTableTag = By.Id("cart_summary");
        By _step1_paymentOptionsDivTag = By.Id("HOOK_PAYMENT");

        // (Step 2) Confirm my order elements
        By _step2_confirmMyOrderButtonTag = By.XPath("//*[@id='cart_navigation']/button");

        /// <summary>
        /// The descriptions are actually the classNames for the ATags
        /// </summary>
        public enum ePayBy
        {
            [Description("bankwire")]
            BankWire,
            [Description("cheque")]
            Check
        }

        public OrderPaymentPage(IWebDriver driver)
            : base(driver, eCurrentStep.Payment)
        {
        }

        protected override List<By> IsLoadedElements()
        {
            return new List<By> { _step1_pageHeadingH1Tag, _step1_cartSummaryTableTag, _step1_paymentOptionsDivTag };
        }

        public override By ContinueShoppingBy()
        {
            return By.XPath("//p[contains(@class, 'cart_navigation')]/a");
        }

        protected override By ProceedToCheckoutBy()
        {
            return null;
        }

        /// <summary>
        /// Clicks the selected payment method and waits for the Confirm Order button to appear
        /// </summary>
        /// <param name="payBy"></param>
        /// <returns></returns>
        public bool ClickPaymentMethod(ePayBy payBy)
        {
            var className = EnumHelper.GetDescription(payBy);
            var by = By.ClassName(className);
            return TryClickAndValidate(by, () => ElementExists(_step2_confirmMyOrderButtonTag, 2));
        }

        public void ClickConfirmMyOrder()
        {
            PerformClick(_step2_confirmMyOrderButtonTag);
        }

    }
}
