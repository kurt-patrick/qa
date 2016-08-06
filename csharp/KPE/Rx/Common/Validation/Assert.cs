/*
 * Created by Ranorex
 * User: user
 * Date: 6/08/2016
 * Time: 6:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KPE.Rx.Common.Validation
{
	/// <summary>
	/// Description of Assert.
	/// </summary>
	public class Assert : ValidationBase
	{
		private static Assert _that = new Assert();
		public static Assert That { get { return _that; } }
		private Assert() : base(ValidationType.Assert)
		{
		}
	}
}
