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
        public SeriesName SeriesName { get; set; }
        public NumberOfTeams NumberOfTeams { get; }
        public HashSet<Guid> TeamIds { get; }
        public MatchDuration MatchDuration { get; }
        public Dictionary<int, List<Match>> Schedule { get; set; }

        
        public Series(MatchDuration matchDuration, NumberOfTeams numberOfTeams, SeriesName name)
        {
            this.Id = Guid.NewGuid();
            this.SeriesName = name;
            this.NumberOfTeams = numberOfTeams;
            this.MatchDuration = matchDuration;
            this.TeamIds = new HashSet<Guid>();
            this.Schedule = new Dictionary<int, List<Match>>();
        }

        public override string ToString()
        {
            return $"{SeriesName}";
        }
    }
}