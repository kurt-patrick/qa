using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Repository
{
    public class GenericFolder
    {
        public string Name { get; private set; }
        public GenericFolder()
        {
        }

        public GenericFolder(string name)
        {
            QA.Utils.StringUtil.ThrowIfNullOrWhiteSpace(name);
            Name = name;
        }

    }
}
