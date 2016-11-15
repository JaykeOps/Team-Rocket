using Domain.Interfaces;
using System;

namespace Domain.Value_Objects
{
    public class Goal : ValueObject<Goal>, IGameEvent
    {
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
    }
}