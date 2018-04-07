using KPE.Mobile.App.Automation.Configuration;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.SelendroidApp
{
    [TestFixtureSource("CapabilitiesList")]
    public class SelendroidAppTestBaseGeneric<T> : TestBaseGeneric<T> where T : PageObjects.PageBase
    {
        protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SelendroidAppTestBaseGeneric(DesiredCapabilities capabilities) : base(capabilities)
        {
        }

        public static List<DesiredCapabilities> CapabilitiesList()
        {
            return AppCapabilities.SelendroidApp();
        }

    }
}
