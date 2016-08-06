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
	/// Description of Verify.
	/// </summary>
	public class Verify : ValidationBase
	{
		private static Verify _that = new Verify();
		public static Verify That { get { return _that; } }
		public Verify() : base(ValidationType.Verify)
		{
		}
	}
}
