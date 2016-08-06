/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 2:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KPE.Rx.Common
{
	/// <summary>
	/// Description of TimeSpans.
	/// </summary>
	public static class TimeSpans
	{
		public static TimeSpan DefaultTimeOut = TimeSpans.Sec10;
		public static TimeSpan Sec2 = TimeSpan.FromSeconds(2);
		public static TimeSpan Sec5 = TimeSpan.FromSeconds(5);
		public static TimeSpan Sec10 = TimeSpan.FromSeconds(10);
		public static TimeSpan Sec20 = TimeSpan.FromSeconds(20);
		public static TimeSpan Sec30 = TimeSpan.FromSeconds(30);
	}
	
	public static class TimeOuts
	{
		public const int TimeOutDefault = TimeOuts.Ten;
		public const int Two = 2;
		public const int Five = 5;
		public const int Ten = 10;
		public const int Twenty = 20;
		public const int Thirty = 30;
	}
	
}
