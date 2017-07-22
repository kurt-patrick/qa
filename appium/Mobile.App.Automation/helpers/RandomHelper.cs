using KPE.Mobile.App.Automation.QA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Helpers
{
    public static class RandomHelper
    {
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

    }
}
