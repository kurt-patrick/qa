using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Repository
{
    public class Folder
    {
        public enum LocatorBy
        {
            XPath
        };

        //ClassName,
        //CssSelector,
        //Id,
        //LinkText,
        //Name,
        //PartialLinkText,
        //TagName,

        //public string Name { get; private set; }
        //public Folder()
        //{
        //}
        private LocatorBy  _locatorBy;
        private Folder _parent = null;

        public string LocatorString { get; private set; }

        public static Folder Create(LocatorBy by, string value)
        {
            return new Folder(LocatorBy.XPath, value);
        }

        public Folder(LocatorBy by, string value)
        {
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(value);
            _locatorBy = by;
            LocatorString = value;
        }

        public Folder(LocatorBy by, string value, Folder parent)
        {
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(value);
            _locatorBy = by;
            LocatorString = value;
            _parent = parent;
        }

        public static Folder ByXPath(string xPath)
        {
            return new Folder(LocatorBy.XPath, xPath);
        }

        public static Folder ByXPath(string xPath, Folder parent)
        {
            return new Folder(LocatorBy.XPath, xPath, parent);
        }

        /// <summary>
        /// Sets a reference to the parent object and returns a reference to self so calls can be chained
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public Folder SetParent(Folder parent)
        {
            QA.Utils.ObjectUtil.ThrowIfNull(parent);
            _parent = parent;
            return this;
        }


    }
}
