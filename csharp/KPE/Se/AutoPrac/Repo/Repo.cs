using KPE.Se.Common.Helpers;
using KPE.Se.Common.Repository;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.Repo
{
    public class HomePage
    {
        public class Header
        {
            public static By Locator { get { return By.ClassName("header-container"); } }
            public static By SignInATag { get { return By.ClassName("login"); } }
            public static By SignOutATag { get { return By.ClassName("logout"); } }
        }

        public class Body
        {
            public static By Locator { get { return By.ClassName("columns-container"); } }
            public static By ActiveTab { get { return By.XPath("//ul[@id='home-page-tabs']/li[@class='active']/a"); } }
            public static By HomePageTab(PageObjects.HomePage.eProductCategory category)
            {
                string className = TabItem.GetHomePageTabClassName(category);
                return By.XPath(string.Format("//ul[@id='home-page-tabs']/li/a[@class='{0}']", className));
            }

            public class TabItem
            {
                private const string Li_XPath = "//div[@class='tab-content']/ul[@id='{0}']/li";

                private string _className = null;
                public TabItem(PageObjects.HomePage.eProductCategory category)
                {
                    _className = GetHomePageTabClassName(category);
                }

                public static string GetHomePageTabClassName(PageObjects.HomePage.eProductCategory category)
                {
                    string retVal = "homefeatured";
                    if (category == PageObjects.HomePage.eProductCategory.BestSellers)
                    {
                        retVal = "blockbestsellers";
                    }
                    return retVal;
                }

                public By GenericLocator()
                {
                    return By.XPath(GenericPath());
                }

                public string GenericPath()
                {
                    return string.Format(Li_XPath, _className);
                }

                public By Specific(int index)
                {
                    return By.XPath(SpecificPath(index));
                }

                public string SpecificPath(int index)
                {
                    QA.Utils.Int32Util.ThrowIfNotAtLeast(1, index);
                    return string.Format(Li_XPath + "[{1}]", _className, index);
                }

            }

        }

        public class Footer
        {
            public static By Locator { get { return By.ClassName("footer-container"); } }
        }

        public static List<By> IsLoadedElements()
        {
            return new List<By> { Header.Locator, Body.Locator, Footer.Locator };
        }

    }

}
