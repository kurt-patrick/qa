/*
 * Created by Ranorex
 * User: user
 * Date: 3/08/2016
 * Time: 9:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using KPE.Rx.Common.Exceptions;
using Ranorex;
using Ranorex.Core.Repository;

namespace KPE.Rx.Common.Helper
{
	/// <summary>
	/// Description of SelectHelper.
	/// </summary>
	public class SelectHelper
	{
		private SelectTag _selectTag = null;
		
		public static SelectHelper Create(SelectTag selectTag)
		{
			return new SelectHelper(selectTag);
		}
		
		public static SelectHelper Create(RepoItemInfo itemInfo)
		{
			return new SelectHelper(itemInfo);
		}
		
		public SelectHelper(SelectTag selectTag)
		{
			if(selectTag == null) {
				throw new ArgumentNullException("selectTag");
			}
			_selectTag = selectTag;
		}
		
		public SelectHelper(RepoItemInfo itemInfo)
			: this(itemInfo.CreateAdapter<SelectTag>(true, TimeSpans.DefaultTimeOut))
		{
		}
		
		public void SelectByValue(string value)
		{
			var option = _selectTag.Options.First(opt => string.Equals(opt.Value, value, StringComparison.CurrentCultureIgnoreCase));
			option.Select();
			InvalidStateException.ThrowIfFalse(IsValueSelected(value), "Failed to select " + value);
		}
		
		public string GetSelectedValue()
		{
			var option = _selectTag.Options.First();
			return option.Value;
		}
		
		public bool IsValueSelected(string value)
		{
			var comp = StringComparison.CurrentCultureIgnoreCase;
			var option = _selectTag.Options.First(opt => string.Equals(opt.Value, value, comp));
			option.Select();
			return string.Equals(GetSelectedValue(), value, comp);
		}
		
	}
}
