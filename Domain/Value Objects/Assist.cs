using Domain.Interfaces;
using DomainTests.Entities;
using System;

namespace Domain.Value_Objects
{
    public class Assist : ValueObject<Assist>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Player Player { get; } // The player who made the goal-giving pass.

        public Assist(MatchMinute matchMinute, Player player)
        {
            this.MatchMinute = matchMinute;
            this.Player = player;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Assist))
            {
                return false;
            }
            else
            {
                Assist assistObject = (Assist)obj;
                return (this.MatchMinute.Equals(assistObject.MatchMinute) && this.Player.Id == assistObject.Player.Id) ? true : false; // Necessary to override MatchMinute.Equals()!
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    
    }
}