using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.Common.Helpers
{
    public static class ReflectionHelper
    {

        public static T CreatePageObject<T>(IWebDriver driver) where T : Common.PageBase
        {
            T retVal = default(T);
            Type pageClassType = typeof(T);

            ConstructorInfo ctor = pageClassType.GetConstructor(new Type[] { typeof(IWebDriver) });

            QA.Utils.ObjectUtil.ThrowIfNull(ctor, "ctor",
                "No constructor for the specified class containing a single argument of type IWebDriver can be found");

            QA.Utils.ObjectUtil.ThrowIfNull(driver, "driver",
                "The IWebDriver object provided must be instantiated and cannot be null");

            retVal = (T)ctor.Invoke(new object[] { driver });

            return retVal;

        }

    }


}
