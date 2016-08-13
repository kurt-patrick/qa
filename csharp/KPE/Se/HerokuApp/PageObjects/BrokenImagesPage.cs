using KPE.Se.Common.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.HerokuApp.PageObjects
{
    public class BrokenImagesPage : Common.PageBase
    {
        By _img01Tag = ImageXPath(1);
        By _img02Tag = ImageXPath(2);
        By _img03Tag = ImageXPath(3);
        By _h3Tag = By.TagName("h3");

        public BrokenImagesPage(IWebDriver driver)
            : base(driver, Constants.BaseUrl + "broken_images")
        {
        }

        private static By ImageXPath(int index)
        {
            string xPath = "//div[@class='example']/img" + ((index > 0) ? string.Format("[{0}]", index) : "");
            return By.XPath(xPath);
        }

        /// <summary>
        /// Index is 1 based and must be between 1 and 3
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if broken else false</returns>
        public bool IsImageBroken(int index)
        {
            QA.Utils.Int32Util.ThrowIfNotBetween(1, 3, index);
            By imgTag = ImageXPath(index);
            return IsImageBroken(imgTag);
        }

        public override bool IsLoaded()
        {
            var locators = new List<By> { _h3Tag, _img01Tag, _img02Tag, _img03Tag };
            return AreElementsVisible(locators);
        }

    }
}
