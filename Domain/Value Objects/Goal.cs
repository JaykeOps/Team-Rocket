using Domain.Interfaces;
using System;

namespace Domain.Value_Objects
{
    public class Goal : ValueObject<Goal>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; }

        public Goal(MatchMinute matchMinute, Guid playerId)
        {
            if (matchMinute == null || playerId == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                this.MatchMinute = matchMinute;
                this.PlayerId = playerId;
            }
        }
    }
}