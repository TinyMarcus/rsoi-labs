using System;
using System.Runtime.Serialization;

namespace rsoi.Core.Exceptions
{
    public class rsoiException : Exception
    {
        public rsoiException()
        {
            
        }

        public rsoiException(string? message) : base(message)
        {
            
        }

        public rsoiException(string? message, Exception? innerException) : base(message, innerException)
        {

        }

        public rsoiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}