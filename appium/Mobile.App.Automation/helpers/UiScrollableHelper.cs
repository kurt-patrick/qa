using KPE.Mobile.App.Automation.QA;
using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    /// <summary>
    /// https://developer.android.com/reference/android/support/test/uiautomator/UiScrollable.html
    /// </summary>
    public class UiScrollableHelper
    {
        private string _container = null;

        public UiScrollableHelper() : this(null)
        {
        }

        public UiScrollableHelper(string container)
        {
            if(string.IsNullOrWhiteSpace(container))
            {
                // default to using the first scrollable object found
                // new UiSelector().scrollable(true).instance(0)
                container = UiSelectorHelper.ScrollableInstance(0);
            }
            _container = container;
        }

        public string ScrollIntoView(string selector)
        {
            return SelectorString("scrollIntoView", selector);
        }

        public string ScrollTextIntoView(string text)
        {
            return SelectorString("scrollTextIntoView", text);
        }

        /// <summary>
        /// Example output: new UiScrollable({0}).flingBackward()
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        private string SelectorString(string function)
        {
            StringQA.ThrowIfNullOrWhiteSpace(_container);
            StringQA.ThrowIfNullOrWhiteSpace(function);
            return string.Format("new UiScrollable({0}).({1})", _container, function);
        }

        /// <summary>
        /// example output: new UiScrollable({0}).scrollIntoView({1})
        /// </summary>
        /// <param name="function"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private string SelectorString(string function, string selector)
        {
            StringQA.ThrowIfNullOrWhiteSpace(_container);
            StringQA.ThrowIfNullOrWhiteSpace(function);
            StringQA.ThrowIfNullOrWhiteSpace(selector);
            return string.Format("new UiScrollable({0}).{1}({2})", _container, function, selector);
        }

    }
}
