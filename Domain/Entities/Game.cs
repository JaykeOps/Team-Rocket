using Domain.CustomExceptions;
using Domain.Interfaces;
using Domain.Services;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class Game : IGameDuration
    {
        public Guid Id { get; }
        public Guid SeriesId { get; }
        public MatchDuration MatchDuration { get; }
        public Guid HomeTeamId { get; }
        public List<Guid> HomeTeamSquad { get; }
        public Guid AwayTeamId { get; }
        public List<Guid> AwayTeamSquad { get; }
        public ArenaName Location { get; set; }
        public MatchDateAndTime MatchDate { get; set; }
        public GameProtocol Protocol { get; }

        public Game(Match match)
        {
            if (match.HomeTeamId != match.AwayTeamId)
            {
                this.Id = Guid.NewGuid();
                this.MatchDuration = match.MatchDuration;
                this.HomeTeamId = match.HomeTeamId;
                this.AwayTeamId = match.AwayTeamId;
                this.SeriesId = match.SeriesId;
                this.Location = match.Location;
                this.MatchDate = match.MatchDate;
                this.HomeTeamSquad = new List<Guid>();
                this.AwayTeamSquad = new List<Guid>();
                this.Protocol = new GameProtocol(this.HomeTeamId, this.AwayTeamId);
            }
            else
            {
                throw new GameContainsSameTeamTwiceException();
            }
        }

        public override string ToString()
        {
            return $"Location: {this.Location} Time: {this.MatchDate} Hometeam: {DomainService.FindTeamById(this.HomeTeamId)} Awayteam: {DomainService.FindTeamById(this.AwayTeamId)}";
        }
    }
}