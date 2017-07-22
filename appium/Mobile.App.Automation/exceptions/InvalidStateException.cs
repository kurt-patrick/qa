using System;

namespace KPE.Mobile.App.Automation.Exceptions
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException(string message) : base(message)
        {
        }

    }
}
