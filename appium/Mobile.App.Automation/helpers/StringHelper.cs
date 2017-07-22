using KPE.Mobile.App.Automation.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class StringHelper
    {
        private static string AlphabetLower = "abcdefghijklmnopqrstuvwxyz";
        private static string AlphabetUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string AlphabetUpperLower = AlphabetUpper + AlphabetLower;

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

        public static string RandomString(int len)
        {
            string retVal = string.Empty;
            if(len > 0)
            {
                var rnd = new Random(DateTime.Now.Millisecond);
                for (int i=0; i<len; i++)
                {
                    retVal += AlphabetUpperLower[rnd.Next(AlphabetUpperLower.Length)];
                }
            }
            return retVal;
        }

        public static string RandomEmail()
        {
            return string.Format("{0}@{1}.com", RandomString(8), RandomString(4));
        }

    }
}
