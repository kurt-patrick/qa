/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 12:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using KPE.Rx.Common.Helper;
using Word;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace KPE.Rx.Common.TestModule
{
    /// <summary>
    /// Description of LoadBrowser.
    /// </summary>
    [TestModule("A9E58D86-228A-4DFA-922F-46F04DF2FE7F", ModuleType.UserCode, 1)]
    public class LoadAUT : ITestModule
    {
    	string _browserName = "chrome";
    	[TestVariable("fe5fb7e5-e3a6-4681-9d48-d4c3688203eb")]
    	public string BrowserName
    	{
    		get { return _browserName; }
    		set { _browserName = value; }
    	}
    	
    	string _url = "";
    	[TestVariable("198e2b81-8c24-43a7-9074-fd6e76b22f73")]
    	public string Url
    	{
    		get { return _url; }
    		set { _url = value; }
    	}
    	
    	int _processId = 0;
    	[TestVariable("f4db8dd0-e86b-4bc2-a3ec-4ef0ad1e3eec")]
    	public string ProcessId
    	{
    		get { return _processId.ToString(); }
    		set { int.TryParse(value, out _processId); }
    	}
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public LoadAUT()
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
            
            // http://www.ranorex.com/forum/private-browsing-t5891.html
            
			string browserArgs = "";
			if("Chrome".Equals(browserArgs,StringComparison.CurrentCultureIgnoreCase))
			{
				browserArgs = "--incognito --disable-save-password-bubble --disable-infobars";
			}
            
            _processId = Ranorex.Host.Local.OpenBrowser(_url, _browserName, browserArgs, true, true, true, true, true);
            
            Validate.Exists(@"/form", TimeSpans.DefaultTimeOut, "Form element exists");
            
            Validate.IsTrue(_processId > 0, "Browser has been opened", true);
            
        }
    }
}
