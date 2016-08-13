using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Helpers
{
    public static class RandomHelper
    {
        private static Random _random = new Random(DateTime.Now.Millisecond);

        public static int GetRandom(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static T GetRandom<T>(IEnumerable<T> values)
        {
            QA.Utils.ObjectUtil.ThrowIfIEnumerableIsEmpty(values);
            var list = values.ToList();
            var index = GetRandom(0, list.Count - 1);
            return list[index];
        }

    }
}
