using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Series : IGameDuration
    {
        public Guid Id { get; }
        public int NumberOfTeams { get; }
        public MatchDuration MatchDuration { get; }
        public List<Match> Schedule { get; }

        public Series(MatchDuration matchDuration, int numberOfTeams)
        {
            this.Id = Guid.NewGuid();
            this.NumberOfTeams = numberOfTeams;
            this.MatchDuration = matchDuration;
            this.Schedule = new List<Match>();
        }
    }
}