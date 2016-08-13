using KPE.Se.Common;
using KPE.Se.HerokuApp.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.HerokuApp.Tests
{
    public class DropDownTests : TestFixtureGenericBase<DropDownPage>
    {
        #region fields
        static Dictionary<int, string> _selectIndexesAndText = new Dictionary<int, string>() {
            { 1, "Option 1" },
            { 2, "Option 2" },
        };
        #endregion

        #region constructors
        public DropDownTests(TestFixtureConfig config)
            : base(config)
        {
        }
        #endregion

        #region methods
        [Test()]
        public void SelectByNameTests()
        {
            foreach (string text in _selectIndexesAndText.Values.ToList())
            {
                _pageObject.SelectByText(text);
                Assert.AreEqual(text, _pageObject.SelectedText());
            }
        }

        [Test()]
        public void SelectByNameTests_Fail()
        {
            const string Fail = "Fail";
            Assert.Throws<NoSuchElementException>(() => _pageObject.SelectByText(Fail));
            Assert.AreNotEqual(Fail, _pageObject.SelectedText());
        }

        [Test()]
        public void SelectByIndexTests()
        {
            foreach (int index in _selectIndexesAndText.Keys)
            {
                _pageObject.SelectByIndex(index);
                Assert.AreEqual(_selectIndexesAndText[index], _pageObject.SelectedText());
            }
        }
        #endregion

    }
}
