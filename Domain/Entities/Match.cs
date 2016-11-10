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
        public Guid HomeTeamId { get; }
        public Guid AwayTeamId { get; }

        public Match(ArenaName location, Guid homeTeamId, Guid awayTeamId, Series series)
        {
            this.Id = Guid.NewGuid();
            this.Location = location;
            this.MatchDuration = series.MatchDuration;
            this.HomeTeamId = homeTeamId;
            this.AwayTeamId = awayTeamId;
        }

        //public override string ToString() // This function can't be written until we can get access to teams with a function like 'FindTeamById(Guid teamId)'.
        //{
        //    return $"Location: {this.Location} Hometeam: {this.HomeTeam} Awayteam: {this.AwayTeam}";
        //}
    }
}