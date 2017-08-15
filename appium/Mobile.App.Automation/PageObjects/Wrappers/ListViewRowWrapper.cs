using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.PageObjects.Wrappers
{
    public class ListViewRowWrapper : PageBase
    {
        private string _xPathBase;
        private IWebElement _element;
        private List<string> _textCache = new List<string>();

        public ListViewRowWrapper(AppiumDriver<IWebElement> driver, IWebElement element, string xPath) : base(driver)
        {
            _element = element;
            _xPathBase = xPath;

            // Get ALL of the child elements linked to this element
            var children = _driver.FindElementsByXPath(_xPathBase + "//*");

            // For each child element - grab its text and save it
            foreach(IWebElement child in children)
            {
                AddTextToCache(child.Text);
                AddTextToCache(GetAttribute(child, "contentDescription"));
            }

        }

        public void TapRow()
        {
            Click(_element);
        }

        public bool HasText(string item)
        {
            return _textCache.Contains(item);
        }

        public bool HasText(List<string> items)
        {
            return _textCache.Intersect(items).Count() == items.Count;
        }

        public bool Contains(string text)
        {
            return _textCache.FirstOrDefault(s => s.Contains(text)) != null;
        }

        public int IndexOf(string item)
        {
            return _textCache.FindIndex(s => s.Equals(item));
        }

        private void AddTextToCache(string text)
        {
            if(!string.IsNullOrWhiteSpace(text) && !_textCache.Contains(text))
            {
                _textCache.Add(text);
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

        public List<string> TextCache()
        {
            return _textCache;
        }

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

    }
}