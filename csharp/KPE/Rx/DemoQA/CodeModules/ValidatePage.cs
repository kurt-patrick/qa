/*
 * Created by Ranorex
 * User: user
 * Date: 2/08/2016
 * Time: 8:31 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using KPE.Rx.Common.Validation;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace KPE.Rx.DemoQA.PageObjects
{
	/// <summary>
	/// Description of ValidatePage.
	/// </summary>
	[TestModule("8170604D-F406-4B1C-A64B-1F706B5F5FED", ModuleType.UserCode, 1)]
	public class ValidatePage : ITestModule
	{
		#region fields
		RegistrationPage _registrationPage = new RegistrationPage();
		#endregion
		
		#region properties
		[TestVariable("3B2E0EED-6A8C-48A4-A170-6B728021C416")]
		public bool ExpErrUsername { set; get; }
		
		[TestVariable("99D1C374-5E30-418A-BF2F-D03B46771380")]
		public bool ExpErrEmail { set; get; }
		
		[TestVariable("ACEFD4E8-395A-4B93-A439-DBD63AA32644")]
		public string EmailErrMsg { set; get; }
		
		[TestVariable("8E1556BD-813E-43DB-A26A-6B26292808A2")]
		public bool ExpErrFName { set; get; }
		
		[TestVariable("7B59A41C-53AE-457D-BEBF-F4F28A942FF7")]
		public bool ExpErrLName { set; get; }
		
		[TestVariable("12BB262C-C875-4DAF-9A64-D433F94C15E8")]
		public bool ExpErrHobby { set; get; }
		
		[TestVariable("9B9C4CE8-78BF-493D-A743-9832630D5345")]
		public bool ExpErrPhone { set; get; }

		[TestVariable("C52AF7F0-9AC5-488E-9F6A-AD1CDC9F40EC")]
		public string PhoneErrMsg { set; get; }
		
		[TestVariable("F1AAB07A-8F1B-44A8-ADF6-8D89C4E1B919")]
		public bool ExpErrPwd { set; get; }
		
		[TestVariable("c10a6e67-2c38-4cc9-b23f-88b323db49b6")]
		public string PwdErrMsg { set; get; }
		
		[TestVariable("6B0262BE-CA61-43D3-A1A9-AEFE37BD2EFF")]
		public string PwdErrMsg2 { set; get; }
		
		[TestVariable("258B51A9-579C-4C8C-A6B3-1645C801F20A")]
		public bool ExpErrPwdC { set; get; }

		[TestVariable("66AFE41C-D94B-40A9-810E-45CAB8521C52")]
		public string PwdCErrMsg { set; get; }
		
		[TestVariable("6EB7B05C-3E29-45F5-BC27-C1B5507FD1EC")]
		public string GenericErrMsg { set; get; }
		
		[TestVariable("80FE26BD-1FE1-49F7-B165-BAF4CDA3067D")]
		public string ExpHeaderMessage { set; get; }
		#endregion
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public ValidatePage()
		{
			// Do not delete - a parameterless constructor is required!
		}

		/// <summary>
		/// Performs the playback of actions in this module.
		/// </summary>
		/// <remarks>You should not call this method directly, instead pass the module
		/// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
		/// that will in turn invoke this method.</remarks>
		void ITestModule.Run()
		{
			Mouse.DefaultMoveTime = 1;
			Keyboard.DefaultKeyPressTime = 1;
			Delay.SpeedFactor = 1.0;
			
			// Validate
			ValidateElementErrorStates();

			// Validate against possible states - success, username exists, etc ...
			ValidateHeaderMessage();
		}
		
        private void ValidateHeaderMessage()
        {
        	if (!string.IsNullOrWhiteSpace(ExpHeaderMessage))
            {
                _registrationPage.ClickSubmit();
                var actual =_registrationPage.GetHeaderMessage();

                string errMsg = string.Format("Header message ({0}) contains ({1})", actual, ExpHeaderMessage);
                Verify.That.IsTrue(actual.IndexOf(ExpHeaderMessage, StringComparison.CurrentCultureIgnoreCase) >= 0, errMsg);

            }
        }
        
        private void ValidateElementErrorStates()
        {
            bool expNameError = ExpErrFName || ExpErrLName;
            ValidateArea(RegistrationPage.eErrorArea.Firstname, expNameError);
            ValidateArea(RegistrationPage.eErrorArea.Lastname, expNameError);
            ValidateArea(RegistrationPage.eErrorArea.Phone, ExpErrPhone);
            ValidateArea(RegistrationPage.eErrorArea.Username, ExpErrUsername);
            ValidateArea(RegistrationPage.eErrorArea.Email, ExpErrEmail);
            ValidateArea(RegistrationPage.eErrorArea.Pwd, ExpErrPwd);
            ValidateArea(RegistrationPage.eErrorArea.PwdConfirm, ExpErrPwdC);
        }

        private void ValidateArea(RegistrationPage.eErrorArea area, bool expected)
        {
            // Check for the error border
            var errMsg = string.Format("{0} is in {1} state", area, ((expected) ? "an error" : "a valid"));
            bool actual = _registrationPage.IsErrorShown(area);
            Verify.That.AreEqual(expected, actual, errMsg);

            // Check the error message
            if (expected && actual)
            {
                string expectedMsg = GetExpectedErrorMessage(area);
                string actualMsg = _registrationPage.GetErrorMessage(area);

                errMsg = string.Format("({0}) error message. Expected ({1}) Actual ({2})", area, expectedMsg, actualMsg);
                Verify.That.AreEqual(actualMsg, expectedMsg, errMsg);
            }
        }

        private string GetExpectedErrorMessage(RegistrationPage.eErrorArea area)
        {
            string genericErrMsg = GenericErrMsg;
            string specificErrMsg = string.Empty;
            switch (area) {
            	case RegistrationPage.eErrorArea.Phone:
            		specificErrMsg = PhoneErrMsg; 
            		break;
            	case RegistrationPage.eErrorArea.Email:
            		specificErrMsg = EmailErrMsg; 
            		break;
            	case RegistrationPage.eErrorArea.Pwd:
            		specificErrMsg = PwdErrMsg; 
            		specificErrMsg = PwdErrMsg2; 
            		break;
            	case RegistrationPage.eErrorArea.PwdConfirm:
            		specificErrMsg = PwdCErrMsg; 
            		break;
        		case RegistrationPage.eErrorArea.Firstname:
            	case RegistrationPage.eErrorArea.Lastname:
            	case RegistrationPage.eErrorArea.Hobby:
            	case RegistrationPage.eErrorArea.Username:
            		break;
            	default:
            		throw new Exception("Invalid value for eErrorArea: " +area.ToString());
            }
            
            return string.IsNullOrWhiteSpace(specificErrMsg) ? genericErrMsg.Trim() : specificErrMsg.Trim();
        }        
		
	}
}
