using KPE.Se.Common.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.HerokuApp.PageObjects
{
    public class DragAndDropPage : Common.PageBase
    {
        By _divA = By.XPath("//div[@id='column-a']");
        By _divAHeader = By.XPath("//div[@id='column-a']/header");
        By _divB = By.XPath("//div[@id='column-b']");
        By _divBHeader = By.XPath("//div[@id='column-b']/header");

        public DragAndDropPage(IWebDriver driver)
            : base(driver, Constants.BaseUrl + "drag_and_drop")
        {
        }

        public override bool IsLoaded()
        {
            var locators = new List<By> { _divA, _divAHeader, _divB, _divBHeader };
            return AreElementsVisible(locators);
        }

        /// <summary>
        /// https://the-internet.herokuapp.com/drag_and_drop
        /// </summary>
        internal bool DragDivAOverDivB()
        {
            try
            {
                return JavaScriptHelper.DragDrop(_driver, "column-a", "column-b") == null;
            }
            catch (Exception ex) 
            {
                LogToConsole(ex.ToString());
            }
            return false;
        }

        internal string GetDivAText()
        {
            return GetText(_divAHeader);
        }

        internal string GetDivBText()
        {
            return GetText(_divBHeader);
        }
    }
}
