/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 12:34 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using KPE.Rx.Common.Exceptions;
using KPE.Rx.Common.Helper;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Common.TestModule
{
    /// <summary>
    /// Description of CloseBrowser.
    /// </summary>
    [TestModule("6C5DBD10-E920-4BEE-A392-939C30BC4AED", ModuleType.UserCode, 1)]
    public class KillAUT : ITestModule
    {
    	int _processId = 0;
    	[TestVariable("1D58D1F7-8054-4E5D-B804-38078229C7FA")]
    	public string ProcessId
    	{
    		get { return _processId.ToString(); }
    		set { int.TryParse(value, out _processId); }
    	}
    	
    	string _browserName = "";
    	[TestVariable("444DA956-C28C-408B-95AD-2EAAAE0FEFAC")]
    	public string BrowserName
    	{
    		get { return _browserName; }
    		set { _browserName = value; }
    	}
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public KillAUT()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            bool flgSuccess =
            	ExceptionCatcher.TryCallMethod(() => Ranorex.Host.Local.CloseApplication(_processId)) ||
            	ExceptionCatcher.TryCallMethod(() => Ranorex.Host.Local.KillBrowser(_browserName)) ||
            	ExceptionCatcher.TryCallMethod(() => Ranorex.Host.Local.KillApplication(_processId));
            
            Validate.AreEqual(flgSuccess, true, "Closed the browser");
            	
        }
    }
}
