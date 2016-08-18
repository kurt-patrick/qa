/*
 * Created by Ranorex
 * User: user
 * Date: 17/08/2016
 * Time: 7:06 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using KPE.Rx.Common.Exceptions;
using KPE.Rx.Common.Helper;
using Ranorex.Core.Repository;

namespace KPE.Rx.Autoprac.Common.PageObjects
{
	/// <summary>
	/// Description of HomePageProduct.
	/// </summary>
	public class HomePageProduct : AutopracPageBase
	{
		private readonly int _index = 0;
		private readonly HomePage.HomePageProductCategory _category = HomePage.HomePageProductCategory.Popular;
		
		public HomePageProduct(HomePage.HomePageProductCategory category, int index)
		{
			InvalidArgumentException.ThrowIfLessThan(1, index);
			_index = index;
			_category = category;
		}
		
		/// <summary>
		/// Mouse over the base element - this will cause the Quick View element to appear
		/// </summary>
		/// <returns>True If Quick View is visible</returns>
		public bool MouseOver()
		{
			SetRepoVariables();
			_ux.Body.HomePage.PopularAndBestSellers.ActiveProduct.Self.MoveTo();
			//return ElementIsVisible(eElements.QuickView, TimeSpans.Sec5);
			var elements = new List<RepoItemInfo> { GetBy(eElements.AddToCartATag), GetBy(eElements.MoreATag) };
			return AreElementsVisible(elements);
		}
		
		private void SetRepoVariables()
		{
			_repo.GenericId = EnumHelper.GetDescription(_category);
			_repo.GenericIndex = _index.ToString();
		}

		
	}
}
