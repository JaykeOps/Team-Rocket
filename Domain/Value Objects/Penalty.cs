using Domain.Interfaces;
using DomainTests.Entities;

namespace Domain.Value_Objects
{
    public class Penalty : IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Player Player { get; } // The player who shot the penalty.

        public Penalty(MatchMinute matchMinute, Player player)
        {
            this.MatchMinute = matchMinute;
            this.Player = player;
        }
    }
}