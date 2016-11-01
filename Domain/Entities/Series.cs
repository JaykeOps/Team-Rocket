using System;
using Domain.Value_Objects;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Series : IGameDuration
    {
        
        public Guid Id { get; }
        public int NumberOfTeams { get; }
        public MatchDuration MatchDuration { get; }
        public List<LeagueTableStats> LeagueTable { get; }
        public List<Match> Schedule { get; }


        public Series(MatchDuration matchDuration, int numberOfTeams)
        {
            this.Id = Guid.NewGuid();
            this.NumberOfTeams = numberOfTeams;
            this.MatchDuration = matchDuration;
            this.LeagueTable = new List<LeagueTableStats>();
            this.Schedule = new List<Match>();
        }
    }
}