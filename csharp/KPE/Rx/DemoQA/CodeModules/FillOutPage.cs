/*
 * Created by Ranorex
 * User: user
 * Date: 2/08/2016
 * Time: 8:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using KPE.Rx.Common;
using KPE.Rx.Common.Helper;
using KPE.Rx.Common.Validation;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace KPE.Rx.DemoQA.PageObjects
{
	/// <summary>
	/// Description of FillOutPage.
	/// </summary>
	[TestModule("D99F250E-B3ED-4CE7-892A-367E0E5919B9", ModuleType.UserCode, 1)]
	public class FillOutPage : ITestModule
	{
		#region fields
		private string _guid = System.Guid.NewGuid().ToString();
		private static string _lastGuid = null;
		RegistrationPage _registrationPage = new RegistrationPage();
		#endregion
		
		#region properties
		private string _username = "";
		[TestVariable("D8963E3F-4BE5-4CF3-8211-60E65802BFC0")]
		public string Username 
		{
			set { _username = ReplaceGuid(value); }
			get { return _username; } 
		}
		
		private string _email = "";
		[TestVariable("37F0F958-A37D-45E9-AB0E-D2ECAEF30C7C")]
		public string Email 
		{ 
			set { _email = ReplaceGuid(value); }
			get { return _email; }
		}
		
		[TestVariable("E0F4B0E8-DDAA-4450-9A77-CDA6DEFB4D35")]
		public string Firstname { set; get; }
		
		[TestVariable("FB87B283-F389-4AB4-9F7E-50275623D047")]
		public string Lastname { set; get; }
		
//		[TestVariable("A002BE67-1D3F-4D98-AD62-FD857F0CD87C")]
//		public string MaritalStatus { set; get; }
		
		[TestVariable("FEB89C23-0ADB-4170-894D-D3BA80D66890")]
		public string Hobby { set; get; }
		
//		[TestVariable("610BBF06-C34F-43A5-979C-6C88679885D6")]
//		public string Country { set; get; }
		
//		[TestVariable("41C54E99-843A-4AD5-9E09-4FAB027C0F2A")]
//		public string Dob { set; get; }
		
		[TestVariable("B5B3AC62-7A73-41FD-82C4-9DE88AC47967")]
		public string PhoneNumber { set; get; }
		
		[TestVariable("194B9877-8C38-4D49-B466-A050DD29DBCD")]
		public string Password { set; get; }

		[TestVariable("1C0EB0BD-5008-4D2F-B804-3A53DC00641C")]
		public string PasswordConfirm { set; get; }
		
		[TestVariable("3E49B1B5-EAA0-4F2A-BC1B-60311687CE1D")]
		public bool ExpErrPasswordConfirm { set; get; }
		#endregion
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public FillOutPage()
		{
			string a = "1";
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
			
			Assert.That.IsTrue(_registrationPage.IsLoaded(), "Registration page failed to load");

			// set values
			SetPageValues();

			// click Submit
			SubmitForm();
			
			_lastGuid = _guid.ToString();
			_guid = System.Guid.NewGuid().ToString();

		}
		
		private string ReplaceGuid(string value)
		{
			var retVal = value.Replace("{guid}", _guid);
			if(!string.IsNullOrWhiteSpace(_lastGuid)) {
				retVal = retVal.Replace("{last-guid}", _lastGuid);
			}
			return retVal;
		}

		private void SubmitForm()
		{
			// A pause is needed here while the screen is filled out
			//bool expErrPwdC = !string.Equals(Password, PasswordConfirm);
			Func<bool> condition = () => _registrationPage.IsErrorShown(RegistrationPage.eErrorArea.PwdConfirm) == ExpErrPasswordConfirm;

			WaitHelper.TryWaitForCondition(condition, TimeOuts.Five);

			// Click submit
			_registrationPage.ClickSubmit();

			// Wait for the page to be loaded
			Assert.That.IsTrue(_registrationPage.IsLoaded(), "Registration page failed to load");

		}

		private void SetPageValues()
		{
			_registrationPage.Firstname = Firstname;
			_registrationPage.Lastname = Lastname;
			_registrationPage.ToggleHobby(Hobby, true);
			_registrationPage.PhoneNumber = PhoneNumber;
			_registrationPage.Username = Username;
			_registrationPage.Email = Email;
			_registrationPage.Password = Password;

			// NOTE: Setting at least 1 field so the Tab press below stays happy
			_registrationPage.PasswordConfirm = PasswordConfirm;

		}

	}
}
