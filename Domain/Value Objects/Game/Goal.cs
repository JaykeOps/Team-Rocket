using Domain.Interfaces;
using Domain.Services;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Goal : ValueObject<Goal>, IGameEvent
    {
        private const string eventType = "Goal";
        public MatchMinute MatchMinute { get; }
        public Guid TeamId { get; set; }
        public Guid PlayerId { get; }

        public string EventType => eventType;

        public Goal(MatchMinute matchMinute, Guid teamId, Guid playerId)
        {
            if (matchMinute == null || teamId == null || playerId == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                this.MatchMinute = matchMinute;
                this.TeamId = teamId;
                this.PlayerId = playerId;
            }
        }

        public override string ToString()
        {
            return $"{eventType}, {DomainService.FindPlayerById(this.PlayerId)}, {DomainService.FindTeamById(this.TeamId)}, {MatchMinute}";
        }
    }
}