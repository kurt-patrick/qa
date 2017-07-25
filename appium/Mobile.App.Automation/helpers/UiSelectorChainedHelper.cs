using KPE.Mobile.App.Automation.QA;

namespace KPE.Mobile.App.Automation.Helpers
{
    public class UiSelectorChainedHelper
    {
        private string _selector = null;

        public UiSelectorChainedHelper()
        {
            _selector = "new UiSelector()";
        }

        public UiSelectorChainedHelper ClassName(string value)
        {
            return AppendSelector("className", value);
        }

        public UiSelectorChainedHelper ResourceId(string value)
        {
            return AppendSelector("resourceId", value);
        }

        public UiSelectorChainedHelper Text(string value)
        {
            return AppendSelector("text", value);
        }

        public UiSelectorChainedHelper TextContains(string value)
        {
            return AppendSelector("textContains", value);
        }

        public UiSelectorChainedHelper Scrollable(bool value)
        {
            return AppendSelector("scrollable", value);
        }

        private UiSelectorChainedHelper AppendSelector(string functionName, string text)
        {
            StringQA.ThrowIfNullOrWhiteSpace(functionName);
            StringQA.ThrowIfNullOrWhiteSpace(text);
            _selector += string.Format(".{0}(\"{1}\")", functionName, text);
            return this;
        }

        private UiSelectorChainedHelper AppendSelector(string functionName, bool value)
        {
            StringQA.ThrowIfNullOrWhiteSpace(functionName);
            _selector += string.Format(".{0}({1})", functionName, value.ToString().ToLower());
            return this;
        }

        public override string ToString()
        {
            return _selector;
        }


    }
}
