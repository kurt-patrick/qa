using KPE.Mobile.App.Automation.Helpers;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests
{
    [TestFixture(Description = "UiSelectorHelper Tests")]
    public class UiSelectorChainedHelperTests
    {
        const string NUnit_Category = "SelectorHelper";

        [Test]
        [Category(NUnit_Category)]
        public void UiSelectorChainedClassNameTest()
        {
            Assert.AreEqual(
                "new UiSelector().className(\"android.widget.CheckBox\")", 
                new UiSelectorChainedHelper().ClassName("android.widget.CheckBox").ToString());
        }

        [Test]
        [Category(NUnit_Category)]
        public void UiSelectorChainedResourceIdTest()
        {
            Assert.AreEqual(
                "new UiSelector().resourceId(\"io.selendroid.testapp:id/input_adds_check_box\")", 
                new UiSelectorChainedHelper().ResourceId("io.selendroid.testapp:id/input_adds_check_box").ToString());
        }

        [Test]
        [Category(NUnit_Category)]
        public void UiSelectorChainedScrollableTest()
        {
            Assert.AreEqual(
                "new UiSelector().scrollable(true)", 
                new UiSelectorChainedHelper().Scrollable(true).ToString());
        }

        [Test]
        [Category(NUnit_Category)]
        public void UiSelectorChainedTextTest()
        {
            Assert.AreEqual(
                "new UiSelector().text(\"Text\")", 
                new UiSelectorChainedHelper().Text("Text").ToString());
        }

        [Test]
        [Category(NUnit_Category)]
        public void UiSelectorChainedTextContainsTest()
        {
            Assert.AreEqual(
                "new UiSelector().textContains(\"TextContains\")", 
                new UiSelectorChainedHelper().TextContains("TextContains").ToString());
        }

        [Test]
        [Category(NUnit_Category)]
        public void UiSelectorChainedTextAndScrollableTest()
        {
            Assert.AreEqual(
                "new UiSelector().text(\"Text\").scrollable(false)", 
                new UiSelectorChainedHelper().Text("Text").Scrollable(false).ToString());
        }

    }
}
