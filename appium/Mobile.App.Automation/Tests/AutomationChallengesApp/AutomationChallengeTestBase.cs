using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    [TestFixtureSource("CapabilitiesList")]
    internal class AutomationChallengeTestBase<T> : TestBaseGeneric<T> where T : PageObjects.PageBase
    {
        protected const string Success = "Success";
        protected const string Fail = "Fail";

        protected NavigationDrawerPage _navigationDrawerPage = null;

        public AutomationChallengeTestBase(DriverCapabilities caps) : base(caps)
        {
            _navigationDrawerPage = new NavigationDrawerPage(_driver);
        }

        public static List<DriverCapabilities> CapabilitiesList()
        {
            return DriverCapabilities.AutomationChallengeApp();
        }

    }
}
