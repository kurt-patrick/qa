using au.com.kleenheat.se.qa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se.helpers
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
