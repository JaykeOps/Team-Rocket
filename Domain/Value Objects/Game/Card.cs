using Domain.Interfaces;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Card : ValueObject<Card>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; } // The player who got the card.
        public Guid TeamId { get; }
        public CardType CardType { get; } // Enum: Yellow or Red.

      

        public Card(MatchMinute matchMinute, Guid teamId, Guid playerId, CardType cardType)
        {
            this.MatchMinute = matchMinute;
            this.TeamId = teamId;
            this.PlayerId = playerId;
            this.CardType = cardType;
        }

        public override string ToString()
        {
            if (this.CardType.Equals(CardType.Red))
            {
                return "Red card";
            }
            else
            {
                return "Yellow card";
            }
        }
    }
}