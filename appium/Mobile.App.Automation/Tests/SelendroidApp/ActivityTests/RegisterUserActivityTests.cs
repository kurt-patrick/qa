using NUnit.Framework;
using System.Collections.Generic;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using KPE.Mobile.App.Automation.Helpers;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    [TestFixtureSource("GalaxyS4")]
    class RegisterUserActivityTests : TestBaseGeneric<HomeScreenPage>
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RegisterUserActivityTests(string capabilities) 
            : base(capabilities) 
        {
        }

        [Test]
        public void NewRegistrationTest()
        {
            var registrationPage =
                _pageObject
                    .ClickUserRegistration()
                    .SwitchPageObject<RegisterUserPage>()
                    .AssertIsLoaded();

            registrationPage.Username = StringHelper.RandomString(6);
            registrationPage.Email = StringHelper.RandomEmail();
            registrationPage.Password = StringHelper.RandomString(6);
            registrationPage.Name = StringHelper.RandomString(6);
            registrationPage.ProgrammingLanguage = RandomHelper.RandomString(new string[] { "Ruby", "PHP", "Scala", "Python" });
            registrationPage.AcceptAdds = true;
            registrationPage.ClickRegisterUser();
        }

        public static List<string> GalaxyS4()
        {
            return GalaxyS4Capabilities.HomeScreenCapabilites();
        }

    }
}
