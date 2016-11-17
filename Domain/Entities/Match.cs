using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using Domain.Services;

namespace Domain.Entities
{
    [Serializable]
    public class Match : IGameDuration
    {
        public Guid Id { get; }
        public ArenaName Location { get; set; }
        public MatchDuration MatchDuration { get; }
        public MatchDateAndTime MatchDate { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public Guid SeriesId { get; set; }

        public Match(ArenaName location, Guid homeTeam, Guid awayTeam, Series series, MatchDateAndTime date)
        {
            this.Id = Guid.NewGuid();
            this.Location = location;
            this.MatchDuration = series.MatchDuration;
            this.MatchDate = date;
            this.HomeTeamId = homeTeam;
            this.AwayTeamId = awayTeam;
            this.SeriesId = series.Id;
        }

        public Match(ArenaName location, Guid homeTeam, Guid awayTeam, Series series)
        {
            this.Id = Guid.NewGuid();
            this.Location = location;
            this.MatchDuration = series.MatchDuration;
            this.HomeTeamId = homeTeam;
            this.AwayTeamId = awayTeam;
        }

        public override string ToString()
        {
            return $"Location: {this.Location} Time:{this.MatchDate} Hometeam: {DomainService.FindTeamById(this.HomeTeamId)} Awayteam: {DomainService.FindTeamById(this.AwayTeamId)}";
        }
    }
}