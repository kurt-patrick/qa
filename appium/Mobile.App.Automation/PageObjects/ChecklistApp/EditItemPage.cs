using KPE.Mobile.App.Automation.PageObjects.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace KPE.Mobile.App.Automation.PageObjects.ChecklistApp
{
    class EditItemPage : PageBase
    {
        public MobileElementWrapper AddDoneButton => new MobileElementWrapper(_driver, By.Id("edit_add_back"));
        public MobileElementWrapper CancelButton => new MobileElementWrapper(_driver, By.Id("edit_cancel"));
        public MobileElementWrapper TxtEdit => new MobileElementWrapper(_driver, By.Id("edit_edittext"));

        public EditItemPage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return IsDisplayed(TxtEdit, CancelButton, AddDoneButton);
        }

    }
}
