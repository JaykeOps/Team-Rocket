using System;
using System.Runtime.Serialization;

namespace Domain.CustomExceptions
{
    [Serializable]
    public class GameContainsSameTeamTwiceException : Exception
    {
        public GameContainsSameTeamTwiceException()
        {
        }

        public GameContainsSameTeamTwiceException(string message) : base(message)
        {
        }

        public GameContainsSameTeamTwiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public GameContainsSameTeamTwiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}