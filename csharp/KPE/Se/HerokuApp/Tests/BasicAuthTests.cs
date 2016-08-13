using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using KPE.Se.HerokuApp.PageObjects;
using OpenQA.Selenium;
using KPE.Se.Common;

namespace KPE.Se.HerokuApp
{
    public class BasicAuthTests : TestFixtureBase
    {
        #region constants
        private const string Valid_Username = "admin";
        #endregion

        #region fields
        private HerokuAppPage _herokuAppPage = null;
        #endregion

        #region constructors
        public BasicAuthTests(TestFixtureConfig configuration) 
            : base(configuration) { }
        #endregion

        #region methods
        public override void TestSetup()
        {
            _herokuAppPage = new HerokuAppPage(_driver);
        }

        [Test, Order(1), Ignore("When authentication box displays post click atag there is no way to enter data")]
        public void InvalidAuthenticationDetails()
        {
            TestLogic("admin1", true);
        }

        [Test, Order(2), Ignore("When authentication box displays post click atag there is no way to enter data")]
        public void ValidAuthenticationDetailsWithCancel()
        {
            TestLogic(Valid_Username, false);
        }

        [Test, Order(3)]
        public void ValidAuthenticationDetails()
        {
            TestLogic(Valid_Username, true);
        }

        private void TestLogic(string userName, bool clickOk)
        {
            _herokuAppPage.NavigateTo(userName, Valid_Username);
            Assert.True(_herokuAppPage.IsLoaded(), "Herokuapp has failed to load");

            _herokuAppPage.ClickBasicAuthATag();

            bool flgInvalidUserName = !Valid_Username.Equals(userName);

            if(flgInvalidUserName)
            {
                // Assert the login form appeared
                Assert.True(_herokuAppPage.IsAuthenticationAlertDisplayed(), "Clicking basic auth atag has not loaded the authentication login");

                // Enter login details (click ok)
                _herokuAppPage.EnterAuthenticationCredentials(userName, Valid_Username, clickOk);

                // Wait for the Alert to reappear (only if invalid details provided)
                Assert.True(_herokuAppPage.IsAuthenticationAlertDisplayed(), "AuthenticationAlert is not Displayed after submitting invalid login details");

            }

            // Validate the page loaded
            var authPage = new BasicAuthPage(_driver);
            Assert.True(authPage.IsLoaded(), "Authpage has failed to load");

            // Validate the page displays "Not authorized"
            if(flgInvalidUserName || clickOk == false)
            {
                Assert.AreEqual("Not authorized", authPage.GetErrorMessage(), "Authpage is not displaying expected error message");
            }
            else
            {
                // Assert page loaded correctly
                Assert.AreEqual("Congratulations! You must have the proper credentials.", authPage.GetSuccessMessage(), "Authpage is not displaying expected success message");
            }

        }

        public override void TearDown()
        {
            _herokuAppPage = null;
        }
        #endregion

    }
}
