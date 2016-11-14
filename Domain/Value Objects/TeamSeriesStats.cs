﻿using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamSeriesStats
    {
        private Dictionary<Guid, TeamStats> seriesStats;

        public TeamStats this[Guid seriesId]
        {
            get
            {
                TeamStats teamStats;
                this.seriesStats.TryGetValue(seriesId, out teamStats);
                if (teamStats != null)
                {
                    return teamStats;
                }
                else
                {
                    throw new SeriesMissingException("Team does not contain any statistic record for " +
                        $"a series with id '{seriesId}'.");
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
                    this.seriesStats.TryGetValue(seriesId, out teamStats);
                    if (teamStats != null)
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