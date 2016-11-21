using System;
using System.Runtime.Serialization;

namespace Domain.CustomExceptions
{
    [Serializable]
    public class GameAlreadyAddedException : Exception
    {
        public GameAlreadyAddedException()
        {
        }

        public GameAlreadyAddedException(string message) : base(message)
        {
        }

        public GameAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public GameAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}