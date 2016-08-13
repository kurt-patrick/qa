/*
 * Created by Ranorex
 * User: user
 * Date: 3/08/2016
 * Time: 8:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Windows.Forms;
using Keyboard = Ranorex.Keyboard;

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

		public static void F5()
		{
			PressKey(Keys.F5);
		}
		
		public static void CtrlF5()
		{
			CtrlPlusKey(Keys.F5);
		}
		
		private static void PressKey(Keys keyCode)
		{
			Keyboard.Press(keyCode);
		}
		
		public static void CtrlPlusKey(params Keys[] keyCode)
		{
			Keyboard.Down(Keys.ControlKey);
			keyCode.ToList().ForEach(key => Keyboard.Press(key));
			Keyboard.Up(Keys.ControlKey);
		}
		
	}
}
