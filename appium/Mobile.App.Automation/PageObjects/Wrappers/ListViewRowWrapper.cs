using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.PageObjects.Wrappers
{
    public class ListViewRowWrapper : PageBase
    {
        private IWebElement _element;
        private bool _flgLoaded = false;
        private List<string> _textCache = new List<string>();
        private string _xPathBase;

        public ListViewRowWrapper(AppiumDriver<IWebElement> driver, IWebElement element, string xPath) : base(driver)
        {
            _element = element;
            _xPathBase = xPath;
        }

        public void TapRow()
        {
            _element.Click();
        }

        public T TapRow<T>() where T : PageBase
        {
            _element.Click();
            return PageObjectFactory.Create<T>(_driver);
        }

        public bool HasText(string item)
        {
            return TextCache().Contains(item);
        }

        public bool HasText(List<string> items)
        {
            return TextCache().Intersect(items).Count() == items.Count;
        }

        public bool Contains(string text)
        {
            return TextCache().FirstOrDefault(s => s.Contains(text)) != null;
        }

        internal bool ToggleCheckBox(bool check)
        {
            var element = _element.FindElement(By.ClassName("android.widget.CheckBox"));
            return ToggleCheckBox(element, check);
        }

        public int IndexOf(string item)
        {
            return TextCache().FindIndex(s => s.Equals(item));
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
            if(!_flgLoaded)
            {
                // Get ALL of the child elements linked to this element
                //var children = _driver.FindElementsByXPath(_xPathBase + "//*[(@text != '') or (@contentDescription != '')]").Where(ele => ele.Displayed);
                //var children = _driver.FindElementsByXPath(_xPathBase + "//*[(@text != '')]").Where(ele => ele.Displayed);
                var children = _driver.FindElementsByXPath(_xPathBase + "//*[(@text != '')]");

                // For each child element - grab its text and save it
                foreach (IWebElement child in children)
                {
                    AddTextToCache(child.Text);
                    //AddTextToCache(GetAttribute(child, "contentDescription"));
                }

                // Only load the child elements once
                _flgLoaded = true;
            }

            return _textCache;
        }

        public override bool IsLoaded()
        {
            return _flgLoaded;
        }

    }
}