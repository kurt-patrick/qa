using KPE.Mobile.App.Automation.PageObjects.Selendroid;
using KPE.Mobile.App.Automation.Tests.SelendroidApp;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Xml;
using System.Xml.XPath;

namespace KPE.Mobile.App.Automation.Tests.Selendroid.ActivityTests
{
    class HomeScreenActivityTests : SelendroidAppTestBaseGeneric<HomeScreenPage>
    {
        public HomeScreenActivityTests(DesiredCapabilities caps) 
            : base(caps) 
        {
        }

        [Test]
        public void HomeScreenActivityIsLoadedTest()
        {
            Assert.IsTrue(_pageObject.IsLoaded());
        }

        [Test]
        public void I_Accept_Adds_Test()
        {
            // assert is checked (default)
            Assert.AreEqual(true, _pageObject.CheckBox.Selected());

            // assert text (default)
            Assert.AreEqual("I accept adds", _pageObject.CheckBox.Text());

            // uncheck
            _pageObject.CheckBox.Click();
            Assert.AreEqual(false, _pageObject.CheckBox.Selected());

            // check
            _pageObject.CheckBox.Click();
            Assert.AreEqual(true, _pageObject.CheckBox.Selected());

        }

        [Test]
        public void Wait_Dialog_Test()
        {
            _pageObject.ProgressButton.Click();

            var dialogPage = Get<DialogPage>(true);
            dialogPage.AssertDialogIsClosed();

            dialogPage.HideKeyboard();

            Get<RegisterUserPage>(true);
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
