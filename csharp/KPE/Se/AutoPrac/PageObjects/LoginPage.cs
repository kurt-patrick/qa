using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.AutoPrac.PageObjects
{
    public class LoginPage : Common.PageBase
    {
        // Details located @ http://www.seleniumframework.com/demo-sites/
        public const string DemoLoginEmail = "abc@xyz.com";
        public const string DemoLoginPassword = "Test@123";
        public const string MyLoginEmail = "";
        public const string MyLoginPassword = "";

        By _emailBy = By.Id("email");
        By _passwordBy = By.Id("passwd");
        By _signInButtonBy = By.Id("SubmitLogin");
        By _errorMessagesBy = By.XPath("//div[@id='center_column']/div/ol/li");

        public LoginPage(IWebDriver driver)
            : base(driver)
        {
            _baseUrl = "http://automationpractice.com/index.php?controller=authentication&back=my-account";
        }

        public string LoginEmail
        {
            get { return GetText(_emailBy); }
            set { SendKeys(_emailBy, value, true); }
        }

        public string LoginPassword
        {
            get { return GetText(_passwordBy); }
            set { SendKeys(_passwordBy, value, true); }
        }

        public void ClickSignIn()
        {
            PerformClick(_signInButtonBy);
        }

        public override bool IsLoaded()
        {
            return AreElementsVisible(new List<By> { _emailBy, _passwordBy, _signInButtonBy });
        }

        public List<string> GetLoginErrors()
        {
            return 
                FindElements(_errorMessagesBy)
                .Select(ele => ele.Text)
                .ToList();
        }

    }
}
