using au.com.kleenheat.se.common;
using au.com.kleenheat.se.helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se.pages
{
    public class HomePage : PageBase
    {
        // icons visible in all view modes
        By _topNavResidential = By.CssSelector("a > span.ico.icon-home");
        By _topNavBusiness = By.CssSelector("a > span.ico.icon-office");
        By _topNavTransport = By.CssSelector("a > span.ico.icon-car-front");

        // Moile view specific icons
        By _topNavToggleMenu = By.Id("mobiToggle");

        // Browser specific icons
        By _tray = By.Id("tray");
        By _trayBecomeACustomer = By.XPath("//div[@id='tray']//a[text()[contains(., 'Become a customer')]]");
        By _postcodeInput = By.Id("postcodeInput");
        By _postcodeBtn = By.Id("postcodeBtn");

        public HomePage(TestCaseSettings settings)
            : base(settings)
        {
        }

        public override Boolean IsLoaded()
        {
            return IsVisible(_postcodeBtn, _postcodeInput, _topNavResidential, _topNavBusiness, _topNavTransport);
        }

        public void NavigateToPage()
        {
            NavigateTo();
        }

        public HomePage EnterPostCode(String value)
        {
            SendKeys(_postcodeInput, value);
            return this;
        }

        public HomePage ClickGetStarted()
        {
            Click(_postcodeBtn);
            return this;
        }

        public Boolean IsTrayVisible()
        {
            return IsVisible(_tray);
        }

        public NewResidentialCustomerPage ClickTrayLinkBecomeACustomer()
        {
            var element = FindElement(_trayBecomeACustomer);
            WaitForMovingElementToBeStationary(element);
            Click(element);

            return new NewResidentialCustomerPage(_testCaseSettings);
        }

        public List<IWebElement> GetTrayLinks()
        {

            /*
            $x("//div[@id='tray']//a[text()[contains(., 'Become a customer')]]")
            $x("//div[@id='tray']//a[text()[contains(., 'Order LPG')]]")
            $x("//div[@id='tray']//a[text()[contains(., 'cylinder return')]]")
            $x("//div[@id='tray']//a[text()[contains(., 'business LPG')]]")
            */

            var elements = FindElements(By.XPath("//div[@id='tray']//a")).ToList();
            for (int i = elements.Count - 1; i > 0; i--)
            {
                if (!elements[i].Displayed)
                {
                    elements.RemoveAt(i);
                }
            }
            return elements;
        }

    }
}

