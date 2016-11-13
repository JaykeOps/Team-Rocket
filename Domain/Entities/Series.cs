using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Series : IGameDuration
    {
        public Guid Id { get; }
        public NumberOfTeams NumberOfTeams { get; }
        public MatchDuration MatchDuration { get; }
        public List<Guid> Schedule { get; } //Holds a list of match guid

        public Series(MatchDuration matchDuration, NumberOfTeams numberOfTeams)
        {
            this.Id = Guid.NewGuid();
            this.NumberOfTeams = numberOfTeams;
            this.MatchDuration = matchDuration;
            this.Schedule = new List<Guid>();
        }
    }
}