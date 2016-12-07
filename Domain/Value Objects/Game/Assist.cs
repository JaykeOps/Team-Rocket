using Domain.Interfaces;
using Domain.Services;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Assist : ValueObject<Assist>, IGameEvent
    {
        private const string eventType = "Assist";
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; }
        public Guid TeamId { get; }

        public string EventType => eventType;

        public Assist(MatchMinute matchMinute, Guid teamId, Guid playerId)
        {
            this.MatchMinute = matchMinute;
            this.TeamId = teamId;
            this.PlayerId = playerId;
        }

        public override string ToString()
        {
            return $"{eventType}, {DomainService.FindPlayerById(this.PlayerId)}, {DomainService.FindTeamById(this.TeamId)}, {MatchMinute}";
        }
    }
}