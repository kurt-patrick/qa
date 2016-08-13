using KPE.QA.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common
{
    public abstract class PageRowBase<T> : PageBase
    {
        #region fields
        private string _xPathBase = null;
        private Dictionary<T, string> _dictXPaths = new Dictionary<T, string>();
        #endregion

        #region constructors
        protected PageRowBase(IWebDriver driver, string xPathBase) : base(driver)
        {
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(xPathBase);
            _xPathBase = xPathBase;

            // Make sure the base path index is >= 1
            int startIndex = _xPathBase.IndexOf('[', _xPathBase.Length - 5);
            int index = int.Parse(_xPathBase.Substring(startIndex + 1).Replace("]", ""));
            if(index < 1)
            {
                LogToConsole("BasePath index must be at least 1: " + xPathBase);
            }

            SetXPaths();
        }
        #endregion

        #region methods
        protected abstract void SetXPaths();
        protected void SetXPath(T key, string xPath)
        {
            StringUtil.ThrowIfNullOrWhiteSpace(xPath);
            _dictXPaths[key] = xPath;
        }

        protected string GetElementsXPath(T key)
        {
            if(!_dictXPaths.ContainsKey(key))
            {
                throw new ArgumentException("No xpath exists for the key: " + key.ToString());
            }
            return _xPathBase + _dictXPaths[key];
        }

        protected By GetBaseBy()
        {
            return By.XPath(_xPathBase);
        }

        protected string GetBaseXPath()
        {
            return _xPathBase;
        }

        /// <summary>
        /// Remove the [n] from the end of the xpath
        /// </summary>
        /// <returns></returns>
        protected string GetGenericBaseXPath()
        {
            return _xPathBase.Substring(0, _xPathBase.Length - 3);
        }

        protected int GetGenericRowCount()
        {
            var by = By.XPath(GetGenericBaseXPath());
            return _driver.FindElements(by).Count;
        }

        protected By GetBy(T key)
        {
            string xPath = GetElementsXPath(key);
            return By.XPath(xPath);
        }

        protected string GetText(T key)
        {
            return GetText(GetBy(key));
        }

        protected decimal GetCurrency(T key)
        {
            return GetCurrency(GetBy(key));
        }

        protected void ClickElement(T key)
        {
            PerformClick(GetBy(key));
        }

        protected bool ElementIsVisible(T key, int timeOut = Periods.TimeOutDefault)
        {
            return ElementIsVisible(GetBy(key), timeOut);
        }

        protected bool ElementIsInvisible(T key, int timeOut = Periods.TimeOutDefault)
        {
            return ElementIsStaleOrHidden(GetBy(key), timeOut);
        }
        #endregion

    }
}
