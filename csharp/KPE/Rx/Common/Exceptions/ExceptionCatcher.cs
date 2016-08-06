/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 2:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KPE.Rx.Common.Exceptions
{
	/// <summary>
	/// Description of ExceptionCatcher.
	/// </summary>
	public static class ExceptionCatcher
	{
		/// <summary>
		/// Calls the provided method within a try catch block
		/// </summary>
		/// <param name="condition"></param>
		/// <returns>true if no exception else false</returns>
		public static bool TryCallMethod(Action condition)
		{
			try {
				condition();
				return true;
			} catch (Exception) {
				return false;
			}
		}
		
	}
}
