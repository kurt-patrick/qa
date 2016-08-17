/*
 * Created by Ranorex
 * User: user
 * Date: 17/08/2016
 * Time: 6:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using KPE.Rx.Common.Helper;
using Ranorex.Core.Repository;

namespace KPE.Rx.Autoprac.Common.PageObjects
{
	/// <summary>
	/// Description of PageBase.
	/// </summary>
	public class AutopracPageBase : KPE.Rx.Common.PageObject.PageBase
	{
		protected static Repo.AutopracRepository _repo = Repo.AutopracRepository.Instance;
		protected static Repo.AutopracRepositoryFolders.AutopracUXAppFolder _ux = Repo.AutopracRepository.Instance.AutopracUX;
		protected List<RepoItemInfo> _isLoadedElements = new List<RepoItemInfo>();
		
		public AutopracPageBase(params RepoItemInfo[] elements) 
		{
			_isLoadedElements.AddRange(elements);
		}

		public override bool IsLoaded()
		{
			ThrowHelper.ThrowArgumentOutOfRangeExceptionIfTrue(
				_isLoadedElements.Count == 0, "_isLoadedElements", 
				"At least 1 element is required to determine if the page is loaded");
			return AreElementsVisible(_isLoadedElements);
		}
		
	}
}
