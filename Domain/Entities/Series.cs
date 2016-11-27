using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class Series : IGameDuration
    {
        public Guid Id { get; set; } //TODO: Set is only for tests!
        public string SeriesName { get; set; } //TODO: MAKE SeriesName a value object... No validation atm?
        public NumberOfTeams NumberOfTeams { get; }
        public HashSet<Guid> TeamIds { get; }
        public MatchDuration MatchDuration { get; }
        public Dictionary<int, List<Match>> Schedule { get; set; }

        public Series(MatchDuration matchDuration, NumberOfTeams numberOfTeams, string seriesName)
        {
            this.Id = Guid.NewGuid();
            this.SeriesName = seriesName;
            this.NumberOfTeams = numberOfTeams;
            this.MatchDuration = matchDuration;
            this.TeamIds = new HashSet<Guid>();
            this.Schedule = new Dictionary<int, List<Match>>();
        }
    }
}