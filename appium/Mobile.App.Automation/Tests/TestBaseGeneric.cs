using System;
using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects;
using OpenQA.Selenium.Remote;

namespace KPE.Mobile.App.Automation.Tests
{
    public class TestBaseGeneric<T> : TestBase where T : PageBase
    {
        protected T _pageObject = null;
        public TestBaseGeneric(DesiredCapabilities capabilities) : base(capabilities)
        {
            _pageObject = PageObjectFactory.Create<T>(_driver);
        }
    }
}
