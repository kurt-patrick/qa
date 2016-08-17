/*
 * Created by Ranorex
 * User: user
 * Date: 17/08/2016
 * Time: 8:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Ranorex;

namespace KPE.Rx.Common.Helper
{
	/// <summary>
	/// Description of ReportHelper.
	/// </summary>
	public static class ReportHelper
	{
		public static void ReportErrorWithScreenshot(string message)
		{
			Report.Error(message);
			Report.Screenshot();
		}
	}
}
