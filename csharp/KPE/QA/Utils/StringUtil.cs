using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.QA.Utils
{
    public static class StringUtil
    {
        public static void ThrowIfNullOrEmpty(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("String cannot be null, or empty");
            }
        }

        public static void ThrowIfNullOrWhiteSpace(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("String cannot be null, empty, or all whitespace");
            }
        }

        public static void ThrowIfNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(paramName + " cannot be null, empty, or all whitespace");
            }
        }

    }
}
