using KPE.Mobile.App.Automation.QA;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;
using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    public sealed class AppiumLocalServiceBuilder
    {
        public AppiumLocalService LocalService { get; private set; } = null;

        private AppiumLocalServiceBuilder(AppiumLocalService appiumLocalService)
        {
            LocalService = appiumLocalService;
        }

        public static AppiumLocalServiceBuilder Build(DesiredCapabilities capabilities)
        {
            ObjectQA.ThrowIfNull(capabilities);

            var serverOptions = new OptionCollector();
            serverOptions.AddCapabilities(capabilities);

            var appiumLocalService = 
                new AppiumServiceBuilder()
                    .UsingAnyFreePort()
                    .WithArguments(serverOptions)
                    .WithIPAddress("127.0.0.1")
                    .WithStartUpTimeOut(TimeSpan.FromSeconds(30))
                    .Build();

            return new AppiumLocalServiceBuilder(appiumLocalService);
        }

        public AppiumLocalServiceBuilder Start()
        {
            ObjectQA.ThrowIfNull(LocalService);
            if (!LocalService.IsRunning)
            {
                LocalService.Start();
            }
            return this;
        }

        public AppiumLocalService AssertIsRunning(int timeout)
        {
            ObjectQA.ThrowIfNull(LocalService);
            TryHelper.TryWaitForCondition(() => LocalService.IsRunning, timeout);
            Assert.IsTrue(LocalService.IsRunning, "Appium local service is not running");
            return LocalService;
        }

    }
}
