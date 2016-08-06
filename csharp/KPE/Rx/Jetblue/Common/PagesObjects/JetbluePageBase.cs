/*
 * Created by Ranorex
 * User: user
 * Date: 30/07/2016
 * Time: 3:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using KPE.Rx.Jetblue.Repo;

namespace KPE.Rx.Jetblue.PageObjects
{
	/// <summary>
	/// Description of PageBase.
	/// </summary>
	public class JetbluePageBase : KPE.Rx.Common.PageObject.PageBase
	{
		protected static JetblueRepository _repository = JetblueRepository.Instance;
		public JetbluePageBase()
		{
		}
		
		public override bool IsLoaded()
		{
			throw new NotImplementedException();
		}
	}
}
