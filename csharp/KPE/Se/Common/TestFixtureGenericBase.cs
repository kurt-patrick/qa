using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.Common
{
    public class TestFixtureGenericBase<T>
        : TestFixtureBase where T : PageBase
    {
        #region MyRegion
        protected T _pageObject = null;
        #endregion

        #region constructors
        public TestFixtureGenericBase(TestFixtureConfig configuration) 
            : base(configuration)
        {
        }
        #endregion

        #region methods
        public override void TestSetup()
        {
            LogDebugBrowserInfo();
            _pageObject = Helpers.ReflectionHelper.CreatePageObject<T>(_driver);
            _pageObject.NavigateTo();
            Assert.IsTrue(_pageObject.IsLoaded(), "Page failed to load");
        }
        #endregion

    }
}
