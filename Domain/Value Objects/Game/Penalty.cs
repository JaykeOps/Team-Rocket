using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;
using System;

namespace Domain.Value_Objects
{
    [Serializable]
    public class Penalty : ValueObject<Penalty>, IGameEvent
    {
        private const string eventType = "Penalty";
        public MatchMinute MatchMinute { get; }
        public bool IsGoal { get; }
        public Guid PlayerId { get; }
        public Guid TeamId { get; }

        public string EventType => eventType;

        public Penalty(MatchMinute matchMinute, Guid playerId, bool isGoal, Game game, Guid teamId)
        {
            this.MatchMinute = matchMinute;
            this.TeamId = teamId;
            this.PlayerId = playerId;
            this.IsGoal = isGoal;

            if (isGoal)
            {
                game.Protocol.Goals.Add(new Goal(matchMinute, teamId, playerId));
            }
        }

        public override string ToString()
        {
            return $"{eventType}, {DomainService.FindPlayerById(this.PlayerId)}, {DomainService.FindTeamById(this.TeamId)}, {MatchMinute}";
        }
    }
}