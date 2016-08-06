/*
 * Created by Ranorex
 * User: user
 * Date: 6/08/2016
 * Time: 6:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net.WebSockets;
using Ranorex;

namespace KPE.Rx.Common.Validation
{
	/// <summary>
	/// Description of ValidationBase.
	/// </summary>
	public abstract class ValidationBase
	{
		public enum ValidationType
		{
			Assert,
			Verify
		}
		
		private bool _exceptionOnFail = false;
		//private Ranorex.Validate.Options _validationOptions = null;
		private readonly ValidationType _validationType = ValidationType.Assert;
		
		protected ValidationBase(ValidationType validationType)
		{
			_validationType = validationType;
			_exceptionOnFail = _validationType == ValidationType.Assert;
			//_validationOptions = ;
		}
		
		public void IsTrue(bool actual, string message)
		{
			AreEqual(actual, true, message);
		}
		
		public void IsFalse(bool actual, string message)
		{
			AreEqual(actual, false, message);
		}
		
		public void AreEqual(string actual, string expected, string message, StringComparison comp)
		{
			AreEqual(string.Equals(actual, expected, comp), true, message);
		}
		
		public void AreEqual(object actual, object expected, string message)
		{
			var options = new Ranorex.Validate.Options(_exceptionOnFail, Ranorex.Validate.ResultOption.OnFail);
			Validate.AreEqual(actual, expected, message, options);
		}
		
		
	}
}
