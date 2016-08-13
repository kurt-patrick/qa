using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.Se.Common.Helpers
{
    public sealed class KeyboardHelper
    {
        private IWebDriver _driver;

        private KeyboardHelper(IWebDriver driver)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(driver);
            _driver = driver;
        }

        public static KeyboardHelper Create(IWebDriver driver)
        {
            return new KeyboardHelper(driver);
        }

        public KeyboardHelper Tab()
        {
            return SendKeys(Keys.Tab);
        }

        private KeyboardHelper SendKeys(string text)
        {
            _driver.SwitchTo().ActiveElement().SendKeys(text);
            return this;
        }

    }
}
