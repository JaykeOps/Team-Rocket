using Domain.Interfaces;
using DomainTests.Entities;

namespace Domain.Value_Objects
{
    class Card : IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Player Player { get; } // The player who got the card.
        public CardType CardType { get; } // Enum: Yellow or Red.

        public Card(MatchMinute matchMinute, Player player, CardType cardType)
        {
            this.MatchMinute = matchMinute;
            this.Player = player;
            this.CardType = cardType;
        }
    }
}
