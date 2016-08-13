using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using KPE.Se.HerokuApp.PageObjects;
using KPE.Se.Common;

namespace KPE.Se.HerokuApp.Tests
{
    public class BrokenImageTests : TestFixtureGenericBase<BrokenImagesPage>
    {
        public BrokenImageTests(TestFixtureConfig configuration)
            : base(configuration) { }

        [Test]
        public void TestForBrokenImages()
        {
            int index = 1;
            var expectedBroken = new bool[] { true, true, false };
            foreach(bool expected in expectedBroken)
            {
                bool actual = _pageObject.IsImageBroken(index);
                Assert.AreEqual(expected, actual, string.Format("Image {0} should be {1}", index, expected ? "broken" : "valid"));
                index += 1;
            }
        }

    }
}
