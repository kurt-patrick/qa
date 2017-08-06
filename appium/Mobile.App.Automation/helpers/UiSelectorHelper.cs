using KPE.Mobile.App.Automation.QA;
using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    /// <summary>
    /// https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html
    /// </summary>
    public class UiSelectorHelper
    {
        private UiSelectorHelper()
        {
        }

        public static string ScrollableInstance(int index)
        {
            if(index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "must be >= 0");
            }
            return string.Format("new UiSelector().scrollable(true).instance({0})", index);
        }

        public static string ClassName(string value)
        {
            return SelectorString("className", value);
        }

        public static string ResourceId(string value)
        {
            return SelectorString("resourceId", value);
        }

        public static string Text(string value)
        {
            return SelectorString("text", value);
        }

        public static string TextContains(string value)
        {
            return SelectorString("textContains", value);
        }

        private static string SelectorString(string functionName, string text)
        {
            StringQA.ThrowIfNullOrWhiteSpace(functionName);
            StringQA.ThrowIfNullOrWhiteSpace(text);
            return string.Format("new UiSelector().{0}(\"{1}\")", functionName, text);
        }

        private static string SelectorString(string functionName, bool value)
        {
            StringQA.ThrowIfNullOrWhiteSpace(functionName);
            return string.Format("new UiSelector().{0}({1})", functionName, value.ToString().ToLower());
        }


    }
}
