using KPE.Se.AutoPrac.PageObjects;
using KPE.Se.Common;
using KPE.Se.Common.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.Tests.Functional
{
    [Parallelizable]
    [Category(Constants.Test_Category_HomePage)]
    public class HomePageTests : Common.TestFixtureBase
    {
        private HomePage _pageObj = null;

        public HomePageTests(TestFixtureConfig config) : base(config) { }

        public override void TestSetup()
        {
            LogToConsole("TestSetup");
            _pageObj = new HomePage(_driver);
        }

        [Test]
        public void HomePageLoads()
        {
            LogToConsole("HomePageLoads");
            LoadAndAssert();
        }

        [Test]
        public void ClickSignToLoadLoginPage()
        {
            LogToConsole("ClickSignToLoadLoginPage");
            LoadAndAssert();
            var loginPage = _pageObj.ClickSignIn();
            Assert.IsTrue(loginPage.IsLoaded(), "Clicking Sign In failed to load the login page", null);
        }

        [Test]
        public void ClearCart()
        {
            LoadAndAssert();

            // Make sure the shopping cart is loaded
            var shoppingCart = _pageObj.ShoppingCart();
            Assert.IsTrue(shoppingCart.IsLoaded(), "The (home page) shopping cart is not loaded");

            // Only add items so that a total of 3 exist in the cart
            int itemCount = shoppingCart.GetCartItemCount();
            if (itemCount < 3)
            {
                var productList = _pageObj.GetPopularProducts(true);
                Assert.IsTrue(productList.Count >= 1, "No (Popular Products) exist");

                // Pick 3 random products
                for (int i = 0; i < (3-itemCount); i++)
                {
                    var product = RandomHelper.GetRandom(productList);
                    var addedToCartModal = product.ClickAddToCart();
                    Assert.IsTrue(addedToCartModal.IsLoaded(), "Clicking Add to cart failed to load the modal");
                    addedToCartModal.ClickContinueShopping();
                    Assert.IsTrue(addedToCartModal.IsClosed(), "Clicking Continue Shopping failed to close the modal");
                }
            }

            // Remove all the items from the cart
            Assert.IsTrue(shoppingCart.ClearCart(), "Failed to ClearCart()");

        }

        private void LoadAndAssert()
        {
            _pageObj.NavigateTo();
            Assert.IsTrue(_pageObj.IsLoaded(), "Home page failed to load", null);
        }

        public override void TearDown()
        {
            _pageObj = null;
        }

    }
}
