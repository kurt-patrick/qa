/*
 * Created by Ranorex
 * User: user
 * Date: 24/07/2016
 * Time: 9:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using KPE.Rx.Common.Exceptions;
using KPE.Rx.Common.Helper;
using Ranorex;
using Ranorex.Core.Repository;

namespace KPE.Rx.Common.PageObject
{
	/// <summary>
	/// Description of PageBase.
	/// </summary>
	public abstract class PageBase
	{
		public PageBase()
		{
		}

		public abstract bool IsLoaded();
		
		protected void PressKeys(InputTag element, string text, bool clear = true)
		{
			if(clear) {
				element.Value = string.Empty;
			}
			element.PressKeys(text, Duration.FromMilliseconds(1));
		}
		
        /// <summary>
        /// Clicks on the element immediately
        /// </summary>
        /// <param name="element"></param>
        protected void PerformClick(WebElement element)
        {
            element.Click();
            //element.Checked;
        }
		
		protected string GetText(WebElement element, bool trim = true)
		{
			var retVal = element.InnerText;
			return (trim) ? retVal.Trim() : retVal;
		}
		
        /// <summary>
        /// Returns the text of the element
        /// </summary>
        /// <param name="by"></param>
        /// <returns>trimmed value of the element</returns>
        protected string GetTextIfElementIsVisible(RepoItemInfo repoItem, int timeOut = TimeOuts.TimeOutDefault, bool trim = true)
        {
            WebElement element = null;
            if (IsElementVisible(repoItem, out element, timeOut))
            {
                return GetText(element, trim);
            }
            return string.Empty;
        }
        
		protected bool AreElementsVisible(List<RepoItemInfo> repoItems, int timeOut = TimeOuts.TimeOutDefault)
		{
			return repoItems.All(repoItem => IsElementVisible(repoItem, timeOut));
		}
		
		protected bool DoElementsExist(List<RepoItemInfo> repoItems, int timeOut = TimeOuts.TimeOutDefault)
		{
			return repoItems.All(repoItem => DoesElementExist(repoItem, timeOut));
		}

		protected bool IsElementVisible(RepoItemInfo repoItem, out WebElement element, int timeOut = TimeOuts.TimeOutDefault)
		{
			element = null;
			WebElement tmpElement = null;
			var startTime = System.DateTime.Now;
			
			// Wait for the element to exist - if not found - return false
			if(!repoItem.Exists(TimeSpan.FromSeconds(timeOut), out tmpElement)) {
				return false;
			}
			
			// The element exists - poll for the remaining time until the element is visible
			var finishTime = System.DateTime.Now;
			int visibilityTimeOut = timeOut - (int)finishTime.Subtract(startTime).TotalSeconds;
			
			if(!WaitHelper.TryWaitForCondition(() => tmpElement.Visible, visibilityTimeOut)) {
				element = null;
				return false;
			}
			
			// success
			element = tmpElement;
			return true;
			
		}
		
		protected bool IsElementVisible(RepoItemInfo repoItem, int timeOut = TimeOuts.TimeOutDefault)
		{
			WebElement element = null;
			return IsElementVisible(repoItem, out element, timeOut);
		}

		protected bool DoesElementExist(RepoItemInfo repoItem, int timeOut = TimeOuts.TimeOutDefault)
		{
			return repoItem.Exists(TimeSpan.FromSeconds(timeOut));
		}
		
		protected void WaitForExists(RepoItemInfo itemInfo, int timeOut = TimeOuts.TimeOutDefault)
		{
			itemInfo.WaitForExists(timeOut);
		}
		
		protected void WaitForNotExists(RepoItemInfo itemInfo, int timeOut = TimeOuts.TimeOutDefault)
		{
			itemInfo.WaitForNotExists(timeOut);
		}

		protected bool DoesElementNotExist(RepoItemInfo itemInfo, int timeOut = TimeOuts.TimeOutDefault)
		{
			return ExceptionCatcher.TryCallMethod(() => itemInfo.WaitForNotExists(timeOut));
		}
		
        protected bool IsCheckBoxSelected(InputTag element)
        {
        	return string.Equals(element.Checked, "true", StringComparison.CurrentCultureIgnoreCase);
        }
        
        protected bool ToggleCheckBox(InputTag element, bool selected)
        {
        	if (IsCheckBoxSelected(element)) {
                return true;
            }

            Func<bool> condition = () => {
                PerformClick(element);
                return IsCheckBoxSelected(element);
            };

            return WaitHelper.TryWaitForCondition(condition);
        }
		
	}
}
