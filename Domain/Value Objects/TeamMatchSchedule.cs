using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamMatchSchedule
    {
        private Dictionary<Guid, List<Guid>> matchSchedule;

        public IEnumerable<Match> this[Guid seriesId]
        {
            get
            {
                var matches = new List<Match>();
            }
        }
    }
}