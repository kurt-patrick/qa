/*
 * Created by Ranorex
 * User: user
 * Date: 2/08/2016
 * Time: 8:31 PM
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

namespace KPE.Rx.DemoQA.PageObjects
{
    /// <summary>
    /// Description of ValidatePage.
    /// </summary>
    [TestModule("8170604D-F406-4B1C-A64B-1F706B5F5FED", ModuleType.UserCode, 1)]
    public class ValidatePage : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public ValidatePage()
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
        }
    }
}
