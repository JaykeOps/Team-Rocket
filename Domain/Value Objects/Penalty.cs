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

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Penalty))
            {
                return false;
            }
            else
            {
                Penalty penaltyObject = (Penalty)obj;
                return (this.MatchMinute.Equals(penaltyObject.MatchMinute) && this.Player.Id == penaltyObject.Player.Id) ? true : false; // Necessary to override MatchMinute.Equals()!
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}