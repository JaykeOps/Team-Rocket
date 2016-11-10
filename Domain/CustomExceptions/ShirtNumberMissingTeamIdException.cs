using System;
using System.Runtime.Serialization;

namespace Domain.Entities
{
    [Serializable]
    internal class ShirtNumberMissingTeamIdException : Exception
    {
        public ShirtNumberMissingTeamIdException()
        {
        }

        public ShirtNumberMissingTeamIdException(string message) : base(message)
        {
        }

        public ShirtNumberMissingTeamIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShirtNumberMissingTeamIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}