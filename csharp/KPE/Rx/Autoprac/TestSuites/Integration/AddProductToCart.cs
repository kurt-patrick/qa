/*
 * Created by Ranorex
 * User: user
 * Date: 18/08/2016
 * Time: 7:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using KPE.Rx.Autoprac.Common.PageObjects;
using KPE.Rx.Common.Validation;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace Integration
{
	/// <summary>
	/// Description of AddProductToCart.
	/// </summary>
	[TestModule("4B648C47-53F9-4C91-8E8C-EE88B7A5B88F", ModuleType.UserCode, 1)]
	public class AddProductToCart : ITestModule
	{
		bool _addBestSeller = false;
		HomePage _homePage = new HomePage();
		HomePage.HomePageProductCategory _productCategory = HomePage.HomePageProductCategory.Popular;
		
		[TestVariable("f0f77f10-6e0a-43ac-b364-fc48701c9d48")]
		public bool AddBestSeller
		{
			get { return _addBestSeller; }
			set 
			{ 
				_addBestSeller = value; 
				_productCategory = (AddBestSeller) ? HomePage.HomePageProductCategory.BestSellers : HomePage.HomePageProductCategory.Popular;
			}
		}
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public AddProductToCart()
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
			
			var products = _homePage.GetProducts(_productCategory, true);
			Assert.That.IsTrue(products.Count > 0, string.Format("At least 1 ({0}) product exists", products.Count));
			
			var product = products.First();
			Assert.That.IsTrue(product.MouseOver(), "Mouse over product");
			
			
		}
	}
}
