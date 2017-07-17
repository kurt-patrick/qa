using KPE.Mobile.App.Automation.Common;
using KPE.Mobile.App.Automation.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.PagesObjects
{
    public static class PageObjectFactory
    {
        public static T Create<T>(TestCaseSettings testCaseSettings)  where T : PageBase
        {
            QA.ObjectQA.ThrowIfNull(testCaseSettings);
            return (T)Activator.CreateInstance(typeof(T), new object[] { testCaseSettings });
        }
    }
}
