/*
 * Created by Ranorex
 * User: user
 * Date: 17/08/2016
 * Time: 7:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using KPE.Rx.Common;
using KPE.Rx.Common.Helper;
using Ranorex;
using Ranorex.Core.Repository;

namespace KPE.Rx.Common
{
	/// <summary>
	/// Description of TryClickAndValidateSettings.
	/// </summary>
	public class TryClickAndValidateSettings
	{
		public bool ElementMustExist { get; set; }
		public int ElementTimeOut { get; set; }
		public int ConditionTimeOut { get; set; }
		
		public static TryClickAndValidateSettings DefaultSettings = new TryClickAndValidateSettings();
		
		private TryClickAndValidateSettings()
		{
			ElementMustExist = true;
			ElementTimeOut = TimeOuts.TimeOutDefault;
			ConditionTimeOut = TimeOuts.TimeOutDefault;
		}
		
	}
}
