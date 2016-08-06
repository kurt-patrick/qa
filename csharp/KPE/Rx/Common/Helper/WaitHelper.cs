/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 2:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KPE.Rx.Common.Helper
{
	/// <summary>
	/// Description of WaitHelper.
	/// </summary>
	public static class WaitHelper
	{
		public static bool TryWaitForCondition(Func<bool> condition, int timeOut = Common.TimeOuts.TimeOutDefault)
		{
			var finishTime = DateTime.Now.AddSeconds(timeOut);

			do
			{
				if (condition())
				{
					return true;
				}
				System.Threading.Thread.Sleep(500);
			}
			while (DateTime.Now < finishTime);

			// fail
			return false;
		}
		
		public static void CallMethodForNSeconds(Action condition, int timeOut = Common.TimeOuts.TimeOutDefault)
		{
			var finishTime = DateTime.Now.AddSeconds(timeOut);
			do
			{
				condition();
				System.Threading.Thread.Sleep(500);
			}
			while (DateTime.Now < finishTime);
		}
		
	}
}
