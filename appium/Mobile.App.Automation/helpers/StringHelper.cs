using KPE.Mobile.App.Automation.QA;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class StringHelper
    {
        public static string GetValueOrDefault(string value, string defaultValue)
        {
            ObjectQA.ThrowIfNull(value);
            ObjectQA.ThrowIfNull(defaultValue);

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return defaultValue;
        }

    }
}
