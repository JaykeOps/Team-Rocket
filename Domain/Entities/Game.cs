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
        public GameProtocol Protocol { get; set; }

        public Game(Match match)
        {
            if (match.HomeTeamId != match.AwayTeamId)
            {
                this.Id = Guid.NewGuid();
                this.MatchDuration = match.MatchDuration;
                this.HomeTeamId = match.HomeTeamId;
                this.AwayTeamId = match.AwayTeamId;
                this.Protocol = new GameProtocol(this.HomeTeamId, this.AwayTeamId);
            }
            else
            {
                throw new GameContainsSameTeamTwiceException();
            }
        }
    }
}