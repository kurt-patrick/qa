using KPE.Mobile.App.Automation.PagesObjects;
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
            _pageObject = PageObjectFactory.Create<T>(_testCaseSettings);
        }

    }
}
