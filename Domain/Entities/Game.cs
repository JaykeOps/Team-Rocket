using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using Domain.CustomExceptions;

namespace Domain.Entities
{
    public class Game : IGameDuration
    {
        public Guid Id { get; }
        public MatchDuration MatchDuration { get; }
        public Guid HomeTeamId { get; }
        public Guid AwayTeamId { get; }
        public GameProtocol Protocol { get; }

        public Game(MatchDuration matchDuration, Guid homeTeamId, Guid awayTeamId)
        {
            if (homeTeamId != awayTeamId)
            {
                this.Id = Guid.NewGuid();
                this.MatchDuration = matchDuration;
                this.HomeTeamId = homeTeamId;
                this.AwayTeamId = awayTeamId;
                this.Protocol = new GameProtocol(this.HomeTeamId, this.AwayTeamId);
            }
            else
            {
                throw new GameContainsSameTeamTwiceException();
            }
        }
    }
}