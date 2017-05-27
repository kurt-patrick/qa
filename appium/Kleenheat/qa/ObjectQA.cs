using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace au.com.kleenheat.se.qa
{
    public static class ObjectQA
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
            if (value == null)
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

        public static void ThrowIfIEnumerableDoesNotContainValue(IEnumerable<string> values, string value)
        {
            ThrowIfNull(values, "values");
            ThrowIfNull(value, "value");
            bool success = values.Any(key => string.Equals(key, value, StringComparison.CurrentCultureIgnoreCase));
            if (!success)
            {
                throw new ArgumentException("The IEnumerable list does not contain the key/value: " + value);
            }
        }

    }
}