using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Helpers
{
    public static class StringHelper
    {
        public static string GetEnvVariable(string key)
        {
            string retVal = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
            if (retVal == null)
            {
                retVal = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);
            }
            if (retVal == null)
            {
                retVal = Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
            }
            return retVal;
        }

    }
}
