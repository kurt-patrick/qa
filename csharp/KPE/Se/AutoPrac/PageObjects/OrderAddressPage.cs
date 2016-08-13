using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class OrderAddressPage : OrderProgressBase
    {
        By _addNewAddressBy = By.XPath("//p[contains(@class, 'address_add')]/a");
        By _addressesAreEqualsBy = By.XPath("//p[contains(@class, 'addressesAreEquals')]");// By.Id("addressesAreEquals");
        By _billingAddressBy = By.Id("address_invoice");
        By _deliveryAddressBy = By.Id("address_delivery");
        By _orderMessageBy = By.XPath("//div[@id='ordermsg']/textarea");

        public OrderAddressPage(IWebDriver driver)
            : base(driver, eCurrentStep.Address)
        {
        }

        protected override List<By> IsLoadedElements()
        {
            return new List<By> { _addNewAddressBy, _addressesAreEqualsBy, _billingAddressBy, _deliveryAddressBy, _orderMessageBy };
        }

        protected override By ProceedToCheckoutBy()
        {
            return By.Name("processAddress");
        }

        public override By ContinueShoppingBy()
        {
            return By.XPath("//p[contains(@class, 'cart_navigation')]/a[@title='Previous']");
        }

    }
}
