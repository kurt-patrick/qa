using KPE.Mobile.App.Automation.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using System;
using System.Diagnostics;
using System.IO;

namespace KPE.Mobile.App.Automation.Helpers
{
    public sealed class AppiumLocalServiceHelper
    {
        private AppiumLocalService _appiumLocalService = null;

        private AppiumLocalServiceHelper(AppiumLocalService service)
        {
            _appiumLocalService = service;
        }

        public static AppiumLocalServiceHelper Build(DriverCapabilities capabilities, out AppiumLocalService service)
        {
            var serverOptions = new OptionCollector();
            serverOptions.AddCapabilities(capabilities.DesiredCapabilities());

            var builder = new AppiumServiceBuilder();

            var debugging = InstalledNodeInCurrentFileSystem;

            service = builder
                .UsingAnyFreePort()
                .WithArguments(serverOptions)
                .WithIPAddress("127.0.0.1")
                .WithStartUpTimeOut(TimeSpan.FromSeconds(10))
                .Build();

            return new AppiumLocalServiceHelper(service);
        }

        public AppiumLocalServiceHelper Start()
        {
            if (!_appiumLocalService.IsRunning)
            {
                _appiumLocalService.Start();
            }
            return this;
        }

        public AppiumLocalServiceHelper WaitUntilRunning(int timeout)
        {
            WaitHelper.TryWaitForCondition(() => IsRunning(), timeout);
            return this;
        }

        public bool IsRunning()
        {
            return _appiumLocalService.IsRunning;
        }

        public void AssertIsRunning()
        {
            Assert.IsTrue(IsRunning(), "Appium local service is not running");
        }


        private static Process StartSearchingProcess(string file, string arguments)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = file;
            if (!string.IsNullOrEmpty(arguments))
            {
                proc.StartInfo.Arguments = arguments;
            }
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.WaitForExit();
            return proc;
        }

        private static string GetTheLastStringFromsOutput(StreamReader processOutput)
        {
            string result = string.Empty;
            while (!processOutput.EndOfStream)
            {
                string current = processOutput.ReadLine();
                if (string.IsNullOrEmpty(current))
                {
                    continue;
                }
                result = current;
            }
            return result;
        }
        private static FileInfo InstalledNodeInCurrentFileSystem
        {
            get
            {
                string instancePath;
                Process p = null;

                bool isWindows = Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows);
                byte[] bytes;
                string pathToScript = null;

                try
                {
                    if (isWindows)
                    {
                        p = StartSearchingProcess("cmd.exe", "/C npm root -g");
                    }
                }
                catch (Exception e)
                {
                    if (p != null)
                    {
                        p.Close();
                    }
                    if (pathToScript != null && File.Exists(pathToScript))
                    {
                        File.Delete(pathToScript);
                    }
                    throw e;
                }

                instancePath = GetTheLastStringFromsOutput(p.StandardOutput);

                try
                {
                    DirectoryInfo defaultAppiumNode;
                    if (string.IsNullOrEmpty(instancePath) || !(defaultAppiumNode = new DirectoryInfo(instancePath + Path.DirectorySeparatorChar +
                            "appium")).Exists)
                    {
                        throw new InvalidElementStateException();
                    }

                    //appium servers v1.5.x and higher
                    FileInfo newResult;

                    string appiumNodeMask = 
                        $"{Path.DirectorySeparatorChar}build{Path.DirectorySeparatorChar}" +
                        $"lib{Path.DirectorySeparatorChar}main.js";
                    if ((newResult = new FileInfo(defaultAppiumNode.FullName + appiumNodeMask)).Exists)
                    {
                        return newResult;
                    }

                    throw new Exception();
                    /*
                    throw new Exception(ErrorNodeNotFound,
                                new IOException($"Could not find the file " +
                                $"{ AppiumServiceConstants.AppiumNodeMask} in the {defaultAppiumNode} directory"));
                                */
                }
                finally
                {
                    p.Close();
                    if (pathToScript != null && File.Exists(pathToScript))
                    {
                        File.Delete(pathToScript);
                    }
                }
            }
        }

    }
}
