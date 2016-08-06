/*
 * Created by Ranorex
 * User: user
 * Date: 24/07/2016
 * Time: 9:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using KPE.Rx.Common;
using KPE.Rx.Common.Exceptions;
using Ranorex;
using Ranorex.Core.Repository;

namespace KPE.Rx.Jetblue.PageObjects
{
	/// <summary>
	/// Description of CitySelectionPage.
	/// </summary>
	public class CitySelectionPage : JetbluePageBase
	{
		private Jetblue.Repo.JetblueRepositoryFolders.SelectionModalFolder _baseFolder = _repository.JetblueUX.Booking.SelectionModal;
		
		public CitySelectionPage()
		{
		}
		
		public string PickRandomCity()
		{
			ATag airportTag = null;
			var chars = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };
			var parentFolder = _baseFolder.Airport.Self;
			foreach(var startsWith in chars)
			{
				_repository.QueryString = startsWith;
				airportTag = parentFolder.Find<ATag>(_baseFolder.Airport.AirportStartsWithInfo.Path, TimeSpans.DefaultTimeOut).FirstOrDefault(ele => ele.Visible);
				if(airportTag != null)
				{
					return airportTag.InnerText;
				}
			}
			
			throw new InvalidStateException("Failed to select a city");
			
		}
		
		public override bool IsLoaded()
		{
			var list = new List<RepoItemInfo> { _baseFolder.SelfInfo, _baseFolder.Region.SelfInfo, _baseFolder.Airport.SelfInfo };
			return AreElementsVisible(list);
		}
		
		public bool IsClosed()
		{
			return DoesElementNotExist(_baseFolder.SelfInfo);
		}
		
	}
}
