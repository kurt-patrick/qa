using KPE.Mobile.App.Automation.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Helpers
{
    public class UiSelectorHelper
    {
        private UiSelectorHelper()
        {
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
