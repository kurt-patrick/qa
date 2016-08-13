using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using KPE.Se.Common.Helpers;
using KPE.Se.DemoQA.PageObjects;
using KPE.Se.Common;

namespace KPE.Se.DemoQA
{
    public class RegistrationTests2 : TestFixtureBase
    {
        private const string Custom_UsernameExists = "username-exists";
        private const string Custom_EmailExists = "email-exists";
        private const string Custom_Success = "success";
        private const string Err_Msg_Field_Rq = "* This field is required";
        private const string Err_Msg_Email_Invalid = "* Invalid email address";
        private const string Err_Msg_Phone_Min_10_Digits_Rq = "* Minimum 10 Digits starting with Country Code";
        private const string Err_Msg_Pwd_Fields_Dont_Match = "* Fields do not match";
        private const string Err_Msg_Pwd_Min_8_Digits_Rq = "* Minimum 8 characters required";

        /// <summary>
        /// Used for debugging
        /// </summary>
        private int _rowIndex = 1;

        /// <summary>
        /// The page object we are using for tests
        /// </summary>
        private RegistrationPage _registrationPage = null;

        /// <summary>
        /// Used to test against saving with the same username
        /// </summary>
        private static RegistrationDs _lastDsRow = null;

        /// <summary>
        /// Constructor that sets up the test fixture with the browsers to test against
        /// </summary>
        /// <param name="config"></param>
        public RegistrationTests2(TestFixtureConfig config)
            : base(config)
        {
        }

        /// <summary>
        /// Called before the test is run
        /// </summary>
        public override void TestSetup()
        {
            _registrationPage = new PageObjects.RegistrationPage(_driver);
        }

        [Test]
        [TestCaseSource("RegistrationTCS")]
        public void FunctionalTests(RegistrationDs dsRow)
        {
            Assert.NotNull(dsRow, "Registration dataset object is null");

            // for debugging - log the current row
            LogToConsole("Row Index: " + _rowIndex);
            LogToConsole("Browser: " + _testFixtureConfig.BrowserName);

            // Load the page
            _registrationPage.NavigateTo();
            Assert.IsTrue(_registrationPage.IsLoaded(), "Registration page failed to load");

            // Use the previuos record
            if (Custom_UsernameExists.Equals(dsRow.Custom))
            {
                dsRow = _lastDsRow;
                dsRow.Custom = Custom_UsernameExists;
            }

            // set values
            SetPageValues(dsRow);

            // click Submit
            SubmitForm(dsRow);

            // Validate
            ValidateElementErrorStates(dsRow);

            // Validate against possible states - success, username exists, etc ...
            CustomLogic(dsRow);

            // for debugging - increment the current row
            _rowIndex += 1;
            _lastDsRow = dsRow;

        }

        private void CustomLogic(RegistrationDs dsRow)
        {
            var dict = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
            {
                { Custom_UsernameExists, "Username already exists" }, { Custom_Success, "Thank you for your registration" },
                { Custom_EmailExists, "E-mail address already exists" }
            };

            if (dict.ContainsKey(dsRow.Custom))
            {
                _registrationPage.ClickSubmit();
                var actual = _registrationPage.GetHeaderMessage();

                string expected = dict[dsRow.Custom];
                string errMsg = string.Format("Header message ({0}) does not contain ({1})", actual, expected);
                Assert.IsTrue(actual.IndexOf(expected, StringComparison.CurrentCultureIgnoreCase) >= 0, errMsg);

            }
        }

        private void SubmitForm(RegistrationDs dsRow)
        {
            // A pause is needed here while the screen is filled out
            Func<bool> condition = () => _registrationPage.IsErrorShown(RegistrationPage.eErrorArea.PwdConfirm) == dsRow.ExpErrPwdC;

            WaitHelper.TryWaitForCondition(condition, Periods.Five);

            // Click submit
            _registrationPage.ClickSubmit();

            // Wait for the page to be loaded
            Assert.IsTrue(_registrationPage.IsLoaded(), "Registration page failed to load");

        }

        private void SetPageValues(RegistrationDs dsRow)
        {
            // NOTE: The if check is purely to try and reduce the time the tests take to run by not settings values that dont need to be set
            if (!string.IsNullOrWhiteSpace(dsRow.Firstname)) { _registrationPage.Firstname = dsRow.Firstname; }
            if (!string.IsNullOrWhiteSpace(dsRow.Lastname)) { _registrationPage.Lastname = dsRow.Lastname; }
            if (!string.IsNullOrWhiteSpace(dsRow.MaritalStatus)) { _registrationPage.SetMaritalStatus(dsRow.MaritalStatus); }
            if (!string.IsNullOrWhiteSpace(dsRow.Hobby)) { _registrationPage.ToggleHobby(dsRow.Hobby, true); }
            if (!string.IsNullOrWhiteSpace(dsRow.Country)) { _registrationPage.SelectCountry(dsRow.Country); }
            if (!string.IsNullOrWhiteSpace(dsRow.Dob)) { _registrationPage.SetDob(dsRow.Dob); }
            if (!string.IsNullOrWhiteSpace(dsRow.PhoneNumber)) { _registrationPage.PhoneNumber = dsRow.PhoneNumber; }
            if (!string.IsNullOrWhiteSpace(dsRow.Username)) { _registrationPage.Username = dsRow.Username; }
            if (!string.IsNullOrWhiteSpace(dsRow.Email)) { _registrationPage.Email = dsRow.Email; }
            if (!string.IsNullOrWhiteSpace(dsRow.Password)) { _registrationPage.Password = dsRow.Password; }

            // NOTE: Setting at least 1 field so the Tab press below stays happy
            _registrationPage.PasswordConfirm = dsRow.PasswordConfirm;

        }

        private void ValidateElementErrorStates(RegistrationDs dsRow)
        {
            bool expNameError = dsRow.ExpErrFName || dsRow.ExpErrLName;
            ValidateArea(RegistrationPage.eErrorArea.Firstname, expNameError, dsRow.Firstname);
            ValidateArea(RegistrationPage.eErrorArea.Lastname, expNameError, dsRow.Lastname);
            ValidateArea(RegistrationPage.eErrorArea.Hobby, dsRow.ExpErrHobby, dsRow.Hobby);
            ValidateArea(RegistrationPage.eErrorArea.Phone, dsRow.ExpErrPhone, dsRow.PhoneNumber);
            ValidateArea(RegistrationPage.eErrorArea.Username, dsRow.ExpErrUsername, dsRow.Username);
            ValidateArea(RegistrationPage.eErrorArea.Email, dsRow.ExpErrEmail, dsRow.Email);
            ValidateArea(RegistrationPage.eErrorArea.Pwd, dsRow.ExpErrPwd, dsRow.Password);
            ValidateArea(RegistrationPage.eErrorArea.PwdConfirm, dsRow.ExpErrPwdC, dsRow.PasswordConfirm, dsRow.Password);
        }

        private void ValidateArea(RegistrationPage.eErrorArea area, bool expected, string value, string value2 = null)
        {
            // Check for the error border
            var errMsg = string.Format("{0} {1} be in an error state. Value: ({2})", area, ((expected) ? "should" : "should NOT"), value ?? string.Empty);
            bool actual = _registrationPage.IsErrorShown(area);
            Assert.AreEqual(expected, actual, errMsg);

            // Check the error message
            if (expected && actual)
            {
                string actualMsg = _registrationPage.GetErrorMessage(area);
                string expectedMsg = GetExpectedErrorMessage(area, value, value2);

                errMsg = string.Format("Error message is not as expected ({0}) Actual ({1}) for area ({2}) against value ({3})", expectedMsg, actualMsg, area, value ?? string.Empty);
                Assert.AreEqual(expectedMsg, actualMsg, errMsg);
            }
        }

        private string GetExpectedErrorMessage(RegistrationPage.eErrorArea area, string value, string value2 = null)
        {
            string actualMsg = _registrationPage.GetErrorMessage(area);
            string expectedMsg = Err_Msg_Field_Rq;
            if (value != null && value.Length > 0)
            {
                switch (area)
                {
                    case RegistrationPage.eErrorArea.Email:
                        expectedMsg = Err_Msg_Email_Invalid;
                        break;
                    case RegistrationPage.eErrorArea.Phone:
                        long tmp = 0;
                        if (value.Length < 10 || value.Length > 16 || !Int64.TryParse(value, out tmp))
                        {
                            expectedMsg = Err_Msg_Phone_Min_10_Digits_Rq;
                        }
                        break;
                    case RegistrationPage.eErrorArea.Pwd:
                        if (value.Length < 8)
                        {
                            expectedMsg = Err_Msg_Pwd_Min_8_Digits_Rq;
                        }
                        break;
                    case RegistrationPage.eErrorArea.PwdConfirm:
                        if (!string.Equals(value, value2))
                        {
                            expectedMsg = Err_Msg_Pwd_Fields_Dont_Match;
                        }
                        break;
                    default:
                        break;
                }
            }
            return expectedMsg;
        }

        public override void TearDown()
        {
            _registrationPage = null;
        }

        /// <summary>
        /// CSV test dataset used to for functional tests
        /// </summary>
        /// <returns></returns>
        internal static List<RegistrationDs> RegistrationTCS()
        {
            return DataSetHelper.LoadFromCsv<RegistrationDs>(@"Tests\Functional\Registration\Datasets\Registration2.csv");
        }

        /// <summary>
        /// Dataset object linked to the above CSV for testing
        /// </summary>
        public class RegistrationDs
        {
            private string _username = string.Empty;
            public string Username
            {
                get { return _username; }
                set { _username = ReplaceGuid(value); }
            }

            private string _email = string.Empty;
            public string Email
            {
                get { return _email; }
                set { _email = ReplaceGuid(value); }
            }

            public string Firstname { set; get; }
            public string Lastname { set; get; }
            public string MaritalStatus { set; get; }
            public string Hobby { set; get; }
            public string Country { set; get; }
            public string Dob { set; get; }
            public string PhoneNumber { set; get; }
            public string Password { set; get; }
            public string PasswordConfirm { set; get; }
            public string Custom { set; get; }
            public bool ExpErrFName { set; get; }
            public bool ExpErrLName { set; get; }
            public bool ExpErrHobby { set; get; }
            public bool ExpErrPhone { set; get; }
            public bool ExpErrUsername { set; get; }
            public bool ExpErrEmail { set; get; }
            public bool ExpErrPwd { set; get; }
            public bool ExpErrPwdC { set; get; }
            private string ReplaceGuid(string value)
            {
                if (value != null)
                {
                    return value.Replace("{guid}", System.Guid.NewGuid().ToString());
                }
                return value;
            }
        }


    }
}
