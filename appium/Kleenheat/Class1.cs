using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            a();
        }

        public static void a()
        {
            IWebDriver driver = new ChromeDriver(Constants.WebDriversDirectory);
            driver.Manage().Window.Maximize();

            driver.Url = Constants.KleenheatBaseUrl;
            driver.Navigate();

            System.Threading.Thread.Sleep(5000);

            driver.Quit();

        }
    }
}
