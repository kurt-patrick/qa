/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 3:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using KPE.Rx.Jetblue.PageObjects;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace KPE.Rx.Jetblue.HomePage
{
    /// <summary>
    /// Description of FlightSearch.
    /// </summary>
    [TestModule("96409B87-0A66-46BB-847E-21A596F2FCEF", ModuleType.UserCode, 1)]
    public class FlightSearch : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public FlightSearch()
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
            
            var bookingPage = new BookingPage();
            CitySelectionPage selectionDialog = null; 
            
            Validate.AreEqual(true, bookingPage.IsLoaded(), "Page is loaded");

			// From            
            selectionDialog = bookingPage.ClickFrom();
            Validate.AreEqual(true, selectionDialog.IsLoaded(), "Selection dialog is loaded");
            string city = selectionDialog.PickRandomCity();
            Validate.AreEqual(true, selectionDialog.IsClosed(), "Selection dialog is closed");
            Validate.AreEqual(true, !string.IsNullOrWhiteSpace(city), "From city has been selected: " + city??"");
            
            
        }
        
        //private void 
    }
}
