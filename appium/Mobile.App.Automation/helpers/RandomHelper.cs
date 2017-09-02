using KPE.Mobile.App.Automation.QA;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class RandomHelper
    {
        const string AlphabetLower = "abcdefghijklmnopqrstuvwxyz";
        const string AlphabetUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string AlphabetUpperLower = AlphabetUpper + AlphabetLower;

        private static Random _random = new Random(DateTime.Now.Millisecond);

        public static int RandomInt()
        {
            return _random.Next();
        }

        public static int RandomInt(int max)
        {
            return _random.Next(max + 1);
        }

        public static int RandomInt(int min, int max)
        {
            return _random.Next(min, max + 1);
        }

        public static string RandomString(IEnumerable<string> list)
        {
            return list.ElementAt(RandomIndex(list));
        }

        public static int RandomIndex(IEnumerable<object> list)
        {
            ObjectQA.ThrowIfIEnumerableIsEmpty(list);
            return RandomInt(list.Count()-1);
        }

        public static T RandomObject<T>(IEnumerable<object> list)
        {
            int index = RandomIndex(list);
            return (T)list.ToArray()[index];
        }

        public static string RandomString(int len)
        {
            string retVal = string.Empty;
            if (len > 0)
            {
                var rnd = new Random(DateTime.Now.Millisecond);
                for (int i = 0; i < len; i++)
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
