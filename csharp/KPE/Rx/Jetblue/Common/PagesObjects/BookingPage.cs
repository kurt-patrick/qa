/*
 * Created by Ranorex
 * User: user
 * Date: 24/07/2016
 * Time: 9:04 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using KPE.Rx.Common.PageObject;
using KPE.Rx.Jetblue.Repo;
using Ranorex.Core.Repository;

namespace KPE.Rx.Jetblue.PageObjects
{
	/// <summary>
	/// Description of BookingPage.
	/// </summary>
	public class BookingPage : PageBase
	{
		private static JetblueRepositoryFolders.BookingFolder _baseFolder 
			= JetblueRepository.Instance.JetblueUX.Booking;
		
		public BookingPage()
		{
		}
		
		public CitySelectionPage ClickFrom()
		{
			_baseFolder.DepartFrom.FromImg.Click();
			return new CitySelectionPage();
		}

		public CitySelectionPage ClickTo()
		{
			_baseFolder.ArriveAt.ToImg.Click();
			return new CitySelectionPage();
		}
		
		public string GetFromInputValue()
		{
			return _baseFolder.DepartFrom.FromInput.InnerText;
		}

		public string GetToInputValue()
		{
			return _baseFolder.ArriveAt.ToInput.InnerText;
		}
		
		public override bool IsLoaded()
		{
			var list = new List<RepoItemInfo> { 
				_baseFolder.SelfInfo, _baseFolder.DepartFrom.SelfInfo, _baseFolder.ArriveAt.SelfInfo,
				_baseFolder.DepartFrom.SelfInfo, _baseFolder.ArriveAt.SelfInfo
			};
			return AreElementsVisible(list);
		}
		
	}
}
