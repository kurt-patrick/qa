using KPE.Se.Common.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.HerokuApp.PageObjects
{
    internal class HerokuAppPage : Common.PageBase
    {
        #region locators
        By _basicAuthATag = By.LinkText("Basic Auth");  // By.XPath("//a[text()='Basic Auth']");
        #endregion

        #region constructors
        public HerokuAppPage(IWebDriver driver)
            : base(driver, Constants.BaseUrl) 
        { 
        }
        #endregion

        #region methods
        internal void NavigateTo(string userName, string password)
        {
            string url = string.Format("https://{0}:{1}@the-internet.herokuapp.com/", userName, password);
            NavigateTo(url);
        }

        public override bool IsLoaded()
        {
            var locators = new List<By> { _basicAuthATag };
            return AreElementsVisible(locators);
        }

        internal void ClickBasicAuthATag()
        {
            PerformClick(_basicAuthATag);
            //return ClickElementAndWaitForAlert(_basicAuthATag);
        }

        internal bool IsAuthenticationAlertDisplayed()
        {
            return WaitHelper.TryWaitForCondition(() => _driver.SwitchTo().Alert() != null);
        }

        internal void EnterAuthenticationCredentials(string userName, string password, bool clickOk)
        {
            var alert = _driver.SwitchTo().Alert();
            alert.SetAuthenticationCredentials(userName, password);

            if (clickOk) 
            {
                alert.Accept();
            }
            else
            {
                alert.Dismiss();
            }

        }

        internal void ClickDismissOnAuthenticationAlert()
        {
            _driver.SwitchTo().Alert().Dismiss();
        }
        #endregion

    }
}
