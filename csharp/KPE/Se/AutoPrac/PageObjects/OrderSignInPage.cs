using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class OrderSignInPage : OrderProgressBase
    {
        LoginPage _loginPage = null;
        public OrderSignInPage(IWebDriver driver)
            : base(driver, eCurrentStep.SignIn)
        {
            _loginPage = new LoginPage(driver);
        }

        protected override List<By> IsLoadedElements()
        {
            return null;
        }

        protected override By ProceedToCheckoutBy()
        {
            return null;
        }

        public void Login(string email, string password)
        {
            _loginPage.LoginEmail = email;
            _loginPage.LoginPassword = password;
            _loginPage.ClickSignIn();
        }

    }
}
