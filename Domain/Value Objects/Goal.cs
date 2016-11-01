using Domain.Interfaces;
using DomainTests.Entities;

namespace Domain.Value_Objects
{
    public class Goal : IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Player Player { get; } // The player who made the goal.

        public Goal(MatchMinute matchMinute, Player player)
        {
            this.MatchMinute = matchMinute;
            this.Player = player;
        }
    }
}