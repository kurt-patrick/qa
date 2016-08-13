using KPE.Se.Common;
using KPE.Se.HerokuApp.PageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.HerokuApp.Tests
{
    public class MultipleWindows : TestFixtureGenericBase<MultipleWindowsPage>
    {
        #region constructors
        public MultipleWindows(TestFixtureConfig configuration)
            : base(configuration) { }
        #endregion

        #region methods
        [Test()]
        public void TestMultipleWindows()
        {
            string currentWindowHandle = _pageObject.GetCurrentWindowHandle();
            _pageObject.ClickATag();
            string newWindowHandle = _pageObject.GetNewWindowHandle(Periods.TimeOutDefault, currentWindowHandle);
            Assert.True(!string.IsNullOrWhiteSpace(newWindowHandle), "Failed to open a new window");
            Assert.AreEqual("New Window", _pageObject.GetWindowTitle(newWindowHandle), "New windows Title is incorrect");
        }
        #endregion
    }
}
