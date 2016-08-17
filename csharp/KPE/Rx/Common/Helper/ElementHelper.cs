/*
 * Created by Ranorex
 * User: user
 * Date: 3/08/2016
 * Time: 9:02 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Ranorex;

namespace KPE.Rx.Common.Helper
{
	/// <summary>
	/// Description of ElementHelper.
	/// </summary>
	public static class ElementHelper
	{
		public static string GetAttribute(WebElement element, string attribute)
		{
			return element.GetAttributeValue<string>(attribute) ?? string.Empty;
		}
		
		public static string GetAttributeClass(WebElement element)
		{
			return GetAttribute(element, "class");
		}
		
		public static bool DoesAttributeContain(WebElement element, string attribute, string contains)
		{
			return GetAttribute(element, attribute).Contains(contains);
		}
	}
}
