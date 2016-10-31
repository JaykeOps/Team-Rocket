using Domain.Interfaces;
using Domain.Value_Objects;
using System;

namespace Domain.Entities
{
    public class Match : IGameDuration
    {
        public Guid Id { get; }
        public ArenaName Location { get; }
        public MatchDuration MatchDuration { get; }
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }

        public Match(ArenaName location, Team homeTeam, Team awayTeam)
        {
            this.Id = new Guid();
            this.Location = location;
            //this.MatchDuration=series.getMatchduration;
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
        }

        public override string ToString()
        {
            return $"Location: {this.Location} Hometeam: {this.HomeTeam} Awayteam: {this.AwayTeam}";
        }
    }
}