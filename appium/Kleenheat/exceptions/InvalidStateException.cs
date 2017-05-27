using au.com.kleenheat.se.qa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace au.com.kleenheat.se.exceptions
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException(string message) : base(message)
        {
        }

    }
}
