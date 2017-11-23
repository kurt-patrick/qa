using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class SwipeToDeletePage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtActual")]
        private IWebElement _txtActual = null;

        public string Actual => _txtActual.Text.Trim();

        public SwipeToDeletePage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return _txtActual.Displayed;
        }

        public void SwipeFirstRow()
        {
            var element = _driver.FindElement(By.Id("swipe_left"));
            SwipeLeft(element);
        }

        void SwipeLeft(IWebElement element)
        {
            int yPos = element.Location.Y + (int)(0.5 * element.Size.Height);
            int xLeft =  (int)(0.25 * element.Size.Width);
            int xRight = (int)(0.75 * element.Size.Width);

            new TouchAction(_driver)
                .Press(xRight, yPos)
                .Wait(500)
                .MoveTo(xLeft, yPos)
                .Release()
                .Perform();
        }

    }
}
