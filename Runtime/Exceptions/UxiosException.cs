using System;

namespace Uxios.Core.Exceptions
{
    public class UxiosException : Exception
    {
        public object Response { get; }

        public UxiosException(string message, object response = null) : base(message)
        {
            Response = response;
        }
    }
}