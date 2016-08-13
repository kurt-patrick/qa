using KPE.Se.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    internal class HomePageCartItem : Common.PageRowBase<HomePageCartItem.eFields>
    {
        internal enum eFields
        {
            RemoveATag
        }

        public HomePageCartItem(IWebDriver driver, string basePath) 
            : base(driver, basePath)
        {
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

        protected override void SetXPaths()
        {
            SetXPath(eFields.RemoveATag, "//a[@class='ajax_cart_block_remove_link']");
        }

        public void CickRemove()
        {
            //// Pre count
            //int rowCountPre = GetGenericRowCount();

            // Click (x)
            LogToConsole("ClickElement(eFields.RemoveATag);");
            ClickElement(eFields.RemoveATag);

            //// Validate row count has decreased
            //int rowCountPost = 0;

            //Func<bool> condition = () => { 
            //    rowCountPost = GetGenericRowCount(); 
            //    return rowCountPost == rowCountPre - 1; 
            //};

            //return TryWaitForCondition(condition, TimeSpans.Sec5);

            //LogToConsole("return ElementIsInvisible(eFields.RemoveATag, TimeSpans.Sec5);");
            //return ElementIsInvisible(eFields.RemoveATag, TimeSpans.Sec5);

        }

        private string GetGenericBasePath()
        {
            // Remove the [n] from the end of the xpath
            return GetBaseXPath().Substring(0, GetBaseXPath().Length - 3);
        }

    }
}
