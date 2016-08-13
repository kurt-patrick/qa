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
    public class DragAndDropTests : TestFixtureGenericBase<DragAndDropPage>
    {
        public DragAndDropTests(TestFixtureConfig configuration)
            : base(configuration) { }

        [Test]
        public void TestDragAndDrop()
        {
            Assert.AreEqual("A", _pageObject.GetDivAText(), "Div A does not contain the text (A)");
            Assert.AreEqual("B", _pageObject.GetDivBText(), "Div B does not contain the text (B)");

            Assert.True(_pageObject.DragDivAOverDivB(), "Drag and drop failed");

            Assert.AreEqual("B", _pageObject.GetDivAText(), "Div A does not contain the text (B)");
            Assert.AreEqual("A", _pageObject.GetDivBText(), "Div B does not contain the text (A)");
        }

    }
}
