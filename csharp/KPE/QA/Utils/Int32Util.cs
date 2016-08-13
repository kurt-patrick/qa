using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.QA.Utils
{
    public static class Int32Util
    {
        public static bool IsValueBetween(int min, int max, int value)
        {
            return value >= min && value <= max;
        }

        public static bool IsValueAtLeast(int min, int value)
        {
            return value >= min;
        }

        public static void ThrowIfNotAtLeast(int min, int value)
        {
            if(!IsValueAtLeast(min, value))
            {
                throw new ArgumentOutOfRangeException(string.Format("Expecting value to be >= ({0}) Actual value is ({1})", min, value));
            }
        }

        public static void ThrowIfNotBetween(int min, int max, int value)
        {
            if (!IsValueBetween(min, max, value))
            {
                throw new ArgumentOutOfRangeException(string.Format("Expecting value to be between ({0} and {1}) Actual value is ({2})", min, max, value));
            }
        }

    }
}
