using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class AggregatedTeamStats
    {
        private Dictionary<Guid, TeamStats> seriesStats;
        private Guid teamId;

        public IReadOnlyDictionary<Guid, TeamStats> AllStats
        {
            get
            {
                foreach (var teamStats in this.seriesStats.Values)
                {
                    teamStats.UpdateAllStats();
                }
                return this.seriesStats;
            }
        }

        public TeamStats this[Guid seriesId]
        {
            get
            {
                TeamStats teamStats;
                if (this.seriesStats.TryGetValue(seriesId, out teamStats))
                {
                    teamStats.UpdateAllStats();
                    return teamStats;
                }
                else
                {
                    throw new SeriesMissingException("A team specific statistic record for " +
                        $"a series with id '{seriesId}' could not be found!");
                }
            }
        }

        public IEnumerable<TeamStats> this[params Guid[] seriesIds]
        {
            get
            {
                TeamStats teamStats;
                var seriesStats = new List<TeamStats>();
                foreach (var seriesId in seriesIds)
                {
                    if (this.seriesStats.TryGetValue(seriesId, out teamStats))
                    {
                        teamStats.UpdateAllStats();
                        seriesStats.Add(teamStats);
                    }
                }
                return seriesStats;
            }
        }

        public AggregatedTeamStats(Guid teamId)
        {
            this.teamId = teamId;
            this.seriesStats = new Dictionary<Guid, TeamStats>();
        }

        public void AddSeries(Series series)
        {
            this.seriesStats.Add(series.Id,
                new TeamStats(series.Id, this.teamId));
        }

        public void RemoveSeries(Series series)
        {
            this.seriesStats.Remove(series.Id);
        }
    }
}