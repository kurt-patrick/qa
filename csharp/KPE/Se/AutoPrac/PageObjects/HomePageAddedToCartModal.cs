using KPE.Se.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class HomePageAddedToCartModal : Common.PageBase
    {
        #region constants
        private const string XPathBase = "//div[@id='layer_cart']/div[@class='clearfix']";
        private const string XPathLayerCartProduct = XPathBase + "/div[contains(@class, 'layer_cart_product')]";
        private const string XPathLayerCartCart = XPathBase + "/div[contains(@class, 'layer_cart_cart')]";
        private const string XPathButtonContainer = "//div[@class='clearfix']//div[@class='button-container']";
        #endregion

        #region locators
        By _crossSpanTagBy = By.XPath(XPathLayerCartProduct + "/span[@class='cross']");
        By _proceedToCheckoutBy = By.XPath("//div[@class='clearfix']//a[@title='Proceed to checkout']");
        // LHS - Added product details
        By _productNameSpanTag = By.Id("layer_cart_product_title");
        By _productTotalSpanTag = By.Id("layer_cart_product_price");
        // RHS - Order totals
        By _orderTotalExShip = By.XPath(XPathLayerCartCart + "//span[@class='ajax_block_products_total']");
        By _orderTotalIncShip = By.XPath(XPathLayerCartCart + "//span[@class='ajax_block_cart_total']");
        By _orderShippingTotal = By.XPath(XPathLayerCartCart + "//span[@class='ajax_cart_shipping_cost']");

        #endregion

        #region constructors
        public HomePageAddedToCartModal(IWebDriver driver)
            : base(driver)
        {
        }
        #endregion

        #region methods
        public bool ClickClose()
        {
            return ClickElementAndValidateModalIsClosed(_crossSpanTagBy);
        }

        public bool ClickContinueShopping()
        {
            var xPath = XPathButtonContainer + "/span[contains(@class, 'continue')]";
            return ClickElementAndValidateModalIsClosed(By.XPath(xPath));
        }

        private bool ClickElementAndValidateModalIsClosed(By by)
        {
            PerformClick(by);
            return ElementIsStaleOrHidden(By.XPath(XPathBase), Periods.Five);
        }

        public OrderSummaryPage ClickProceedToCheckout()
        {
            PerformClick(_proceedToCheckoutBy);
            return new OrderSummaryPage(_driver);
        }

        public override bool IsLoaded()
        {
            var elements = new List<By> { _proceedToCheckoutBy, _productNameSpanTag, _productTotalSpanTag };
            return AreElementsVisible(elements);
        }

        internal bool IsClosed()
        {
            return ElementIsStaleOrHidden(By.XPath(XPathBase));
        }

        public string GetAddedProductName()
        {
            return GetText(_productNameSpanTag);
        }

        public decimal GetAddedProductTotal()
        {
            return GetCurrency(_productTotalSpanTag);
        }

        public decimal GetOrderTotalExShip()
        {
            return GetCurrency(_orderTotalExShip);
        }

        public decimal GetOrderTotalIncShip()
        {
            return GetCurrency(_orderTotalIncShip);
        }

        public decimal GetOrderShippingTotal()
        {
            return GetCurrency(_orderShippingTotal);
        }
        #endregion
    }
}
