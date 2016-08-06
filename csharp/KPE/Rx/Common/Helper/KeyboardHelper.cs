/*
 * Created by Ranorex
 * User: user
 * Date: 3/08/2016
 * Time: 8:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace KPE.Rx.Common.Helper
{
	/// <summary>
	/// Description of KeyboardHelper.
	/// </summary>
	public static class KeyboardHelper
	{
		public static void Tab()
		{
			PressKey(Keys.Tab);
		}
		
		private static void PressKey(Keys keyCode)
		{
			Ranorex.Keyboard.Press(keyCode);
		}
	}
}
