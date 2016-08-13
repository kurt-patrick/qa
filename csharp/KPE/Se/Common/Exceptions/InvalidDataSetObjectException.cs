using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Exceptions
{
    class InvalidDataSetObjectException : Exception
    {
        public InvalidDataSetObjectException() : base()
        {
        }

        public InvalidDataSetObjectException(string message)
            : base(message)
        {
        }
    }
}
