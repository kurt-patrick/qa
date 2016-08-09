/*
 * Created by Ranorex
 * User: user
 * Date: 9/08/2016
 * Time: 8:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Ranorex;
using Ranorex.Core.Repository;

namespace KPE.Rx.Common.Helper
{
	/// <summary>
	/// Description of ThrowHelper.
	/// </summary>
	public static class ThrowHelper
	{
		public static void ThrowElementNotFoundException(RepoItemInfo itemInfo)
		{
			throw new ElementNotFoundException(itemInfo.Path);
		}
		
		public static void ThrowElementNotFoundExceptionIfNull(RepoItemInfo itemInfo, object value)
		{
			if(value == null) {
				ThrowElementNotFoundException(itemInfo);
			}
		}
		
		public static void ThrowElementNotFoundExceptionIfFalse(RepoItemInfo itemInfo, bool value)
		{
			if(!value) {
				ThrowElementNotFoundException(itemInfo);
			}
		}
	}
}
