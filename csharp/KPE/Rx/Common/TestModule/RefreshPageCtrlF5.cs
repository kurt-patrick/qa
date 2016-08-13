/*
 * Created by Ranorex
 * User: user
 * Date: 13/08/2016
 * Time: 5:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using KPE.Rx.Common.Helper;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace KPE.Rx.Common.TestModule
{
    /// <summary>
    /// Description of RefreshPageCtrlF5.
    /// </summary>
    [TestModule("D2330858-A9F2-4545-BB1B-3877C4F3C52F", ModuleType.UserCode, 1)]
    public class RefreshPageCtrlF5 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public RefreshPageCtrlF5()
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
            
            KeyboardHelper.F5();
            
        }
    }
}
