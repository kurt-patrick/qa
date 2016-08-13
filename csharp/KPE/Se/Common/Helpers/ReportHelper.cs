using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Helpers
{
    public static class ReportHelper
    {
        private static int _screenshotCount = 0;

        public static void TakeScreenshot(IWebDriver driver)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            if (screenshotDriver == null)
            {
                return;
            }

            var currentDirectory = Directory.GetCurrentDirectory();
            var testName = TestContext.CurrentContext.Test.Name;
            string fileName = string.Format("{0}_{1:00}.png", testName, GetScreenshotCount());

            Console.WriteLine("Taking screenshot.");
            var screenshot = screenshotDriver.GetScreenshot();

            Console.WriteLine("Saving screenshot {0}.", fileName);
            screenshot.SaveAsFile(currentDirectory + "\\" + fileName, ImageFormat.Png);
        }

        public static void TakeScreenshot(IWebDriver driver, string message)
        {
            TakeScreenshot(driver);
            WriteLineToCurrentResult(message);
        }

        private static int GetScreenshotCount()
        {
            _screenshotCount += 1;
            return _screenshotCount;
        }

        public static void WriteLineToCurrentResult(string value)
        {
            if(!string.IsNullOrWhiteSpace(value)) 
            {
                TestContext.WriteLine(value);
            }
        }

        public static void LogToConsole(string text)
        {
            Console.WriteLine(string.Format("{0}: {1}", text, DateTime.Now.ToString("hh:mm:ss:ffff")));
        }

    }
}
