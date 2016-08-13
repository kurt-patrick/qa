using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.HerokuApp.PageObjects
{
    internal class BasicAuthPage : Common.PageBase
    {
        By _bodyTag = By.TagName("body");
        By _successPTag = By.CssSelector(".example p");

        public BasicAuthPage(IWebDriver driver) 
            : base(driver) { }

        public override bool IsLoaded()
        {
            var locators = new List<By> { _bodyTag };
            return AreElementsVisible(locators);
        }

        internal string GetErrorMessage()
        {
            return GetText(_bodyTag);
        }

        internal string GetSuccessMessage()
        {
            return GetText(_successPTag);
        }

    }
}
