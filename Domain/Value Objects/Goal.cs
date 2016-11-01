using Domain.Interfaces;
using DomainTests.Entities;
using System;

namespace Domain.Value_Objects
{
    public class Goal : ValueObject, IGameEvent
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

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Goal))
            {
                return false;
            }
            else
            {
                Goal goalObject = (Goal)obj;
                return (this.MatchMinute.Equals(goalObject.MatchMinute) && this.Player.Id == goalObject.Player.Id) ? true : false; // Necessary to override MatchMinute.Equals()!
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator !=(Goal goalOne, Goal goalTwo)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Goal goalOne, Goal goalTwo)
        {
            throw new NotImplementedException();
        }
    }
}