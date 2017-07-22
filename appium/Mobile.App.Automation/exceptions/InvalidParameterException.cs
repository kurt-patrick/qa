using System;

namespace KPE.Mobile.App.Automation.Exceptions
{
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string message) : base(message)
        {
        }

    }
}
