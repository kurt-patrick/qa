using KPE.Se.AutoPrac.PageObjects;
using KPE.Se.Common;
using KPE.Se.Common.Helpers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.Tests.Integration
{
    [Parallelizable(ParallelScope.None)]
    public class IntegrationTests : Common.TestFixtureBase
    {
        HomePage _homePage = null;
        MyAccountPage _myAccountPage = null;
        OrderSignInPage _orderSignInPage = null;
        OrderAddressPage _orderAddressPage = null;
        OrderShippingPage _orderShippingPage = null;
        OrderPaymentPage _orderPaymentPage = null;

        public IntegrationTests(TestFixtureConfig config) : base(config) { }

        public override void TestSetup()
        {
            _homePage = new HomePage(_driver);
            _myAccountPage = new MyAccountPage(_driver);
            _orderSignInPage = new OrderSignInPage(_driver);
            _orderAddressPage = new OrderAddressPage(_driver);
            _orderShippingPage = new OrderShippingPage(_driver);
            _orderPaymentPage = new OrderPaymentPage(_driver);
        }

        [Test(Description="CompleteFlowTest_FromHomePageToPayment")]
        public void CompleteFlowTest_AddToCartThruToPayment_StartFromHomePage_LoginAfterAddToCart()
        {
            // HomePage - Load page and validate
            // -------------------------------------------------------------------------
            _homePage.NavigateTo();
            Assert.IsTrue(_homePage.IsLoaded(), "Homepage is not loaded");
            if(_homePage.IsSignedIn())
            {
                Assert.IsTrue(_homePage.ClickSignOut(), "Clicking sign out failed ");
                Assert.IsTrue(_homePage.IsLoaded(), "Homepage is not loaded");
            }

            var shoppingCart = _homePage.ShoppingCart();
            Assert.IsTrue(shoppingCart.ClearCart(), "Clearing the cart has failed");

            // HomePage
            // -------------------------------------------------------------------------
            var productList = _homePage.GetPopularProducts(true);
            Assert.IsTrue(productList.Count >= 1, "No (Popular Products) exist");

            // Pick a random product
            var product = RandomHelper.GetRandom(productList);
            var productInfo = product.GetProductDetails();
            Assert.IsTrue(product.MouseOver(), "MouseOver() product has failed");
            var addedToCartModal = product.ClickAddToCart();

            // AddToCartModal - Click Proceed to Checkout
            // -------------------------------------------------------------------------
            //LogToConsole("Added To Cart Modal");
            Assert.IsTrue(addedToCartModal.IsLoaded(), "Clicking Add to cart failed to load the modal");
            Assert.AreEqual(addedToCartModal.GetAddedProductName(), productInfo.Name, "Product name differs between the site and the modal");
            // todo: this needs to take into account qty's
            Assert.AreEqual(addedToCartModal.GetAddedProductTotal(), productInfo.Price, "Product price differs between the site and the modal");
            Assert.AreEqual(addedToCartModal.GetOrderTotalExShip(), productInfo.Price, "Order total (Ex Ship) is not as expected");

            var orderSummaryPage = addedToCartModal.ClickProceedToCheckout();

            // SummaryPage - Validate we are in step "Summary" - and - Login
            // -------------------------------------------------------------------------
            //LogToConsole("Order Summary Page");
            Assert.IsTrue(orderSummaryPage.IsLoaded(), "Order summary page failed to load");
            // todo: validate the order contains the item we just added
            orderSummaryPage.ClickProceedToCheckout();

            // SignInPage - Perform login
            // -------------------------------------------------------------------------
            //LogToConsole("SignIn Page");
            Assert.IsTrue(_orderSignInPage.IsLoaded(), "Login page failed to load");
            _orderSignInPage.Login(LoginPage.MyLoginEmail, LoginPage.MyLoginPassword);

            // Address page
            // -------------------------------------------------------------------------
            //LogToConsole("Address Page");
            Assert.IsTrue(_orderAddressPage.IsLoaded(), "Address page failed to load");
            _orderAddressPage.ClickProceedToCheckout();

            // Shipping Page
            // -------------------------------------------------------------------------
            //LogToConsole("Shipping Page");
            Assert.IsTrue(_orderShippingPage.IsLoaded(), "Shipping page failed to load");
            Assert.IsTrue(_orderShippingPage.AgreeToTerms(true), "Failed to Agree to terms");
            _orderShippingPage.ClickProceedToCheckout();

            // Payment Page
            // -------------------------------------------------------------------------
            //LogToConsole("Payment Page");
            Assert.IsTrue(_orderPaymentPage.IsLoaded(), "Payment page failed to load");
            // todo: Validate products added
            // todo: Validate totals
            Assert.IsTrue(_orderPaymentPage.ClickPaymentMethod(OrderPaymentPage.ePayBy.BankWire), "Clicking pay by bank wire failed");

            // todo: validate the Payment information is displayed
            // Click "I confirm my order" button
            _orderPaymentPage.ClickConfirmMyOrder();
            // todo: validate the text "Your order on My Store is complete."

        }

        //[Test]
        //public void CompleteFlowTest_AddToCartThruToPayment_StartFromHomePage_LoginBeforeAddToCart()
        //{

        //}

        public override void TearDown()
        {
            _homePage = null;
            _myAccountPage = null;
            _orderSignInPage = null;
            _orderAddressPage = null;
            _orderShippingPage = null;
            _orderPaymentPage = null;
        }

    }
}
