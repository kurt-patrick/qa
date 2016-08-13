using KPE.Se.Common;
using KPE.Se.Common.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.HerokuApp.PageObjects
{
    public class DropDownPage : PageBase
    {
        #region fields
        SelectTagHelper _selectTagHelper = null;
        #endregion

        #region page factory properties
        [FindsBy(How = How.Id, Using = "dropdown")]
        private IWebElement _selectTag = null;
        #endregion

        #region constructors
        public DropDownPage(IWebDriver driver)
            : base(driver, "https://the-internet.herokuapp.com/dropdown")
        {
        }
        #endregion

        #region methods
        public override void NavigateTo()
        {
            base.NavigateTo();

            // initialise the web elements
            PageFactory.InitElements(this, new RetryingElementLocator(_driver, Common.TimeSpans.TimeOutDefault));

            // initalise the select helper with the select tag web element
            _selectTagHelper = new SelectTagHelper(_selectTag);

        }

        public override bool IsLoaded()
        {
            return ElementIsVisible(_selectTag);
        }

        internal void SelectByText(string text)
        {
            _selectTagHelper.SelectByText(text);
        }

        internal string SelectedText()
        {
            return _selectTagHelper.GetSelectedText();
        }

        internal void SelectByIndex(int index)
        {
            _selectTagHelper.SelectByIndex(index);
        }
        #endregion

    }
}
