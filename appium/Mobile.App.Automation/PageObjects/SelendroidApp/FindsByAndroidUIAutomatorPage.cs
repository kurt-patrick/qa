using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.Selendroid
{
    /// <summary>
    /// All of these locators are finding the same element however using a different locator method
    /// This has been done purely as an example of different ways of doing things
    /// </summary>
    public class FindsByAndroidUIAutomatorPage : PageBase
    {
        public MobileElementWrapper ByID => new MobileElementWrapper(_driver, By.Id("input_adds_check_box"));
        public MobileElementWrapper ByClassName => new MobileElementWrapper(_driver, By.ClassName("android.widget.CheckBox"));
        public MobileElementWrapper ByXPath => new MobileElementWrapper(_driver, By.XPath("//android.widget.CheckBox[@text='I accept adds']"));
        public MobileElementWrapper ByXPathGeneric => new MobileElementWrapper(_driver, By.XPath("//*[@text='I accept adds']"));


        public FindsByAndroidUIAutomatorPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return true;
        }

    }
}
