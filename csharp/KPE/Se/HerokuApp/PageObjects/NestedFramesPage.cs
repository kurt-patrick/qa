using KPE.Se.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.HerokuApp.PageObjects
{
    public class NestedFramesPage : PageBase
    {
        #region constructors
        public NestedFramesPage(IWebDriver driver)
            : base(driver, "https://the-internet.herokuapp.com/nested_frames")
        {
        }
        #endregion

        #region methods
        /// <summary>
        /// This is the parent frame of left, middle, and right frames
        /// </summary>
        /// <returns>true if exists else false</returns>
        public bool DoesTopFrameExist()
        {
            return DoesFrameExist("frame-top");
        }

        public bool DoesLeftFrameExist()
        {
            return DoesChildFrameOfTopFrameExist("frame-left");
        }

        public bool DoesMiddleFrameExist()
        {
            return DoesChildFrameOfTopFrameExist("frame-middle");
        }

        public bool DoesRightFrameExist()
        {
            return DoesChildFrameOfTopFrameExist("frame-right");
        }

        private bool DoesChildFrameOfTopFrameExist(string frameName)
        {
            try
            {
                var driver = _driver.SwitchTo().DefaultContent();
                driver = SwitchToFrame("frame-top");
                SwitchToFrame(frameName, driver);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The bottom frame which is independant of the top frame
        /// </summary>
        /// <returns>true if exists else false</returns>
        public bool DoesBottomFrameExist()
        {
            var driver = _driver.SwitchTo().DefaultContent();
            return DoesFrameExist("frame-bottom", driver);
        }

        public override bool IsLoaded()
        {
            return ElementExists(By.TagName("frameset"));
        }
        #endregion

    }
}
