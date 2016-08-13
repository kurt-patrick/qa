using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.DemoQA.PageObjects
{
    internal class RegistrationPage : Common.PageBase
    {
        internal enum eErrorArea
        {
            Firstname,
            Lastname,
            Hobby,
            Phone,
            Username,
            Email,
            Pwd,
            PwdConfirm
        }

        public enum eMaritalStatus
        {
            NotSet,
            Single,
            Married,
            Divorced
        }

        public enum eHobby
        {
            Dance,
            Reading,
            Cricket
        }

        private Dictionary<eErrorArea, int> _errorDivDict = new Dictionary<eErrorArea, int> 
        {
            { eErrorArea.Firstname, 1 }, { eErrorArea.Lastname, 1 }, { eErrorArea.Hobby, 3 }, 
            { eErrorArea.Phone, 6 }, { eErrorArea.Username, 7 }, { eErrorArea.Email, 8 }, 
            { eErrorArea.Pwd, 11 }, { eErrorArea.PwdConfirm, 12 }
        };

        By _h1Tag = By.TagName("h1");
        By _entryDivTag = By.ClassName("entry-content");
        By _submitInputTag = By.Name("pie_submit");

        [FindsBy(How = How.Id, Using = "name_3_firstname")]
        [CacheLookup()]
        IWebElement _firstName = null;

        [FindsBy(How = How.Id, Using = "name_3_lastname")]
        [CacheLookup()]
        IWebElement _lastName = null;

        [FindsBy(How = How.Id, Using = "dropdown_7")]
        [CacheLookup()]
        IWebElement _country = null;

        [FindsBy(How = How.Id, Using = "mm_date_8")]
        [CacheLookup()]
        IWebElement _dobMonth = null;

        [FindsBy(How = How.Id, Using = "dd_date_8")]
        [CacheLookup()]
        IWebElement _dobDay = null;

        [FindsBy(How = How.Id, Using = "yy_date_8")]
        [CacheLookup()]
        IWebElement _dobYear = null;

        [FindsBy(How = How.Id, Using = "phone_9")]
        [CacheLookup()]
        IWebElement _phoneNumber = null;

        [FindsBy(How = How.Id, Using = "username")]
        [CacheLookup()]
        IWebElement _userName = null;

        [FindsBy(How = How.Id, Using = "email_1")]
        [CacheLookup()]
        IWebElement _email = null;

        [FindsBy(How = How.Id, Using = "password_2")]
        [CacheLookup()]
        IWebElement _password = null;

        [FindsBy(How = How.Id, Using = "confirm_password_password_2")]
        [CacheLookup()]
        IWebElement _passwordConfirm = null;

        public RegistrationPage(IWebDriver driver)
            : base(driver, "http://demoqa.com/registration")
        {
            // http://relevantcodes.com/pageobjects-and-pagefactory-design-patterns-in-selenium/
            PageFactory.InitElements(_driver, this);
        }

        public string Firstname
        {
            set { SendKeys(_firstName, value, true); }
            get { return GetText(_firstName, true); }
        }

        public string Lastname
        {
            set { SendKeys(_lastName, value, true); }
            get { return GetText(_lastName, true); }
        }

        public string PhoneNumber
        {
            set { SendKeys(_phoneNumber, value, true); }
            get { return GetText(_phoneNumber, true); }
        }

        public string Username
        {
            set { SendKeys(_userName, value, true); }
            get { return GetText(_userName, true); }
        }

        public string Email
        {
            set { SendKeys(_email, value, true); }
            get { return GetText(_email, true); }
        }

        public string Password
        {
            set { SendKeys(_password, value, true); }
            get { return GetText(_password, true); }
        }

        public string PasswordConfirm
        {
            set 
            { 
                SendKeys(_passwordConfirm, value, true);
                SendKeys(_passwordConfirm, Keys.Tab, false);
            }
            get { return GetText(_passwordConfirm, true); }
        }

        public void SetMaritalStatus(eMaritalStatus status)
        {
            if (status != eMaritalStatus.NotSet)
            {
                By by = By.XPath(string.Format("//input[@value='{0}']", status.ToString().ToLower()));
                PerformClick(by);
            }
        }

        public void SetMaritalStatus(string value)
        {
            eMaritalStatus enumValue = eMaritalStatus.NotSet;
            Enum.TryParse<eMaritalStatus>(value, true, out enumValue);
            SetMaritalStatus(enumValue);
        }

        public eMaritalStatus GetMaritalStatus()
        {
            var retVal = eMaritalStatus.NotSet;

            var by = By.XPath("//ul[@id='pie_register']/li[2]//input");
            var element = FindElements(by).FirstOrDefault(ele => ele.Selected);
            if(element != null)
            {
                var text = GetText(element, true);
                retVal = (eMaritalStatus) Enum.Parse(typeof(eMaritalStatus), text, true);
            }

            return retVal; 
        }

        public bool ToggleHobby(string hobby, bool selected)
        {
            if(string.IsNullOrWhiteSpace(hobby)) {
                return true;
            }

            // code will throw if the enum value is invalid
            eHobby enumValue = (eHobby) Enum.Parse(typeof(eHobby), hobby);
            return ToggleHobby(enumValue, selected);
        }

        public bool ToggleHobby(eHobby hobby, bool selected)
        {
            var by = GetHobbyBy(hobby);
            return ToggleCheckBox(by, selected);
        }

        public bool IsHobbySelected(eHobby hobby)
        {
            var by = GetHobbyBy(hobby);
            return IsCheckBoxSelected(by);
        }

        private By GetHobbyBy(eHobby hobby)
        {
            // NOTE: Using "starts-with" match as Cricket has a space at the end 'cricket '

            var xPath =
                string.Format("//ul[@id='pie_register']/li[3]//input[starts-with(@value, '{0}')]",
                    hobby.ToString().ToLower());

            return By.XPath(xPath);
        }

        public void SelectCountry(string country)
        {
            if(!string.IsNullOrWhiteSpace(country))
            {
                SelectHelper(_country).SelectByText(country);
            }
        }

        public void SetDob(DateTime dob)
        {
            SetDob(dob.Month.ToString(), dob.Day.ToString(), dob.Year.ToString());
        }

        public void SetDob(string dob)
        {
            DateTime theDate;
            if(!string.IsNullOrWhiteSpace(dob) && DateTime.TryParse(dob, out theDate))
            {
                SetDob(theDate);
            }
        }

        public void SetDob(string month, string day, string year)
        {
            SelectHelper(_dobMonth).SelectByText(month);
            SelectHelper(_dobDay).SelectByText(day);
            SelectHelper(_dobYear).SelectByText(year);
        }

        public List<string> GetDob()
        {
            string month = SelectHelper(_dobMonth).GetSelectedText();
            string day = SelectHelper(_dobDay).GetSelectedText();
            string year = SelectHelper(_dobYear).GetSelectedText();
            return new List<string> { month, day, year };
        }

        public bool IsDobValid()
        {
            return
                IsDobValueValid(_dobMonth) &&
                IsDobValueValid(_dobDay) &&
                IsDobValueValid(_dobYear);
        }

        private bool IsDobValueValid(IWebElement element)
        {
            int intValue = 0;
            string text = SelectHelper(element).GetSelectedText();

            return int.TryParse(text, out intValue) && intValue >= 1;
        }
        
        public override bool IsLoaded()
        {
            return AreElementsVisible(new List<By> { _h1Tag, _entryDivTag, _submitInputTag });
        }


        internal void ClickSubmit()
        {
            PerformClick(_submitInputTag);
        }

        internal bool IsErrorShown(eErrorArea area)
        {
            var element = GetErrorElement(area, false);
            bool retVal = element.GetAttribute("class").Contains("error");
            return retVal;
        }

        internal string GetErrorMessage(eErrorArea area)
        {
            var element = GetErrorElement(area, true);
            if (area == eErrorArea.Firstname && _driver as InternetExplorerDriver != null)
            {
                // This code is here purely to ensure the element is visible - making IE happy
                MoveToElement(element);
            }
            return GetText(element, true);
        }

        /// <summary>
        /// Returns the Web Element for either the Error Border (DivTag) or the Error Message (SpanTag)
        /// </summary>
        /// <param name="area"></param>
        /// <param name="errorBorder"></param>
        /// <returns></returns>
        private IWebElement GetErrorElement(eErrorArea area, bool errorMessage)
        {
            var basePath = string.Format("//form/ul/li[{0}]/div", _errorDivDict[area]);
            if (errorMessage) {
                basePath += "//span[contains(@class, 'legend error')]";
            }
            var by = By.XPath(basePath);
            return FindElement(by);
        }

        /// <summary>
        /// This message is displayed below the header and above the form once submit is clicked on a valid form fully completed
        /// </summary>
        /// <returns></returns>
        internal string GetHeaderMessage()
        {
            var by = By.XPath("//article/div/p");
            return GetTextIfElementIsVisible(by, Common.Periods.Five);
        }

    }

}
