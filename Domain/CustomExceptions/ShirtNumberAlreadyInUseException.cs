using System;
using System.Runtime.Serialization;

namespace Domain.Entities
{
    [Serializable]
    public class ShirtNumberAlreadyInUseException : Exception
    {
        public ShirtNumberAlreadyInUseException()
        {
        }

        public ShirtNumberAlreadyInUseException(string message) : base(message)
        {
        }

        public ShirtNumberAlreadyInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShirtNumberAlreadyInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}