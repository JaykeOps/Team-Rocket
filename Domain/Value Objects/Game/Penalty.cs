using Domain.Entities;
using Domain.Interfaces;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Penalty : ValueObject<Penalty>, IGameEvent
    {
        public MatchMinute MatchMinute { get; }
        public bool IsGoal { get; }
        public Guid PlayerId { get; }

        public Penalty()
        {
            
        }

        public Penalty(MatchMinute matchMinute, Guid playerId, bool isGoal, Game game, Guid teamId)
        {
            this.MatchMinute = matchMinute;
            this.PlayerId = playerId;
            this.IsGoal = isGoal;

            if (isGoal)
            {
                game.Protocol.Goals.Add(new Goal(matchMinute, teamId, playerId));
            }
        }
    }
}