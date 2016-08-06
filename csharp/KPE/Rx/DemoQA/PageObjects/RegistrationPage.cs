/*
 * Created by Ranorex
 * User: user
 * Date: 2/08/2016
 * Time: 8:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KPE.Rx.Common;
using KPE.Rx.Common.Helper;
using Ranorex;
using Ranorex.Core.Repository;

namespace KPE.Rx.DemoQA.PageObjects
{
	/// <summary>
	/// Description of RegistrationPage.
	/// </summary>
	public class RegistrationPage : KPE.Rx.Common.PageObject.PageBase
	{
		#region enum
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
		#endregion

		#region fields
		private static DemoQA.Repo.DemoQARepository _repo = DemoQA.Repo.DemoQARepository.Instance;
		private static DemoQA.Repo.DemoQARepositoryFolders.DemoQAAppFolder _baseFolder = DemoQA.Repo.DemoQARepository.Instance.DemoQA;
		
		private Dictionary<eErrorArea, int> _errorDivDict = new Dictionary<eErrorArea, int>
		{
			{ eErrorArea.Firstname, 1 }, { eErrorArea.Lastname, 1 }, { eErrorArea.Hobby, 3 },
			{ eErrorArea.Phone, 6 }, { eErrorArea.Username, 7 }, { eErrorArea.Email, 8 },
			{ eErrorArea.Pwd, 11 }, { eErrorArea.PwdConfirm, 12 }
		};

		//By _h1Tag = By.TagName("h1");
		//By _entryDivTag = By.ClassName("entry-content");
		//By _submitInputTag = By.Name("pie_submit");

//		[FindsBy(How = How.Id, Using = "dropdown_7")]
//		[CacheLookup()]
//		IWebElement _country = null;
//
//		[FindsBy(How = How.Id, Using = "mm_date_8")]
//		[CacheLookup()]
//		IWebElement _dobMonth = null;
//
//		[FindsBy(How = How.Id, Using = "dd_date_8")]
//		[CacheLookup()]
//		IWebElement _dobDay = null;
//
//		[FindsBy(How = How.Id, Using = "yy_date_8")]
//		[CacheLookup()]
//		IWebElement _dobYear = null;
//
//		[FindsBy(How = How.Id, Using = "phone_9")]
//		[CacheLookup()]
//		IWebElement _phoneNumber = null;
//
//		[FindsBy(How = How.Id, Using = "username")]
//		[CacheLookup()]
//		IWebElement _userName = null;
//
//		[FindsBy(How = How.Id, Using = "email_1")]
//		[CacheLookup()]
//		IWebElement _email = null;
//
//		[FindsBy(How = How.Id, Using = "password_2")]
//		[CacheLookup()]
//		IWebElement _password = null;
//
//		[FindsBy(How = How.Id, Using = "confirm_password_password_2")]
//		[CacheLookup()]
//		IWebElement _passwordConfirm = null;
		#endregion

		#region constructors
		public RegistrationPage() : base()
		{
		}
		#endregion

		#region properties
		public string Firstname
		{
			set { PressKeys(_baseFolder.Firstname, value); }
			get { return GetText(_baseFolder.Firstname); }
		}

		public string Lastname
		{
			set { PressKeys(_baseFolder.Lastname, value); }
			get { return GetText(_baseFolder.Lastname); }
		}

		public string PhoneNumber
		{
			set { PressKeys(_baseFolder.PhoneNumber, value); }
			get { return GetText(_baseFolder.PhoneNumber); }
		}

		public string Username
		{
			set { PressKeys(_baseFolder.Username, value); }
			get { return GetText(_baseFolder.Username); }
		}

		public string Email
		{
			set { PressKeys(_baseFolder.Email, value); }
			get { return GetText(_baseFolder.Email); }
		}

		public string Password
		{
			set { PressKeys(_baseFolder.Password, value); }
			get { return GetText(_baseFolder.Password); }
		}

		public string PasswordConfirm
		{
			set
			{
				var element = _baseFolder.PasswordConfirm;
				PressKeys(element, value);
				KeyboardHelper.Tab();
			}
			get { return GetText(_baseFolder.PasswordConfirm); }
		}
		#endregion

		#region methods
		public void SetMaritalStatus(eMaritalStatus status)
		{
			if (status != eMaritalStatus.NotSet)
			{
				_repo.GenericKey = status.ToString().ToLower();
				WebElement element = _repo.DemoQA.MaritalStatus.SetMaritalStatus;
				PerformClick(element);
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
			WebElement element = _repo.DemoQA.Self.Find<InputTag>(_repo.DemoQA.MaritalStatus.AllMaritalStatusesInfo.Path).FirstOrDefault(ele => ele.Checked == "true");
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
			var element = GetHobbyElement(hobby);
			return ToggleCheckBox(element, selected);
		}

		public bool IsHobbySelected(eHobby hobby)
		{
			var by = GetHobbyElement(hobby);
			return IsCheckBoxSelected(by);
		}

		private InputTag GetHobbyElement(eHobby hobby)
		{
			_repo.GenericKey = hobby.ToString().ToLower(); 
			return _baseFolder.Hobby.SingleHobby;
		}

		public void SelectCountry(string country)
		{
			if(!string.IsNullOrWhiteSpace(country))
			{
				SelectHelper.Create(_baseFolder.Country).SelectByValue(country);
			}
		}

		public override bool IsLoaded()
		{
			return DoElementsExist(new List<RepoItemInfo> { _baseFolder.PageHeadingInfo, _baseFolder.MainContainerInfo, _baseFolder.SubmitInfo });
		}

		internal void ClickSubmit()
		{
			PerformClick(_baseFolder.Submit);
		}

		internal bool IsErrorShown(eErrorArea area)
		{
			var element = GetErrorElement(area, false);
			bool retVal = ElementHelper.DoesAttributeContain(element, "class", "error");
			return retVal;
		}

		internal string GetErrorMessage(eErrorArea area)
		{
			var element = GetErrorElement(area, true);
			element.MoveTo();
//			if (area == eErrorArea.Firstname && _driver as InternetExplorerDriver != null)
//			{
//				// This code is here purely to ensure the element is visible - making IE happy
//				MoveToElement(element);
//			}
			return GetText(element, true);
		}

		/// <summary>
		/// Returns the Web Element for either the Error Border (DivTag) or the Error Message (SpanTag)
		/// </summary>
		/// <param name="area"></param>
		/// <param name="errorBorder"></param>
		/// <returns></returns>
		private WebElement GetErrorElement(eErrorArea area, bool errorMessage)
		{
			_repo.GenericIndex = _errorDivDict[area].ToString();
			if(errorMessage) 
			{
				return _baseFolder.Error.ErrorMessage;
			} 
			else 
			{
				return _baseFolder.Error.ErrorDiv;
			}
		}

		/// <summary>
		/// This message is displayed below the header and above the form once submit is clicked on a valid form fully completed
		/// </summary>
		/// <returns></returns>
		internal string GetHeaderMessage()
		{
			return GetTextIfElementIsVisible(_baseFolder.HeaderMessageInfo, TimeOuts.Five);
		}
		#endregion
	}
}
