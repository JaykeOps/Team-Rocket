using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace Domain.Entities
{
    public class Series : IGameDuration
    {
        public Guid Id { get; }
        public string SeriesName { get; set; }
        public NumberOfTeams NumberOfTeams { get; }
        public HashSet<Guid> TeamIds { get; }
        public MatchDuration MatchDuration { get; }
        public List<Guid> Schedule { get; }

        public Series(MatchDuration matchDuration, NumberOfTeams numberOfTeams, string seriesName)
        {
            this.Id = Guid.NewGuid();
            this.SeriesName = seriesName;
            this.NumberOfTeams = numberOfTeams;
            this.MatchDuration = matchDuration;
            this.TeamIds = new HashSet<Guid>();
            this.Schedule = new List<Guid>();
        }
    }
}