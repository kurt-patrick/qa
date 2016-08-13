using KPE.Se.Common;
using KPE.Se.Common.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace KPE.Se.HerokuApp.PageObjects
{
    public class MultipleWindowsPage : PageBase
    {
        #region constructors
        By _h3Tag = By.TagName("h3");
        By _clickHereATag = By.CssSelector(".example a");
        #endregion

        #region constructors
        public MultipleWindowsPage(IWebDriver driver)
            : base(driver, "https://the-internet.herokuapp.com/windows")
        {
        }
        #endregion

        #region methods
        internal void ClickATag()
        {
            PerformClick(_clickHereATag);
        }

        public string GetCurrentWindowHandle()
        {
            return _driver.CurrentWindowHandle;
        }

        /// <summary>
        /// Polls the web browser for the new window
        /// </summary>
        /// <param name="windowHandles"></param>
        /// <returns>handle for the new window</returns>
        public string GetNewWindowHandle(int timeOut, params string[] existingHandles)
        {
            QA.Utils.ArrayUtil.ThrowIfArrayIsNullOrEmpty(existingHandles);

            string newHandle = string.Empty;

            Func<bool> condition = () => {
                newHandle = _driver.WindowHandles.FirstOrDefault(handle => !existingHandles.Contains(handle, StringComparer.CurrentCultureIgnoreCase));
                return !string.IsNullOrWhiteSpace(newHandle);
            };

            WaitHelper.TryWaitForCondition(condition, timeOut);

            return newHandle;
        }

        public string GetWindowTitle(string windowHandle)
        {
            var driver = _driver.SwitchTo().Window(windowHandle);
            return driver.Title;
        }

        public override bool IsLoaded()
        {
            var list = new List<By> { _h3Tag, _clickHereATag };
            return AreElementsVisible(list);
        }
        #endregion

    }
}
