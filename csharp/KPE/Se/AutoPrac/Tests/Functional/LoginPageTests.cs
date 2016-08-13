using KPE.Se.AutoPrac.PageObjects;
using KPE.Se.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.Tests.Functional
{
    [Parallelizable]
    public class LoginPageTests : Common.TestFixtureBase
    {
        private LoginPage _pageObj = null;

        public LoginPageTests(TestFixtureConfig config) : base(config) { }

        public override void TestSetup()
        {
            _pageObj = new LoginPage(_driver);
            _pageObj.NavigateTo();
            Assert.IsTrue(_pageObj.IsLoaded(), "Login page has failed to load");
        }

        [Test]
        public void LoginTest_Pass()
        {
            PerformLoginSteps(LoginPage.MyLoginEmail, LoginPage.MyLoginPassword);
            var myAccountPage = new MyAccountPage(_driver);
            Assert.IsTrue(myAccountPage.IsLoaded(), "My account page has failed to load");
        }

        [Test]
        public void LoginTest_Fail()
        {
            PerformLoginSteps("fake@email.com", "password");
            var errors = _pageObj.GetLoginErrors();
            Assert.IsTrue(errors != null && errors.FirstOrDefault(err => err.StartsWith("Authentication failed", StringComparison.CurrentCultureIgnoreCase)) != null, "Authentication failed was not displayed");
        }

        private void PerformLoginSteps(string email, string pwd)
        {
            _pageObj.LoginEmail = email;
            _pageObj.LoginPassword = pwd;
            _pageObj.ClickSignIn();
        }

        public override void TearDown()
        {
            _pageObj = null;
        }

    }

}
