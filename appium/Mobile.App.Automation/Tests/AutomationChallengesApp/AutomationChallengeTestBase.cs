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
    // Fixtures: Run on 1 device at a time
    // Self: Runs test in parallel (worked well when running tests for a specific class)
    // Children: Runs test in parallel (with a single class 1 of the 2 tests failed with errors)
    // All: Runs test in parallel (with a single class 1 of the 2 tests failed with errors)

    [TestFixtureSource("CapabilitiesList")]
    [Parallelizable(ParallelScope.Self)]
    internal class AutomationChallengeTestBase<T> : TestBaseGeneric<T> where T : PageObjects.PageBase
    {
        protected const string Success = "Success";
        protected const string Fail = "Fail";

        protected NavigationDrawerPage _navigationDrawerPage = null;

        public AutomationChallengeTestBase(AppCapabilities caps) : base(caps)
        {
            _navigationDrawerPage = new NavigationDrawerPage(_driver);
        }

        public static List<AppCapabilities> CapabilitiesList()
        {
            return AppCapabilities.AutomationChallengeApp();
        }

    }
}
