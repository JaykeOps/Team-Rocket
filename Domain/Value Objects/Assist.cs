using Domain.Interfaces;
using System;

namespace Domain.Value_Objects
{
    public class Assist : ValueObject<Assist>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; }

        public Assist(MatchMinute matchMinute, Guid playerId)
        {
            this.MatchMinute = matchMinute;
            this.PlayerId = playerId;
        }
    }
}