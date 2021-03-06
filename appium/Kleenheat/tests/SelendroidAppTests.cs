﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se.tests
{
    [TestFixtureSource("DriverCaps")]
    [Ignore("playing around with android apps")]
    class SelendroidAppTests : TestBase
    {
        private const string ID_BASE = "io.selendroid.testapp:id/";
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SelendroidAppTests(string capabilities) 
            : base(capabilities) 
        {
        }

        public override void TestSetup()
        {
            // nothing to do here
        }

        [Test]
        [Category("android")]
        public void I_Accept_Adds_Test()
        {
            var element = GetElementById("input_adds_check_box");

            // check defaults
            Assert.AreEqual("I accept adds", element.Text);
            Assert.True(IsChecked(element), "Checkbox is not selected");

            // Click and validate (Unchecked)
            element.Click();
            Assert.False(IsChecked(element));

            // Click and validate (Checked)
            element.Click();
            Assert.True(IsChecked(element));

        }

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

        public static List<string> DriverCaps()
        {
            return new List<string>() 
            { 
                "appiumVersion=1.4.16.1,device=Android,deviceName=a710eaec,appPackage=io.selendroid.testapp,appActivity=.HomeScreenActivity" 
            };
        }

    }
}
