using KPE.Se.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class HomePage : KPE.Se.Common.PageBase
    {
        #region enums
        public enum eProductCategory
        {
            Popular = 0,
            BestSellers
        }
        #endregion

        #region locators
        By _headerBy = By.ClassName("header-container");
        By _bodyBy = By.ClassName("columns-container");
        By _footerBy = By.ClassName("footer-container");
        By _signInBy = By.ClassName("login");
        By _signOutATag = By.ClassName("logout");
        #endregion

        #region constructors
        public HomePage(IWebDriver driver) : base(driver)
        {
            _baseUrl = "http://automationpractice.com/index.php";
        }
        #endregion

        #region methods
        public override bool IsLoaded()
        {
            return AreElementsVisible(new List<By> { _headerBy, _bodyBy, _footerBy });
        }

        public LoginPage ClickSignIn()
        {
            PerformClick(_signInBy);
            return new LoginPage(_driver);
        }

        public bool ClickSignOut()
        {
            //Click(_signOutATag);

            //Func<bool> condition = () => {
            //    return ElementIsStaleOrHidden(_signOutATag) && ElementIsVisible(_signInBy);
            //};

            //return TryWaitForCondition(condition);

            return TryClickAndValidate(_signOutATag, () => ElementIsVisible(_signInBy));
        }

        public bool IsSignedIn()
        {
            return ElementExists(_signOutATag);
        }

        public bool ClickTab(eProductCategory value)
        {
            if(value == GetSelectedTab())
            {
                return true;
            }

            // Click the required Tab and validate
            var by = By.XPath(string.Format("//ul[@id='home-page-tabs']/li/a[@class='{0}']", GetTabClassName(value)));
            return TryClickAndValidate(by, () => value == GetSelectedTab());
        }

        public List<HomePageProduct> GetPopularProducts(bool clickTab)
        {
            return GetProducts(eProductCategory.Popular, clickTab);
        }

        public List<HomePageProduct> GetBestSellerProducts(bool clickTab)
        {
            return GetProducts(eProductCategory.BestSellers, clickTab);
        }

        private string GetTabClassName(eProductCategory value)
        {
            string retVal = "homefeatured";
            if (value == eProductCategory.BestSellers)
            {
                retVal = "blockbestsellers";
            }
            return retVal;
        }

        public eProductCategory GetSelectedTab()
        {
            var retVal = eProductCategory.Popular;
            var element = _driver.FindElement(By.XPath("//ul[@id='home-page-tabs']/li[@class='active']/a"));
            if (element.GetAttribute("class").Equals("blockbestsellers"))
            {
                retVal = eProductCategory.BestSellers;
            }
            return retVal;
        }

        private List<HomePageProduct> GetProducts(eProductCategory value, bool clickTab)
        {
            if (clickTab)
            {
                ClickTab(eProductCategory.Popular);
            }

            if (GetSelectedTab() != value)
            {
                throw new Common.Exceptions.InvalidStateException(string.Format("The tab {0} must be clicked before calling GetProducts()", value.ToString()));
            }

            string xPath = string.Format("//div[@class='tab-content']/ul[@id='{0}']/li", GetTabClassName(value));
            return
                _driver.FindElements(By.XPath(xPath))
                .Select((ele, index) => new HomePageProduct(_driver, string.Format("{0}[{1}]", xPath, index + 1)))
                .ToList();

        }

        public HomePageCart ShoppingCart()
        {
            return new HomePageCart(_driver);
        }
        #endregion

    }
}
