using System;
using KPE.Mobile.App.Automation.Common;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.PageObjects.GmailApp
{
    public class ConversationListRowPage : PageBase
    {
        private string _xPathBase;
        private IWebElement _element;
        private List<string> _text = new List<string>();

        public ConversationListRowPage(TestCaseSettings settings, IWebElement element, string xPath) : base(settings)
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

        public bool HasText(string text)
        {
            return _text.Contains(text);
        }

        public bool HasText(List<string> text)
        {
            return text.Intersect(text).Count() == text.Count;
        }

        public bool Contains(string text)
        {
            return _text.FirstOrDefault(s => s.Contains(text)) != null;
        }

        public int IndexOf(string text)
        {
            return _text.FindIndex(s => s.Equals(text));
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

        public override bool IsLoaded()
        {
            throw new NotImplementedException();
        }

    }
}