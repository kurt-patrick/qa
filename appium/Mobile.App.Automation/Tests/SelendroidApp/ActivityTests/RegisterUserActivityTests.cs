using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    class RegisterUserActivityTests : SelendroidAppTestBaseGeneric<HomeScreenPage>
    {
        public RegisterUserActivityTests(DesiredCapabilities caps) 
            : base(caps) 
        {
        }

        [Test]
        public void NewRegistrationTest()
        {
            _pageObject.Registration.Click();

            var registrationPage = Get<RegisterUserPage>(true);

            registrationPage.Username.PressKeys(RandomHelper.RandomString(3));
            registrationPage.Email.PressKeys(RandomHelper.RandomEmail());
            registrationPage.Password.PressKeys(RandomHelper.RandomString(3));
            registrationPage.Name.PressKeys(RandomHelper.RandomString(3));
            registrationPage.ProgrammingLanguage.SelectByText(RandomHelper.RandomString(new string[] { "Ruby", "PHP", "Scala", "Python" }));
            registrationPage.AcceptAdds.ToggleState(true);
            registrationPage.RegisterUser.Click();
        }

    }
}
