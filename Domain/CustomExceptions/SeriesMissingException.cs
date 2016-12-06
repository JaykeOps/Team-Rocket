using System;
using System.Runtime.Serialization;

namespace Domain.Value_Objects
{
    [Serializable]
    public class SeriesMissingException : Exception
    {
        public SeriesMissingException()
        {
        }

        public SeriesMissingException(string message) : base(message)
        {
        }

        public SeriesMissingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SeriesMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}