using KPE.Mobile.App.Automation.Helpers;
using NUnit.Framework;

namespace KPE.Mobile.App.Automation.Tests
{
    [TestFixture(Description = "UiSelectorHelper Tests")]
    public class UiSelectorHelperTests
    {
        [Test]
        public void UiSelectorHelperClassNameTest()
        {
            Assert.AreEqual(
                "new UiSelector().className(\"android.widget.CheckBox\")", 
                UiSelectorHelper.ClassName("android.widget.CheckBox"));
        }

        [Test]
        public void UiSelectorHelperResourceIdTest()
        {
            Assert.AreEqual(
                "new UiSelector().resourceId(\"io.selendroid.testapp:id/input_adds_check_box\")", 
                UiSelectorHelper.ResourceId("io.selendroid.testapp:id/input_adds_check_box"));
        }

        [Test]
        public void UiSelectorHelperTextTest()
        {
            Assert.AreEqual(
                "new UiSelector().text(\"Text\")", 
                UiSelectorHelper.Text("Text").ToString());
        }

        [Test]
        public void UiSelectorHelperTextContainsTest()
        {
            Assert.AreEqual(
                "new UiSelector().textContains(\"TextContains\")", 
                UiSelectorHelper.TextContains("TextContains").ToString());
        }

    }
}
