/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 5:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KPE.Rx.Common.Exceptions
{
	/// <summary>
	/// Description of InvalidStateException.
	/// </summary>
	public class InvalidStateException : System.Exception
	{
		public InvalidStateException(string message) : base(message)
		{
		}
		
		public static void ThrowIfFalse(bool value, string message)
		{
			if(!value) {
				Throw(message);
			}
		}
		
		public static void Throw(string message)
		{
			throw new InvalidStateException(message);
		}
			
	}
}
