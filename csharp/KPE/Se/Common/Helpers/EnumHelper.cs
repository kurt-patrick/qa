using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KPE.Se.Common.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = 
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes.First().Description;
            }

            // no description exists - return the enum name
            return source.ToString();
        }
    }
}
