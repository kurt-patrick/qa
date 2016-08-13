using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Helpers
{
    public static class ByHelper
    {
        public static By XPath(this By by, string basePath, string elementPath)
        {
            string xPath = basePath + elementPath;
            return By.XPath(xPath);
        }

        public static By GetByXPath(this string basePath)
        {
            return By.XPath(basePath);
        }

        public static By GetChildXPathLocator(this string basePath, string elementPath)
        {
            return By.XPath(basePath + elementPath);
        }


    }
}
