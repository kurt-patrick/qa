using KPE.Mobile.App.Automation.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using System;
using System.Diagnostics;
using System.IO;

namespace KPE.Mobile.App.Automation.Helpers
{
    public sealed class AppiumLocalServiceHelper
    {
        private AppiumLocalService _appiumLocalService = null;

        private AppiumLocalServiceHelper(AppiumLocalService service)
        {
            _appiumLocalService = service;
        }

        public static AppiumLocalServiceHelper Build(DriverCapabilities capabilities, out AppiumLocalService service)
        {
            var serverOptions = new OptionCollector();
            serverOptions.AddCapabilities(capabilities.DesiredCapabilities());

            var builder = new AppiumServiceBuilder();

            service = builder
                .UsingAnyFreePort()
                .WithArguments(serverOptions)
                .WithIPAddress("127.0.0.1")
                .WithStartUpTimeOut(TimeSpan.FromSeconds(10))
                .Build();

            return new AppiumLocalServiceHelper(service);
        }

        public AppiumLocalServiceHelper Start()
        {
            if (!_appiumLocalService.IsRunning)
            {
                _appiumLocalService.Start();
            }
            return this;
        }

        public AppiumLocalServiceHelper WaitForIsRunning(int timeout)
        {
            WaitHelper.TryWaitForCondition(() => IsRunning(), timeout);
            return this;
        }

        public bool IsRunning()
        {
            return _appiumLocalService.IsRunning;
        }

        public void AssertIsRunning()
        {
            Assert.IsTrue(IsRunning(), "Appium local service is not running");
        }

    }
}
