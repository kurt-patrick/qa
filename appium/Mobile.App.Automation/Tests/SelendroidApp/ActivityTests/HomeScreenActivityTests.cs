using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    [TestFixtureSource("GalaxyS4")]
    class HomeScreenActivityTests : TestBaseGeneric<HomeScreenPage>
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HomeScreenActivityTests(string capabilities) 
            : base(capabilities) 
        {
        }

        [Test]
        public void HomeScreenActivityIsLoadedTest()
        {
            _pageObject.AssertIsLoaded();
        }

        [Test]
        public void I_Accept_Adds_Test()
        {
            // assert is checked (default)
            _pageObject.AssertCheckBoxState(true);

            // assert text (default)
            _pageObject.AssertCheckBoxText("I accept adds");

            // uncheck
            _pageObject.ToggleCheckBox(false);
            _pageObject.AssertCheckBoxState(false);

            // check
            _pageObject.ToggleCheckBox(true);
            _pageObject.AssertCheckBoxState(true);

        }

        [Test]
        public void Wait_Dialog_Test()
        {
            _pageObject
                .ClickShowProgressBar()
                .SwitchPageObject<DialogPage>()
                .AssertIsLoaded()
                .AssertDialogIsClosed()
                .HideKeyboard<RegisterUserPage>()
                .AssertIsLoaded();
        }

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.HomeScreenActivity();
        }

    }
}
