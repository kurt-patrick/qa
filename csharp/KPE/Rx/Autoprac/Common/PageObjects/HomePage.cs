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
		public enum HomePageProductCategory
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

		public bool ClickTab(HomePageProductCategory category)
		{
			if(category == GetSelectedTab()) {
				return true;
			}

			_repo.GenericKey = EnumHelper.GetDescription(category);
			RepoItemInfo itemInfo = _ux.Body.HomePage.PopularAndBestSellers.ActiveProductsInfo;
			return TryClickAndValidate(itemInfo, () => category == GetSelectedTab());
		}

		public List<HomePageProduct> GetPopularProducts(bool clickTab)
		{
			return GetProducts(HomePageProductCategory.Popular, clickTab);
		}

		public List<HomePageProduct> GetBestSellerProducts(bool clickTab)
		{
			return GetProducts(HomePageProductCategory.BestSellers, clickTab);
		}

		private string GetTabClassName(HomePageProductCategory value)
		{
			return EnumHelper.GetDescription(value);
		}

		public HomePageProductCategory GetSelectedTab()
		{
			var retVal = HomePageProductCategory.Popular;
			var element = _ux.Body.HomePage.PopularAndBestSellers.ActiveTab;
			string bestSellersClass = EnumHelper.GetDescription(HomePageProductCategory.BestSellers);
			if (ElementHelper.GetAttributeClass(element).Equals(bestSellersClass))
			{
				retVal = HomePageProductCategory.BestSellers;
			}
			return retVal;
		}

		public List<HomePageProduct> GetProducts(HomePageProductCategory category, bool clickTab)
		{
			if (clickTab)
			{
				ClickTab(HomePageProductCategory.Popular);
			}

			if (GetSelectedTab() != category)
			{
				InvalidStateException.Throw(
					string.Format("The tab {0} must be clicked before calling GetProducts()", category.ToString()));
			}

			_repo.GenericKey = EnumHelper.GetDescription(category);
			var itemInfo = _ux.Body.HomePage.PopularAndBestSellers.ActiveProductsInfo;
			
			return
				_ux.Body.Self.Find(itemInfo.Path)
				.Select((ele, index) => new HomePageProduct(category, index + 1))
				.ToList();

		}

		public HomePageCart ShoppingCart()
		{
			return new HomePageCart();
		}
		#endregion
	}
}
