using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPE.RestSharp.OpenWeatherMap
{
    internal static class Helper
    {
        static string APP_ID;
        public static string GetAppId()
        {
            if (string.IsNullOrWhiteSpace(APP_ID))
            {
                APP_ID = GetEnvVariable("OpenWeatherMap_APPID");
                if (string.IsNullOrWhiteSpace(APP_ID))
                {
                    throw new ArgumentException("APP_ID is required");
                }
            }
            return APP_ID;
        }

        public static void ThrowIfNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }

        static string GetEnvVariable(string key)
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
