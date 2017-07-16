using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Mobile.App.Automation.Tests
{
    public class TestBaseGeneric<T> : TestBase where T : PageObjects.PageBase
    {
        protected T _pageObject = null;
        public TestBaseGeneric(string testFixtureData) : base(testFixtureData)
        {
            _pageObject = (T)Activator.CreateInstance(typeof(T), new object[] { this._testCaseSettings });
        }

    }
}
