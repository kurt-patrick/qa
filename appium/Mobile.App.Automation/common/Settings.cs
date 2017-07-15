using KPE.Mobile.App.Automation.Helpers;
using KPE.Mobile.App.Automation.QA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KPE.Mobile.App.Automation.Common
{
    internal class Settings
    {
        private int _timeOut = 20;
        private string _baseUrl = null;
        private Dictionary<string, int> _timeOuts = new Dictionary<string, int>();
        private Dictionary<string, string> _urlMap = new Dictionary<string, string>();

        private static Settings _instance = new Settings();
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Settings()
        {
            var xmlDoc = GetXmlDocument();

            XmlNodeList nodes = xmlDoc.SelectNodes("//urls/url");
            foreach (XmlNode node in nodes)
            {
                string key = GetXmlNodeAttribute(node, "key", "");
                _urlMap[key] = node.InnerText ?? "";
            }

            // There should be at least 1 url added to the settings file
            ObjectQA.ThrowIfIEnumerableIsEmpty(_urlMap);

            // Determine base url (Precedence: First match against 'default' ELSE first entry in dictionary)
            var defaultNode = xmlDoc.SelectSingleNode("//urls/url[@default='true']");
            if(defaultNode != null)
            {
                _baseUrl = _urlMap[GetXmlNodeAttribute(defaultNode, "key", "")];
            }
            else
            {
                _baseUrl = _urlMap[_urlMap.Keys.First()];
            }

        }

        private XmlDocument GetXmlDocument()
        {
            string filePath = "settings.xml";
            if (!File.Exists(filePath))
            {
                var folderPath = System.IO.Path.GetDirectoryName(
                      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

                filePath = Path.Combine(folderPath, filePath);

            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Cannot find settings file: " + filePath);
            }

            // Load default settings file
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            return xmlDoc;
        }

        public Dictionary<string, string> GetDriverCapabilitiesFromSettingsFile(string key)
        {
            StringQA.ThrowIfNullOrWhiteSpace(key);

            var xmlDoc = GetXmlDocument();
            var retVal = new Dictionary<string, string>();
            string xpath = string.Format("//driverCapabilities/capabilities[@key='{0}']/capability", key);

            var nodes = xmlDoc.SelectNodes(xpath);
            foreach (XmlNode node in nodes)
            {
                string capName = GetXmlNodeAttribute(node, "name", "");
                string capValue = node.InnerText;
                bool isEnvVar = 
                    string.Equals(
                    "true", 
                    GetXmlNodeAttribute(node, "isEnvVar", "false"), 
                    StringComparison.CurrentCultureIgnoreCase);

                if (isEnvVar)
                {
                    capValue = EnvironmentHelper.GetEnvVariable(capValue);
                }

                // Throw an exception if value is not provided
                StringQA.ThrowIfNullOrWhiteSpace(capName);
                StringQA.ThrowIfNullOrWhiteSpace(capValue);

                retVal[capName] = capValue;
                
            }

            return retVal;
        }

        private string GetXmlNodeAttribute(XmlNode node, string name, string defaultValue)
        {
            ObjectQA.ThrowIfNull(node);
            ObjectQA.ThrowIfNull(defaultValue);

            string retVal = null;
            XmlAttribute attribute = node.Attributes[name];

            if(attribute != null) 
            {
                retVal = attribute.InnerText;
            }
            return retVal ?? defaultValue;

        }

        public static Settings Instance { get { return _instance; } }

        public int DefaultTimeOut
        {
            get { return _timeOut; }
            set 
            { 
                if(value < 1)
                {
                    throw new ArgumentOutOfRangeException("value", "Must be > 0");
                }
                _timeOut = value; 
            }
        }

        public string GetUrlFromKey(string key)
        {
            ObjectQA.ThrowIfIEnumerableDoesNotContainValue(_urlMap.Keys.ToList(), key);
            return _urlMap[key];
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set 
            {
                StringQA.ThrowIfNullOrWhiteSpace(value);

                if(_urlMap.ContainsKey(value))
                {
                    // set the url using the name/key from the settings file
                    _baseUrl = _urlMap[value];
                    _log.Debug(string.Format("BaseUrl (set) key := [{0}] url := [{1}]", value, _baseUrl));
                }
                else
                {
                    // An explicit url has been provided
                    _baseUrl = value;
                    _log.Debug("BaseUrl (set): " + _baseUrl);
                }

            }
        }
        
        


        // dev.kleenheat.com.au
        // test.kleenheat.com.au
        // blue.kleenheat.com.au
        // green.kleenheat.com.au
        // www.kleenheat.com.au

    }
}
