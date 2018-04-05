using KPE.Mobile.App.Automation.Configuration;
using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    class HomeScreenActivityTests : SelendroidAppTestBaseGeneric<HomeScreenPage>
    {
        public HomeScreenActivityTests(AppCapabilities caps) 
            : base(caps) 
        {
        }

        [Test]
        public void HomeScreenActivityIsLoadedTest()
        {
            _pageObject.AssertIsLoaded();
        }

        [Test]
        public void I_Accept_Adds_Test()
        {
            // assert is checked (default)
            _pageObject.AssertCheckBoxState(true);

            // assert text (default)
            _pageObject.AssertCheckBoxText("I accept adds");

            // uncheck
            _pageObject.ToggleCheckBox(false);
            _pageObject.AssertCheckBoxState(false);

            // check
            _pageObject.ToggleCheckBox(true);
            _pageObject.AssertCheckBoxState(true);

        }

        [Test]
        public void Wait_Dialog_Test()
        {
            _pageObject
                .ClickShowProgressBar()
                .SwitchPageObject<DialogPage>()
                .AssertIsLoaded()
                .AssertDialogIsClosed()
                .HideKeyboard<RegisterUserPage>()
                .AssertIsLoaded();
        }

        [Test]
        public void PageSourceTest()
        {
            string pageSource = _driver.PageSource;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(pageSource);

            Assert.AreEqual(3, GetNodeCount("//android.widget.FrameLayout"));
            Assert.AreEqual(3, GetNodeCount("//*[@class='android.widget.FrameLayout']"));
            Assert.AreEqual(1, GetNodeCount("//*[@resource-id='android:id/content']/*"));
            Assert.AreEqual(0, GetNodeCount("//*[@resource-id='android:id/content']/cake"));
            Assert.AreEqual(1, GetNodeCount("//*[@text='Displays a Toast']"));
            Assert.AreEqual(0, GetNodeCount("//*[text()='Displays a Toast']"));
            Assert.Throws<XPathException>(() => GetNodeCount("//*[text()='Displays a Toast'"));

            int GetNodeCount(string xpath)
            {
                try
                {
                    return xmlDoc.SelectNodes(xpath).Count;
                }
                catch (XPathException)
                {
                    // XPATH Statement is invalid
                    throw;
                }

            }


        }

    }
}
