using System;

namespace KPE.Mobile.App.Automation.Exceptions
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException(string message) : base(message)
        {
        }

        public static void ThrowIfFalse(bool state, string message)
        {
            if(!state)
            {
                throw new InvalidStateException(message);
            }
        }

    }
}
