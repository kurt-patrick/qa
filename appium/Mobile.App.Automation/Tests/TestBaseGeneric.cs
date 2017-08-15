using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects;

namespace KPE.Mobile.App.Automation.Tests
{
    public class TestBaseGeneric<T> : TestBase where T : PageObjects.PageBase
    {
        protected T _pageObject = null;
        public TestBaseGeneric(DriverCapabilities caps) : base(caps)
        {
            _pageObject = PageObjectFactory.Create<T>(_driver);
        }

    }
}
