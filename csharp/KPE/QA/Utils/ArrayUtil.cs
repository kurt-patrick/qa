using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.QA.Utils
{
    public static class ArrayUtil
    {
        /// <summary>
        /// Checks that at least 1 item exists and all items are not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>true if the above criteria is met</returns>
        public static bool IsArrayNullOrEmpty<T>(params T[] array)
        {
            return 
                (array == null) || 
                (array.Length == 0) ||
                (array.Count(itm => itm == null) > 0);
        }

        public static void ThrowIfArrayIsNullOrEmpty<T>(params T[] array)
        {
            if(IsArrayNullOrEmpty(array))
            {
                throw new ArgumentOutOfRangeException("The array must contain at least 1 value which cannot be null");
            }
        }

    }
}
