using KPE.Se.Common;
using KPE.Se.Common.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class HomePageCart : Common.PageBase
    {
        private const string _basePath = "//div[@class='shopping_cart']";
        private By _cartBTag = By.XPath(_basePath + "/a/b");
        private By _cartQtySpanTag = By.XPath(_basePath + "//span[@class='ajax_cart_quantity']");
        private By _cartCheckoutATag = By.XPath(_basePath + "//p[@class='cart-buttons']/a[@id='button_order_cart']");

        public HomePageCart(IWebDriver driver) : base(driver)
        {
        }

        public int GetCartQty()
        {
            var rv = 0;
            if(ElementIsVisible(_cartQtySpanTag))
            {
                var element = FindElement(_cartQtySpanTag, Periods.Two);
                Int32.TryParse(element.Text, out rv);
            }

            // NOTE: IE doesnt appear to have the style tag - instead TryParse the value
            //var style = element.GetAttribute("style") ?? string.Empty;
            //LogToConsole("style: " + style);
            //if(style.Contains("inline"))
            //{
            //    // if style contains "display: inline;" then the cart qty is at least 1
            //    // note: not using try catch as i want this to blow up if it doesnt contain an integer
            //    LogToConsole("element.Text: " + element.Text??string.Empty);
            //    rv = int.Parse(element.Text);
            //}


            return rv;
        }


        internal bool ClearCart()
        {
            LogToConsole("ClearCart()");
            if(GetCartQty() <= 0)
            {
                LogToConsole("No items exist - success");
                return true;
            }

            if (!MouseOver())
            {
                LogToConsole("MouseOver() has failed");
                return false;
            }

            // get the list of products in the cart
            int actualRowCnt = 0;
            bool flgSuccess = true;
            var cartItems = GetCartItems();
            int expRowCnt = cartItems.Count - 1;

            // Working from bottom to top - click (x) on each item
            //LogToConsole("cartItems.Count: " + cartItems.Count.ToString());
            for(int i = cartItems.Count - 1; i >= 0; i--)
            {
                //LogToConsole("cartItems[i].CickRemove();");
                cartItems[i].CickRemove();

                // Wait for the row count to decrease
                flgSuccess = WaitHelper.TryWaitForCondition(
                    () => { 
                        actualRowCnt = GetCartItemCount();
                        return actualRowCnt == expRowCnt;
                    });

                LogToConsole(string.Format("Actual row count ({0}) Expected row count ({1})", actualRowCnt, expRowCnt));
                expRowCnt -= 1;

            }

            // success := GetCartQty() == 0
            return flgSuccess;

        }

        /// <summary>
        /// Mouse over the shopping cart
        /// This will cause the dropdown to appear with cart items, totals, and buttons
        /// </summary>
        /// <returns>True if the cart buttons are visible</returns>
        private bool MouseOver()
        {
            LogToConsole("MouseOver()");
            LogToConsole("MouseOverElement(_cartQtySpanTag);");
            MoveToElement(_cartBTag);
            LogToConsole("return ElementIsVisible(_cartCheckoutATag, TimeSpans.Sec5);");
            return ElementIsVisible(_cartCheckoutATag, Periods.Five);
        }

        internal List<HomePageCartItem> GetCartItems()
        {
            var basePath = "//div[@class='cart_block_list']//dt";
            return 
                _driver.FindElements(By.XPath(basePath))
                .Select((ele, index) => new HomePageCartItem(_driver, basePath + string.Format("[{0}]", index + 1)))
                .ToList();
        }

        /// <summary>
        /// Returns the number of unique items in the cart (e.g. 3 items)
        /// Not the total qty of products e.g. 3 unique products of 2 each being qty 6
        /// </summary>
        /// <returns></returns>
        internal int GetCartItemCount()
        {
            var basePath = "//div[@class='cart_block_list']//dt";
            return _driver.FindElements(By.XPath(basePath)).Count;
        }

        public override bool IsLoaded()
        {
            return ElementIsVisible(By.XPath(_basePath), Periods.Five);
        }

    }
}
