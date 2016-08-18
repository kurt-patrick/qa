/*
 * Created by Ranorex
 * User: user
 * Date: 18/08/2016
 * Time: 8:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace KPE.Rx.Common.Exceptions
{
	/// <summary>
	/// Description of InvalidArgumentException.
	/// </summary>
	public class InvalidArgumentException : Exception
	{
		public InvalidArgumentException() : base()
		{
		}
		
		public InvalidArgumentException(string message) : base(message)
		{
		}
		
		public static void ThrowIfLessThan(int min, int actual)
		{
			if(actual < min)
			{
				throw new InvalidArgumentException(
					string.Format("Value ({0}) is invalid. Must be >= ({1})", actual, min));
			}
		}
	}
}
