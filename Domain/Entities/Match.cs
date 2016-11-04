using Domain.Interfaces;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    public class Match : IGameDuration
    {
        public Guid Id { get; }
        public ArenaName Location { get; set; }
        public MatchDuration MatchDuration { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public Match(ArenaName location, Team homeTeam, Team awayTeam, Series series)
        {
            this.Id = new Guid();
            this.Location = location;
            this.MatchDuration = series.MatchDuration;
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
        }

        public override string ToString()
        {
            return $"Location: {this.Location} Hometeam: {this.HomeTeam} Awayteam: {this.AwayTeam}";
        }
    }
}