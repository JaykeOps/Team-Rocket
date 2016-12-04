using Domain.Interfaces;
using System;
using Domain.Services;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Assist : ValueObject<Assist>, IGameEvent
    {
        public const string EventType = "Assist";
        public MatchMinute MatchMinute { get; }
        public Guid PlayerId { get; }
        public Guid TeamId { get; }



        public Assist(MatchMinute matchMinute,Guid teamId, Guid playerId)
        {
            this.MatchMinute = matchMinute;
            this.TeamId = teamId;
            this.PlayerId = playerId;
        }

        public override string ToString()
        {
            return $"{EventType}, {DomainService.FindPlayerById(this.PlayerId)}, {DomainService.FindTeamById(this.TeamId)}, {MatchMinute}";
        }
    }
}