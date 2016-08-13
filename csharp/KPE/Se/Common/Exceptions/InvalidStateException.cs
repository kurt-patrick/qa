using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KPE.Se.Common.Exceptions
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException() : base()
        {
        }

        public InvalidStateException(string message) : base(message)
        {
        }

    }
}
