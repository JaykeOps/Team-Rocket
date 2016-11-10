using Domain.Interfaces;
using System;

namespace Domain.Value_Objects
{
    public class Card : ValueObject<Card>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; } // The player who got the card.
        public CardType CardType { get; } // Enum: Yellow or Red.

        public Card(MatchMinute matchMinute, Guid playerId, CardType cardType)
        {
            this.MatchMinute = matchMinute;
            this.PlayerId = playerId;
            this.CardType = cardType;
        }
    }
}