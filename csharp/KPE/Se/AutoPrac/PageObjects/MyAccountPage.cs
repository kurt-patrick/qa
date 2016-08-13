using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class MyAccountPage : Common.PageBase
    {
        #region locators
        By _signOutATagBy = By.XPath("//a[@class='logout']");
        By _orderHistoryATagBy = By.XPath("//a[@title='Orders']");
        By _creditSlipsATagBy = By.XPath("//a[@title='Credit slips']");
        By _myAddressesATagBy = By.XPath("//a[@title='Addresses']");
        By _myInfoATagBy = By.XPath("//a[@title='Information']");
        By _myWishlistATagBy = By.XPath("//a[@title='My wishlists']");
        #endregion

        #region constructors
        public MyAccountPage(IWebDriver driver) : base(driver)
        {
        }
        #endregion

        #region methods
        public override bool IsLoaded()
        {
            var list = new List<By> { _signOutATagBy, _orderHistoryATagBy, _creditSlipsATagBy, _myAddressesATagBy, _myInfoATagBy, _myWishlistATagBy };
            return AreElementsVisible(list);
        }
        #endregion

    }
}
