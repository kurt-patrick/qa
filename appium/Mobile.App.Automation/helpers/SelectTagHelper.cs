using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Helpers
{
    public class SelectTagHelper
    {
        private SelectElement _selectElement = null;

        public SelectTagHelper(IWebElement element)
        {
            _selectElement = new SelectElement(element);
        }

        public void SelectByText(string text)
        {
            _selectElement.SelectByText(text);
        }

        public string GetSelectedText()
        {
            var option = _selectElement.SelectedOption;
            if (option != null)
            {
                return option.Text ?? string.Empty;
            }
            return string.Empty;
        }

        public void SelectByIndex(int index)
        {
            _selectElement.SelectByIndex(index);
        }
    }
}