using Applitools.Appium;
using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.ChecklistApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace KPE.Mobile.App.Automation.Tests.ChecklistApp
{
    [TestFixtureSource("CapabilitiesList")]
    internal class ChecklistTestBase : TestBaseGeneric<MainPage>
    {
        const string AppName = "ChecklistApp";

        //Eyes _eyes = null;

        public ChecklistTestBase(DesiredCapabilities capabilities) : base(capabilities)
        {
        }

        public static List<DesiredCapabilities> CapabilitiesList()
        {
            return AppCapabilities.ChecklistApp();
        }

        /*
        private Eyes GetEyes(string testName)
        {
            if(_eyes == null)
            {
                _eyes = new Eyes();
                _eyes.ApiKey = EnvironmentHelper.GetEnvVariable("ApplitoolsEyesApiKey");
                _eyes.Open(_driver, AppName, testName);
            }
            return _eyes;
        }
        */


    }
}
