using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using NUnit.Framework;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    [TestFixtureSource("CapabilitiesList")]
    internal class ChecklistTestBase : TestBaseGeneric<MainPage>
    {
        public ChecklistTestBase(DriverCapabilities caps) : base(caps)
        {
        }

        public static List<DriverCapabilities> CapabilitiesList()
        {
            return DriverCapabilities.ChecklistApp();
        }

    }
}
