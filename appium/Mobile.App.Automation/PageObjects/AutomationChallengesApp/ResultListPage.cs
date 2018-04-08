using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class ResultListPage : ListViewWrapper
    {
        public MobileElementWrapper Actual => new MobileElementWrapper(_driver, By.Id("txtActual"));
        public MobileElementWrapper Expected => new MobileElementWrapper(_driver, By.Id("txtExpected"));

        public ResultListPage(AppiumDriver<IWebElement> driver) : base(driver, "//android.support.v7.widget.RecyclerView")
        {
        }

        public string[] GetExpected()
        {
            var array = Expected.Text(true).Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if(array.Length != 4)
            {
                throw new Exceptions.InvalidStateException("The expected value should contain 4 values");
            }
            return array;
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(Actual, Expected);
        }
    }
}
