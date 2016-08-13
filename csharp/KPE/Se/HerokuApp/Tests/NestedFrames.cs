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
    public class NestedFrames : TestFixtureGenericBase<NestedFramesPage>
    {
        #region constructors
        public NestedFrames(TestFixtureConfig config)
            : base(config) { }
        #endregion

        #region methods
        [Test()]
        public void TestAllFramesExist()
        {
            // Parent frame
            Assert.True(_pageObject.DoesTopFrameExist(), "Top Frame does not exist");

            // Child frames of Top frame
            Assert.True(_pageObject.DoesLeftFrameExist(), "Left Frame does not exist");
            Assert.True(_pageObject.DoesMiddleFrameExist(), "Middle Frame does not exist");
            Assert.True(_pageObject.DoesRightFrameExist(), "Right Frame does not exist");

            // independant frame
            Assert.True(_pageObject.DoesBottomFrameExist(), "Bottom Frame does not exist");
        }
        #endregion

    }
}
