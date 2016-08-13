using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class OrderShippingPage : OrderProgressBase
    {
        By _deliveryAddressBy = By.ClassName("delivery_options_address");
        By _agreeToTermsCheckBoxBy = By.Id("cgv");

        public OrderShippingPage(IWebDriver driver)
            : base(driver, eCurrentStep.Shipping)
        {
        }

        protected override List<By> IsLoadedElements()
        {
            return new List<By> { };
        }

        protected override By ProceedToCheckoutBy()
        {
            return By.Name("processCarrier");
        }

        public override By ContinueShoppingBy()
        {
            return By.XPath("//p[contains(@class, 'cart_navigation')]//a[@title='Previous']");
        }

        public bool AgreeToTerms(bool selected)
        {
            // http://stackoverflow.com/questions/14682763/how-to-select-checkboxes-using-selenium-java-webdriver
            // IE requires you to press spacebar - see above article
            // driver.findElement(By.id("idOfTheElement").SendKeys(Keys.Space);

            // NOTE: Using FindElement() rather than FindVisibleElement() 
            // because, in the resolution with IE, results in the element not visible on the page
            var element = FindElement(_agreeToTermsCheckBoxBy);
            if (element.Selected == selected)
            {
                return true;
            }

            // Click the checkbox and wait for its state to change
            return TryClickAndValidate(element, () => element.Selected == selected);

        }

    }
}
