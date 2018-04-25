using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    [TestFixtureSource("CapabilitiesList")]
    //[Parallelizable(ParallelScope.Self)]
    [NonParallelizable]
    internal class AutomationChallengeTestBase<T> : TestBaseGeneric<T> where T : PageObjects.PageBase
    {
        protected const string Success = "Success";
        protected const string Fail = "Fail";
        protected const string NUnit_Category = "AutomationChallengesApp";

        protected NavigationDrawerPage _navigationDrawerPage = null;

        public AutomationChallengeTestBase(DesiredCapabilities capabilities) : base(capabilities)
        {
            _navigationDrawerPage = new NavigationDrawerPage(_driver);
        }

        public static List<DesiredCapabilities> CapabilitiesList()
        {
            return AppCapabilities.AutomationChallengeApp();
        }

    }
}
