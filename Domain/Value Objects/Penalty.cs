using Domain.Interfaces;
using DomainTests.Entities;
using System;

namespace Domain.Value_Objects
{
    public class Penalty : ValueObject<Penalty>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Player Player { get; } // The player who shot the penalty.

        public Penalty(MatchMinute matchMinute, Player player)
        {
            this.MatchMinute = matchMinute;
            this.Player = player;
        }

       

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}