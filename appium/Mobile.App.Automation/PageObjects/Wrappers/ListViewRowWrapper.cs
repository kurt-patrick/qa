using System;
using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.PageObjects.Wrappers
{
    public class ListViewRowWrapper : PageBase
    {
        private string _xPathBase;
        private IWebElement _element;
        private List<string> _text = new List<string>();

        public ListViewRowWrapper(TestCaseSettings settings, IWebElement element, string xPath) : base(settings)
        {
            _element = element;
            _xPathBase = xPath;

            // Get ALL of the child elements linked to this element
            var children = _driver.FindElementsByXPath(_xPathBase + "//*");

            // For each child element - grab its text and save it
            foreach(IWebElement child in children)
            {
                AddText(child.Text);
                AddText(GetAttribute(child, "contentDescription"));
            }

        }

        public void TapRow()
        {
            Click(_element);
        }

        public bool HasText(string item)
        {
            return _text.Contains(item);
        }

        public bool HasText(List<string> items)
        {
            return _text.Intersect(items).Count() == items.Count;
        }

        public bool Contains(string text)
        {
            return _text.FirstOrDefault(s => s.Contains(text)) != null;
        }

        public int IndexOf(string item)
        {
            return _text.FindIndex(s => s.Equals(item));
        }

        private void AddText(string text)
        {
            if(!string.IsNullOrWhiteSpace(text) && !_text.Contains(text))
            {
                _text.Add(text);
            }
        }

        private string GetAttribute(IWebElement element, string attributeName)
        {
            string retVal = null;
            try
            {
                retVal = element.GetAttribute(attributeName);
            }
            catch (Exception)
            {
            }
            return retVal;
        }

        public List<string> GetText()
        {
            return _text;
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

    }
}