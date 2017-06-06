using au.com.kleenheat.se.pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se.tests
{
    public class End2End : TestBase
    {
        private HomePage _homePage = null;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public End2End(string capabilities) 
            : base(capabilities) 
        {
        }

        public override void TestSetup()
        {
            _homePage = new HomePage(_testCaseSettings);
        }



        [Test]
        [Category("e2e")]
        public void SearchByPostCodeCreateNewCustomerWithFail()
        {
            _log.Info("Pre test");
            _homePage.NavigateToPage();
            Assert.True(_homePage.IsLoaded(), "homepage failed to load");            
            Assert.True(_homePage.EnterPostCode("6001").ClickGetStarted().IsTrayVisible(), "homepage postcode search tray is not visible");

            var newCustomerPage = _homePage.ClickTrayLinkBecomeACustomer();
            Assert.True(newCustomerPage.IsLoaded(), "New Residential customer page failed to load");
            newCustomerPage.EnterContactDetails("Mr.", "Bob", "Brown", "0894001234", "bob.brown@email.com", "23121980");
            newCustomerPage.EnterCyclinderDetails("19 William Street", "Perth", "6061", "Western Australia", "3", false);
            newCustomerPage.EnterTermsAndConditions(true, true);
            newCustomerPage.ClickSubmit();

            string expected = "There was a problem with your submission. Errors have been highlighted below.";
            Assert.AreEqual(expected, newCustomerPage.GetErrorMessage());
            _log.Info("Post test");
        }

        [Test]
        [Category("smoke")]
        public void HomePageLoads()
        {
            _homePage.NavigateToPage();
            Assert.True(_homePage.IsLoaded(), "homepage failed to load");
        }

        //[Test]
        public void AndroidApp_SMemo_Test()
        {
            // package := com.sec.android.widgetapp.diotek.smemo
            _testCaseSettings.GetWebDriver().FindElement(By.Id(@"com.sec.android.widgetapp.diotek.smemo:id/actionbar_new_memo_text")).Click();
            System.Threading.Thread.Sleep(500);

            IWebElement element = _testCaseSettings.GetWebDriver().FindElement(By.Id(@"com.sec.android.widgetapp.diotek.smemo:id/switcher"));
            element.SendKeys("test");

            Assert.AreEqual("test", element.Text);



        }

    }
}
