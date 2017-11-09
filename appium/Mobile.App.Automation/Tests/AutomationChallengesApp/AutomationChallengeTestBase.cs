using KPE.Mobile.App.Automation.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Tests.AutomationChallengesApp
{
    [TestFixtureSource("CapabilitiesList")]
    internal class AutomationChallengeTestBase : TestBase
    {
        public AutomationChallengeTestBase(DriverCapabilities caps) : base(caps)
        {
        }

        public static List<DriverCapabilities> CapabilitiesList()
        {
            return DriverCapabilities.AutomationChallengeApp();
        }

    }
}
