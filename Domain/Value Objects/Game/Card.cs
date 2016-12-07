using Domain.Interfaces;
using Domain.Services;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Card : ValueObject<Card>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; }
        public Guid TeamId { get; }
        public CardType CardType { get; }

        public string EventType => this.CardType.Equals(CardType.Red) ? "Red card" : "Yellow card";

        public Card(MatchMinute matchMinute, Guid teamId, Guid playerId, CardType cardType)
        {
            this.MatchMinute = matchMinute;
            this.TeamId = teamId;
            this.PlayerId = playerId;
            this.CardType = cardType;
        }

        public override string ToString()
        {
            return $"{EventType}, {DomainService.FindPlayerById(this.PlayerId)}, {DomainService.FindTeamById(this.TeamId)}, {MatchMinute}";
        }
    }
}