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
		RegistrationPage _registrationPage = new RegistrationPage();
		#endregion
		
		#region properties
		[TestVariable("D8963E3F-4BE5-4CF3-8211-60E65802BFC0")]
		public string Username { set; get; }
		
		[TestVariable("37F0F958-A37D-45E9-AB0E-D2ECAEF30C7C")]
		public string Email { set; get; }
		
		[TestVariable("E0F4B0E8-DDAA-4450-9A77-CDA6DEFB4D35")]
		public string Firstname { set; get; }
		
		[TestVariable("FB87B283-F389-4AB4-9F7E-50275623D047")]
		public string Lastname { set; get; }
		
		[TestVariable("A002BE67-1D3F-4D98-AD62-FD857F0CD87C")]
		public string MaritalStatus { set; get; }
		
		[TestVariable("FEB89C23-0ADB-4170-894D-D3BA80D66890")]
		public string Hobby { set; get; }
		
		[TestVariable("610BBF06-C34F-43A5-979C-6C88679885D6")]
		public string Country { set; get; }
		
		[TestVariable("41C54E99-843A-4AD5-9E09-4FAB027C0F2A")]
		public string Dob { set; get; }
		
		[TestVariable("B5B3AC62-7A73-41FD-82C4-9DE88AC47967")]
		public string PhoneNumber { set; get; }
		
		[TestVariable("194B9877-8C38-4D49-B466-A050DD29DBCD")]
		public string Password { set; get; }

		[TestVariable("1C0EB0BD-5008-4D2F-B804-3A53DC00641C")]
		public string PasswordConfirm { set; get; }
		#endregion
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public FillOutPage()
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
			Mouse.DefaultMoveTime = 300;
			Keyboard.DefaultKeyPressTime = 100;
			Delay.SpeedFactor = 1.0;
			
			Validate.AreEqual(_registrationPage.IsLoaded(), true, "Registration page failed to load");

//			// Use the previuos record
//			if(Custom_UsernameExists.Equals(dsRow.Custom))
//			{
//				dsRow = _lastDsRow;
//				dsRow.Custom = Custom_UsernameExists;
//			}

			// set values
			SetPageValues();

			// click Submit
			SubmitForm();

		}

		private void SubmitForm()
		{
			// A pause is needed here while the screen is filled out
			bool expErrPwdC = !string.Equals(Password, PasswordConfirm);
			Func<bool> condition = () => _registrationPage.IsErrorShown(RegistrationPage.eErrorArea.PwdConfirm) == expErrPwdC;

			WaitHelper.TryWaitForCondition(condition, TimeOuts.Five);

			// Click submit
			_registrationPage.ClickSubmit();

			// Wait for the page to be loaded
			Validate.AreEqual(_registrationPage.IsLoaded(), true, "Registration page failed to load");

		}

		private void SetPageValues()
		{
			_registrationPage.Firstname = Firstname;
			_registrationPage.Lastname = Lastname;
			_registrationPage.SetMaritalStatus(MaritalStatus);
			_registrationPage.ToggleHobby(Hobby, true);
			_registrationPage.SelectCountry(Country);
			//_registrationPage.SetDob(dsRow.Dob);
			_registrationPage.PhoneNumber = PhoneNumber;
			_registrationPage.Username = Username;
			_registrationPage.Email = Email;
			_registrationPage.Password = Password;

			// NOTE: Setting at least 1 field so the Tab press below stays happy
			_registrationPage.PasswordConfirm = PasswordConfirm;

		}

	}
}
