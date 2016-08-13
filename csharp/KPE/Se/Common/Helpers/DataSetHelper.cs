using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPE.Se.Common.Exceptions;
using System.Reflection;

namespace KPE.Se.Common.Helpers
{
    public static class DataSetHelper
    {
        public static List<T> LoadFromCsv<T>(string relativePath) where T : new()
        {
            var retVal = new List<T>();
            using (TextFieldParser parser = CreateTextFieldParser(relativePath))
            {
                // Parse the csv headers
                Dictionary<int, string> csvHeadersDict = ParseCsvHeaders(parser);

                // Remove csv headers that do have an associated property on the object <T>
                RemoveCsvHeadersNotFoundInMappedObject<T>(csvHeadersDict);

                // foreach row create a new object T and populate
                while (!parser.EndOfData)
                {
                    var newRow = CreateObjectAndSetProperties<T>(parser, csvHeadersDict);
                    retVal.Add(newRow);
                }

            }

            if (retVal.Count == 0) {
                throw new Exceptions.InvalidCsvExpection("The CSV contains no data except for headers");
            }

            return retVal;
        }

        private static TextFieldParser CreateTextFieldParser(string relativePath)
        {
            QA.Utils.StringUtil.ThrowIfNullOrEmpty(relativePath);

            var currentDir = System.IO.Directory.GetCurrentDirectory();

            currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            //currentDir = System.Reflection.Assembly.GetExecutingAssembly().Location;

            currentDir = System.IO.Path.Combine(currentDir, relativePath);

            var retVal = new TextFieldParser(currentDir);
            retVal.SetDelimiters(",");
            retVal.TrimWhiteSpace = true;
            retVal.TextFieldType = FieldType.Delimited;
            return retVal;
        }

        private static Dictionary<int, string> ParseCsvHeaders(TextFieldParser parser)
        {
            int index = 0;
            var retVal = new Dictionary<int, string>();
            foreach (var column in parser.ReadFields())
            {
                retVal[index] = column;
                index += 1;
            }

            // Validate at least 1 key exists in the dictionary
            if(retVal.Count == 0) {
                throw new InvalidCsvExpection("No columns exist");
            }

            return retVal;
        }

        private static T CreateObjectAndSetProperties<T>(TextFieldParser parser, Dictionary<int, string> csvHeadersDict) where T : new()
        {
            T retVal = new T();
            Type type = retVal.GetType();
            var fields = parser.ReadFields();
            var mappedIndexes = csvHeadersDict.Keys.ToList();

            foreach (int mappedIndex in mappedIndexes)
            {
                string csvValue = fields[mappedIndex];
                string propertyName = csvHeadersDict[mappedIndex];

                // Get PropertInfo object mapped with the dataset
                var property = type.GetProperties().First(prop => prop.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase));

                // Cast <string> into strongly typed value
                object setValue = CastCsvValueIntoStronglyTypeValue(property, csvValue);

                // Set the objects property using reflection
                property.SetValue(retVal, setValue, null);

            }

            return retVal;
        }

        private static object CastCsvValueIntoStronglyTypeValue(PropertyInfo property, string csvValue)
        {
            if (property.PropertyType.Equals(typeof(string)))
            {
                return csvValue;
            }
            else if (property.PropertyType.Equals(typeof(bool)))
            {
                var ignoreCase = StringComparison.CurrentCultureIgnoreCase;
                if (string.IsNullOrWhiteSpace(csvValue) || "no".Equals(csvValue, ignoreCase) || "n".Equals(csvValue, ignoreCase) || "f".Equals(csvValue, ignoreCase))
                {
                    return false;
                }
                if ("yes".Equals(csvValue, ignoreCase) || "y".Equals(csvValue, ignoreCase) || "t".Equals(csvValue, ignoreCase))
                {
                    return false;
                }
                return bool.Parse(csvValue);
            }
            else if (property.PropertyType.Equals(typeof(int)))
            {
                return int.Parse(csvValue);
            }
            else if (property.PropertyType.Equals(typeof(decimal)))
            {
                return decimal.Parse(csvValue);
            }
            else if (property.PropertyType.Equals(typeof(double)))
            {
                return double.Parse(csvValue);
            }

            throw new NotImplementedException("Case not handled: " + property.PropertyType.ToString());
        }

        private static void RemoveCsvHeadersNotFoundInMappedObject<T>(Dictionary<int, string> csvHeadersDict) where T : new()
        {
            T obj = new T();

            var propertyNames = obj.GetType().GetProperties().Select(info => info.Name).ToList();
            if(propertyNames.Count == 0)
            {
                throw new InvalidDataSetObjectException("The dataset object contains no properties");
            }

            var keys = csvHeadersDict.Keys.ToList();
            foreach(int key in keys)
            {
                string csvColumn = csvHeadersDict[key];
                if(!propertyNames.Contains(csvColumn, StringComparer.CurrentCultureIgnoreCase))
                {
                    csvHeadersDict.Remove(key);
                }
            }

            // Validate there is at least 1 mapped column
            if (csvHeadersDict.Count == 0)
            {
                throw new InvalidCsvExpection("The csv contains no properties that are mapped to the dataset object");
            }

        }


    }

}
