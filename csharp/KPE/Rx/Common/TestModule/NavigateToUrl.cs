/*
 * Created by Ranorex
 * User: user
 * Date: 4/08/2016
 * Time: 9:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace KPE.Rx.Common.TestModule
{
    /// <summary>
    /// Description of NavigateToUrl.
    /// </summary>
    [TestModule("BC6EA647-E741-41AD-8B11-599F8CC38081", ModuleType.UserCode, 1)]
    public class NavigateToUrl : ITestModule
    {
    	string _url = "";
    	[TestVariable("c6a99981-9823-403f-9b37-22af97d926d2")]
    	public string Url
    	{
    		get { return _url; }
    		set { _url = value; }
    	}
    	
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public NavigateToUrl()
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
            
            var webdoc = Ranorex.Host.Local.FindDescendant<WebDocument>();
            webdoc.Navigate(_url, true, TimeSpans.DefaultTimeOut);
            if(webdoc != null)
            {
            	//WaitHelper.CallMethodForNSeconds(() => Report.Info("doc.State: " + doc.State??string.Empty), Common.Seconds.Five);
            }
            
        }
    }
}
