using Domain.Interfaces;
using System;
using Domain.Services;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Goal : ValueObject<Goal>, IGameEvent
    {
        public const string EventName = "Goal";
        public MatchMinute MatchMinute { get; }
        public Guid TeamId { get; set; }
        public Guid PlayerId { get; }

        
       
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
            return $"{EventName}, {DomainService.FindPlayerById(this.PlayerId)}, {DomainService.FindTeamById(this.TeamId)}, {MatchMinute}";
        }
    }
}