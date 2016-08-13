using KPE.Se.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class HomePageProduct : Common.PageRowBase<HomePageProduct.eElements>
    {
        #region enums
        public enum eElements
        {
            ProductName,
            ProductPrice,
            QuickView,
            MoreATag,
            AddToCartATag
        }
        #endregion

        #region constructors
        public HomePageProduct(IWebDriver driver, string xPathBase)
            : base(driver, xPathBase)
        {
        }
        #endregion

        #region methods
        protected override void SetXPaths()
        {
            SetXPath(eElements.ProductName, "//a[@class='product-name']");
            SetXPath(eElements.ProductPrice, "//div[@class='right-block']//span[contains(@class, 'product-price')]");
            SetXPath(eElements.QuickView, "//a[@class='quick-view']");
            SetXPath(eElements.MoreATag, "//a[contains(@class, 'lnk_view')]");
            SetXPath(eElements.AddToCartATag, "//a[contains(@class, 'ajax_add_to_cart_button')]");
        }

        public string GetProductName()
        {
            return GetText(eElements.ProductName);
        }

        public decimal GetProductPrice()
        {
            return GetCurrency(eElements.ProductPrice);
        }

        /// <summary>
        /// Mouse over the base UL - this will cause the Quick View element to appear
        /// If Quick View is visible - success
        /// </summary>
        /// <returns></returns>
        public bool MouseOver()
        {
            MoveToElement(GetBaseBy());
            // NOTE: In a standard environment / dev enviroment -- mousing over would show the "Quick View" span tag
            //       When run in sauce labs - "Quick view" is not shown, however all the products are appearing as if they has been moused over
            //       as such, Success is determined based on "Add to cart" && "More" being visible
            //return ElementIsVisible(eElements.QuickView, TimeSpans.Sec5);
            var elements = new List<By> { GetBy(eElements.AddToCartATag), GetBy(eElements.MoreATag) };
            return AreElementsVisible(elements);
        }

        public bool IsItemInStock()
        {
            throw new NotImplementedException();
            //return false;
        }

        public HomePageAddedToCartModal ClickAddToCart()
        {
            MouseOver();
            ClickElement(eElements.AddToCartATag);
            return new HomePageAddedToCartModal(_driver);
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        public ProductDetail GetProductDetails()
        {
            var retVal = new ProductDetail()
            {
                Name = GetProductName(),
                Price = GetProductPrice()
            };
            return retVal;
        }
        #endregion

        public class ProductDetail
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

    }
}
