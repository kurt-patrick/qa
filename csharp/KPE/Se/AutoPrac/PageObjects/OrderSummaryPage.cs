using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class OrderSummaryPage : OrderProgressBase
    {
        By _pageHeadingBy = By.Id("cart_title");
        By _orderProgressContainerBy = By.Id("order_step");
        By _orderDetailContentBy = By.Id("order-detail-content");

        public OrderSummaryPage(IWebDriver driver) 
            : base(driver, eCurrentStep.Summary)
        {
        }

        protected override List<By> IsLoadedElements()
        {
            return new List<By> { _pageHeadingBy, _orderDetailContentBy, _orderProgressContainerBy };
        }

        protected override By ProceedToCheckoutBy()
        {
            return By.XPath("//p[contains(@class, 'cart_navigation')]/a[@title='Proceed to checkout']");
        }
    }
}
