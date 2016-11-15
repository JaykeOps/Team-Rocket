using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using Domain.CustomExceptions;

namespace Domain.Entities
{
    public class Game : IGameDuration
    {
        public Guid Id { get; }
        public Guid SeriesId { get; }
        public MatchDuration MatchDuration { get; }
        public Guid HomeTeamId { get; }
        public List<Guid>HomeTeamSquad { get;}
        public Guid AwayTeamId { get; }
        public List<Guid> AwayTeamSquad { get; }
        public GameProtocol Protocol { get; }
        public GameProtocol Protocol { get; set; }

        public Game(Match match)
        {
            if (match.HomeTeamId != match.AwayTeamId)
            {
                this.Id = Guid.NewGuid();
                this.MatchDuration = match.MatchDuration;
                this.HomeTeamId = match.HomeTeamId;
                this.AwayTeamId = match.AwayTeamId;
                this.MatchDuration = matchDuration;
                this.SeriesId = seriesId;
                this.HomeTeamId = homeTeamId;
                this.HomeTeamSquad= new List<Guid>();
                this.AwayTeamId = awayTeamId;
                this.AwayTeamSquad=new List<Guid>();
                this.Protocol = new GameProtocol(this.HomeTeamId, this.AwayTeamId);
            }
            else
            {
                throw new GameContainsSameTeamTwiceException();
            }
        }
    }
}