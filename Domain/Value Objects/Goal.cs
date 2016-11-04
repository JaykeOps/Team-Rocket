using Domain.Interfaces;
using DomainTests.Entities;
using System;

namespace Domain.Value_Objects
{
    public class Goal : ValueObject<Goal>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Player Player { get; } // The player who made the goal.

        public Goal(MatchMinute matchMinute, Player player)
        {
            if (matchMinute == null || player == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                this.MatchMinute = matchMinute;
                this.Player = player;
            }
        }


        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

       
    }
}