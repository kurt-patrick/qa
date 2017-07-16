using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    [TestFixtureSource(DriverCapabilities.HomeScreenActivityOnGalaxyS4)]
    class HomeScreenActivityTests : TestBaseGeneric<HomeScreenActivityPage>
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

        /*
        [Test]
        public void Wait_Dialog_Test()
        {
            GetElementById("waitingButtonTest").Click();

            var wait = new WebDriverWait(_testCaseSettings.GetWebDriver(), TimeSpan.FromSeconds(20));

            // wait for modal to appear
            var modalMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("message")));

            // wait for modal to close
            wait.Until(ExpectedConditions.StalenessOf(modalMessage));

            // wait for username to be visible
            var username = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("io.selendroid.testapp:id/inputUsername")));
            username.SendKeys("testignigingn");

            // wait for register to exist
            var registerBtn = wait.Until(ExpectedConditions.ElementExists(By.Id("io.selendroid.testapp:id/btnRegisterUser")));
            registerBtn.Click();

        }

        /// <summary>
        /// AndroidElements must used the attribute checked to determine a checkbox's state
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsChecked(IWebElement element)
        {
            AndroidElement androidEle = element as AndroidElement;
            if(androidEle != null) 
            {
                return string.Equals("true", element.GetAttribute("checked"));
            }
            return element.Selected;
        }

        private IWebElement GetElementById(string id)
        {
            return GetElementByFullId(ID_BASE + id);
        }

        private IWebElement GetElementByFullId(string id)
        {
            return _driver.FindElement(By.Id(id));
        }
        */

        public static List<string> HomeScreenActivityOnGalaxyS4Capabilities()
        {
            return DriverCapabilities.HomeScreenActivityOnGalaxyS4Capabilities();
        }

    }
}
