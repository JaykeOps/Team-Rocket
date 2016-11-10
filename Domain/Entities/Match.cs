using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Net.Sockets;

namespace Domain.Entities
{
    public class Match : IGameDuration
    {
        public Guid Id { get; }
        public ArenaName Location { get; set; }
        public MatchDuration MatchDuration { get;}
        public MatchDateAndTime MatchDate { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }

        public Match(ArenaName location, Guid homeTeam, Guid awayTeam, Series series, MatchDateAndTime date)
        {
            this.Id = Guid.NewGuid();
            this.Location = location;
            this.MatchDuration = series.MatchDuration;
            this.MatchDate = date;
            this.HomeTeamId = homeTeam;
            this.AwayTeamId = awayTeam;
        }

        public override string ToString()
        {
            return $"Location: {this.Location} Time:{this.MatchDate} Hometeam: {this.HomeTeamId} Awayteam: {this.AwayTeamId}";
        }
    }
}