using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.QA.Utils
{
    public static class ObjectUtil
    {
        public static void ThrowIfNull(object value)
        {
            ThrowIfNull(value, "Variable or Parameter");
        }

        public static void ThrowIfNull(object value, string paramName)
        {
            ThrowIfNull(value, paramName, "The object is null");
        }

        public static void ThrowIfNull(object value, string paramName, string message)
        {
            if(value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void ThrowIfIEnumerableIsEmpty<T>(IEnumerable<T> values)
        {
            ThrowIfNull(values, "values");
            var list = values.ToList();
            if (list.Count == 0)
            {
                throw new ArgumentException("The IEnumerable list must contain at least 1 value");
            }
        }


    }
}
