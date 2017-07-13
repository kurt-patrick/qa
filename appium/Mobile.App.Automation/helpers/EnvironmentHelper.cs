using au.com.kleenheat.se.qa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace au.com.kleenheat.se.helpers
{
    public static class EnvironmentHelper
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            return retVal ?? "";
        }

        /// <summary>
        /// Reads a file and reads the contents as a list
        /// </summary>
        /// <param name="path"></param>
        /// <returns>All the lines within the file including blank or whitespace lines</returns>
        public static List<string> GetFileContentsAsList(string path)
        {
            StringQA.ThrowIfNullOrWhiteSpace(path);

            if (!File.Exists(path))
            {
                _log.Debug("File not found (will try to detect bin folder assembly): " + path);

                // In the case of debugging from NUnit, the folder path is different
                path = Path.Combine(GetBinFolderPath(), path);
                if (!File.Exists(path))
                {
                    _log.Error("File not found: " + path);
                    throw new FileNotFoundException("File not foundL " + path);
                }
            }

            // Only return lines where at least 1 char
            var retVal = File.ReadAllLines(path).ToList();

            return retVal;
        }

        /// <summary>
        /// Get the bin folder path
        /// </summary>
        /// <returns></returns>
        public static string GetBinFolderPath()
        {
            // note: the use of substring is required as the value starts with file://
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
        }

    }
}