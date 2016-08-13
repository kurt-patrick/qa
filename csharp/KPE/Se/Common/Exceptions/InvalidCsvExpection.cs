using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Exceptions
{
    public class InvalidCsvExpection : Exception
    {
        public InvalidCsvExpection() : base()
        {
        }

        public InvalidCsvExpection(string message)
            : base(message)
        {
        }
    }
}
