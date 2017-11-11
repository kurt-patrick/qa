using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;

namespace KPE.Mobile.App.Automation.PageObjects.AutomationChallengesApp
{
    class PinCodePage : PageBase
    {
        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtPin")]
        private IWebElement _txtPin = null;

        [CacheLookup()]
        [FindsByAndroidUIAutomator(ID = "txtPinEntered")]
        private IWebElement _txtPinEntered = null;

        public string PinEntered => _txtPinEntered.Text.Trim();

        public PinCodePage(AppiumDriver<IWebElement> driver) : base(driver)
        {
        }

        public override bool IsLoaded()
        {
            return _txtPin != null && _txtPin.Displayed;
        }

        public List<int> GetPin()
        {
            var retVal = new List<int>();
            var pinCode = _txtPin.Text.Trim().Split(" ".ToCharArray()).LastOrDefault();
            if (string.IsNullOrWhiteSpace(pinCode))
            {
                throw new Exceptions.InvalidStateException("failed to extract the pinCode from the android textview");
            }

            foreach(var ch in pinCode)
            {
                retVal.Add(int.Parse(ch.ToString()));
            }

            return retVal;
        }

        /// <summary>
        /// Enter the entire pin code
        /// </summary>
        /// <param name="list"></param>
        public void EnterPIN(List<int> list)
        {
            if (list == null || list.Count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(list), "the list must contain at least 1 number between 1 and 9");
            }
            list.ForEach(pinValue => ClickPinNumber(pinValue));
        }

        /// <summary>
        /// Click one of the pin numbers button e.g. 1 through 9
        /// </summary>
        /// <param name="number"></param>
        public void ClickPinNumber(int number)
        {
            if(number < 1 || number > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "must be between 1 and 9");
            }

            var locator = By.Id($"button{number}");
            Click(locator);
        }

    }
}
