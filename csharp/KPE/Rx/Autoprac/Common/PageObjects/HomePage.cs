/*
 * Created by Ranorex
 * User: user
 * Date: 17/08/2016
 * Time: 6:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using KPE.Rx.Common;
using KPE.Rx.Common.Exceptions;
using KPE.Rx.Common.Helper;
using Ranorex;
using Ranorex.Core.Repository;

namespace KPE.Rx.Autoprac.Common.PageObjects
{
	/// <summary>
	/// Description of HomePage.
	/// </summary>
	public class HomePage : AutopracPageBase
	{
		#region enums
		public enum eProductCategory
		{
			[Description("homefeatured")]
			Popular = 0,
			[Description("blockbestsellers")]
			BestSellers
		}
		#endregion

		#region constructors
		public HomePage() : base(_ux.Header.SelfInfo, _ux.Body.SelfInfo, _ux.Footer.SelfInfo)
		{
		}
		#endregion

		#region methods
		public LoginPage ClickSignIn()
		{
			PerformClick(_ux.Header.Login);
			return new LoginPage();
		}

		public bool ClickSignOut()
		{
			return TryClickAndValidate(_ux.Header.Logout, () => IsElementVisible(_ux.Header.LoginInfo));
		}

		public bool IsSignedIn()
		{
			return DoesElementExist(_ux.Header.LogoutInfo);
		}

		public bool ClickTab(eProductCategory value)
		{
			if(value == GetSelectedTab())
			{
				return true;
			}

			// Click the required Tab and validate
			RepoItemInfo itemInfo = _ux.Body.HomePage.PopularAndBestSellers.PopularProductsInfo;
			if(value == eProductCategory.BestSellers) {
				itemInfo = _ux.Body.HomePage.PopularAndBestSellers.BestSellersTabInfo;
			}
			return TryClickAndValidate(itemInfo, () => value == GetSelectedTab());
		}

		public List<HomePageProduct> GetPopularProducts(bool clickTab)
		{
			return GetProducts(eProductCategory.Popular, clickTab);
		}

		public List<HomePageProduct> GetBestSellerProducts(bool clickTab)
		{
			return GetProducts(eProductCategory.BestSellers, clickTab);
		}

		private string GetTabClassName(eProductCategory value)
		{
			return EnumHelper.GetDescription(value);
		}

		public eProductCategory GetSelectedTab()
		{
			var retVal = eProductCategory.Popular;
			var element = _ux.Body.HomePage.PopularAndBestSellers.ActiveTab;
			string bestSellersClass = EnumHelper.GetDescription(eProductCategory.BestSellers);
			if (ElementHelper.GetAttributeClass(element).Equals(bestSellersClass))
			{
				retVal = eProductCategory.BestSellers;
			}
			return retVal;
		}

		private List<HomePageProduct> GetProducts(eProductCategory value, bool clickTab)
		{
			if (clickTab)
			{
				ClickTab(eProductCategory.Popular);
			}

			if (GetSelectedTab() != value)
			{
				InvalidStateException.Throw(
					string.Format("The tab {0} must be clicked before calling GetProducts()", value.ToString()));
			}

			var bodyEle = _ux.Body.Self;
			RepoItemInfo itemInfo = _ux.Body.HomePage.PopularAndBestSellers.PopularProductsInfo;
			if(value == eProductCategory.BestSellers) {
				itemInfo = _ux.Body.HomePage.PopularAndBestSellers.BestSellerProductsInfo;
			}
			
			return
				bodyEle.Find(itemInfo.Path)
				.Select((ele, index) => new HomePageProduct(string.Format("{0}[{1}]", itemInfo.Path, index + 1)))
				.ToList();

		}

		public HomePageCart ShoppingCart()
		{
			return new HomePageCart();
		}
		#endregion
	}
}
