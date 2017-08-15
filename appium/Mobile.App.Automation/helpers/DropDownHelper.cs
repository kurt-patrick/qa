using KPE.Mobile.App.Automation.QA;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;

namespace KPE.Mobile.App.Automation.Helpers
{
    public class DropDownHelper : PageObjects.PageBase
    {
        private readonly IWebElement _element = null;

        public DropDownHelper(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public void SelectByText(IWebElement element, string text)
        {
            StringQA.ThrowIfNullOrWhiteSpace(text);

            // Click on the spinner so the list of elements is presented
            element.Click();

            // Click on the element based on text
            _driver.FindElementByXPath(string.Format("//*[@text='{0}']", text)).Click();
        }

        public string GetSelectedText()
        {
            throw new NotImplementedException("todo");
        }

        public override bool IsLoaded()
        {
            return _element.Displayed;
        }
    }
}