using Domain.Interfaces;
using Domain.Entities;
using System;

namespace Domain.Value_Objects
{
    public class Penalty : ValueObject<Penalty>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; }

        public Penalty(MatchMinute matchMinute, Guid playerId)
        {
            this.MatchMinute = matchMinute;
            this.PlayerId = playerId;
        }
    }
}