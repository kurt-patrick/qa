using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    class RegisterUserActivityTests : SelendroidAppTestBaseGeneric<HomeScreenPage>
    {
        public RegisterUserActivityTests(DriverCapabilities caps) 
            : base(caps) 
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

    }
}
