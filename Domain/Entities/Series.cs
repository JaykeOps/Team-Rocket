using Domain.Interfaces;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using Domain.Services;
using System.Security.AccessControl;
using Domain.Helper_Classes;

namespace Domain.Entities
{
    [Serializable]
    public class Series : IGameDuration
    {
        public Guid Id { get; }
        public string SeriesName { get; set; }
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
            this.Schedule=new Dictionary<int, List<Match>>();
            
        }
    }
}