using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.Common.Helpers
{
    public static class JavaScriptHelper
    {
        public static object ExecuteScript(IWebDriver driver, string script, params object[] args)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(driver);
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(script);
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            var response = js.ExecuteScript(script, args);
            return response;
        }

        /// <summary>
        /// Uses JS to drag one element onto a destination element
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="dragElementId"></param>
        /// <param name="dropElementId"></param>
        /// <returns></returns>
        public static object DragDrop(IWebDriver driver, string dragElementId, string dropElementId)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(driver);
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(dragElementId);
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(dropElementId);

            string script = "$('#?0?').simulateDragDrop({ dropTarget: '#?1?'});"
                .Replace("?0?", dragElementId).Replace("?1?", dropElementId);

            string dndJs = Properties.Resources.DragDropJs;

            return ExecuteScript(driver, dndJs + script);

        }

        /// <summary>
        /// Uses JavaScript to determine if the image is broken or not
        /// http://stackoverflow.com/questions/16784534/find-broken-images-in-page-image-replace-by-another-image/
        /// http://elementalselenium.com/tips/67-broken-images
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if broken else false</returns>
        public static bool IsImageBroken(IWebDriver driver, IWebElement element)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(element);

            string script = "return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 0";
            var response = ExecuteScript(driver, script, element);

            return !bool.Parse(response.ToString());
        }


    }
}
