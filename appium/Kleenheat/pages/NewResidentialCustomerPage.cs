using au.com.kleenheat.se.common;
using au.com.kleenheat.se.helpers;
using au.com.kleenheat.se.qa;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se.pages
{
    public class NewResidentialCustomerPage : PageBase
    {
        // Contact Details
        private By _title = By.Id("input_2_1_2");
        private By _first = By.Id("input_2_1_3");
        private By _last = By.Id("input_2_1_6");
        private By _phoneNumber = By.Id("input_2_15");
        private By _emailAddress = By.Id("input_2_14");
        private By _dateOfBirth = By.Id("input_2_54");

        // Cyclinder Details
        private By _deliveryStreet = By.Id("input_2_4_1");
        private By _deliveryCity = By.Id("input_2_4_3");
        private By _deliveryPostCode = By.Id("input_2_4_5");
        private By _deliveryState = By.Id("input_2_51");
        private By _cyclindersRequired = By.Id("input_2_47");
        private By _hasKleenheatGasCyclinders_Yes = By.Id("choice_2_29_0");
        private By _hasKleenheatGasCyclinders_No = By.Id("choice_2_29_1");

        // Terms and conditions
        private By _agreeToTermsAndConditions = By.Id("choice_2_23_1");
        private By _acknowledge = By.Id("choice_2_21_1");
        private By _submit = By.Id("gform_submit_button_2");

        // Error message
        private By _errorDivTag = By.ClassName("validation_error");

        public NewResidentialCustomerPage(TestCaseSettings settings)
            : base(settings)
        {

        }

        public override bool IsLoaded()
        {
            return Exists(_title, _first, _last);
        }

        public void EnterContactDetails(string title, string first, string last, string phoneNumber, string emailAddress, string dob)
        {
            SelectTitle(title);
            EnterFirst(first);
            EnterLast(last);
            EnterPhoneNumber(phoneNumber);
            EnterEmailAddress(emailAddress);
            EnterDateOfBirth(dob);
        }

        public void EnterCyclinderDetails(string street, string city, string postCode, string state, string cyclRq, bool hasExistingCyclinders)
        {
            SendKeys(_deliveryStreet, street);
            SendKeys(_deliveryCity, city);
            SendKeys(_deliveryPostCode, postCode);
            SelectHelper(_deliveryState).SelectByText(state);
            SelectHelper(_cyclindersRequired).SelectByText(cyclRq);
            Click(hasExistingCyclinders ? _hasKleenheatGasCyclinders_Yes : _hasKleenheatGasCyclinders_No);
        }

        public void EnterTermsAndConditions(bool agree, bool acknowledge)
        {
            ToggleCheckBox(_agreeToTermsAndConditions, agree);
            ToggleCheckBox(_acknowledge, acknowledge);
        }

        internal void SelectTitle(string text)
        {
            SelectHelper(_title).SelectByText(text);
        }

        internal void EnterFirst(string text)
        {
            SendKeys(_first, text);
        }

        internal void EnterLast(string text)
        {
            SendKeys(_last, text);
        }

        internal void EnterPhoneNumber(string text)
        {
            SendKeys(_phoneNumber, text);
        }

        internal void EnterEmailAddress(string text)
        {
            SendKeys(_emailAddress, text);
        }

        /// <summary>
        /// DOB must be 8 chars in length
        /// </summary>
        /// <param name="text"></param>
        internal void EnterDateOfBirth(string text)
        {
            var element = FindElement(_dateOfBirth);
            SendKeys(element, text.Substring(0, 2), false);
            SendKeys(element, text.Substring(2, 2), false);
            SendKeys(element, text.Substring(4, 4), false);
        }

        internal void ClickSubmit()
        {
            Click(_submit);
        }

        internal string GetErrorMessage()
        {
            return GetText(_errorDivTag);
        }
    }
}
