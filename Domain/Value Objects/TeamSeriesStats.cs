using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamSeriesStats : IPresentableTeamSeriesStats
    {
        private Dictionary<Guid, TeamStats> seriesStats;

        public TeamStats this[Guid seriesId]
        {
            get
            {
                TeamStats teamStats;
                if (this.seriesStats.TryGetValue(seriesId, out teamStats))
                {
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
                        seriesStats.Add(teamStats);
                    }
                }
                return seriesStats;
            }
        }

        public TeamSeriesStats()
        {
            this.seriesStats = new Dictionary<Guid, TeamStats>();
        }

        public void AddSeries(Series series, Guid teamId)
        {
            this.seriesStats.Add(series.Id, new TeamStats(series.Id, teamId));
        }

        public void RemoveSeries(Series series)
        {
            this.seriesStats.Remove(series.Id);
        }
    }
}