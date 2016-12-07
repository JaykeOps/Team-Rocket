using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class AggregatedPlayerStats
    {
        private readonly Dictionary<Guid, PlayerStats> allSeriesStats;

        public IReadOnlyDictionary<Guid, PlayerStats> AllStats
        {
            get
            {
                foreach (var playerStats in this.allSeriesStats.Values)
                {
                    playerStats.UpdateAllStats();
                }
                return this.allSeriesStats;
            }
        }

        public PlayerStats this[Guid seriesId]
        {
            get
            {
                PlayerStats playerStats;
                if (this.allSeriesStats.TryGetValue(seriesId, out playerStats))
                {
                    playerStats.UpdateAllStats();
                    return playerStats;
                }
                else
                {
                    throw new SeriesMissingException("A player specific statistic record for " +
                        $"a series with id '{seriesId}' could not be found!");
                }
            }
        }

        public IEnumerable<PlayerStats> this[params Guid[] seriesIds]
        {
            get
            {
                var seriesStats = new List<PlayerStats>();
                foreach (var seriesId in seriesIds)
                {
                    PlayerStats playerStats;
                    if (this.allSeriesStats.TryGetValue(seriesId, out playerStats))
                    {
                        playerStats.UpdateAllStats();
                        seriesStats.Add(playerStats);
                    }
                }
                return seriesStats;
            }
        }

        public AggregatedPlayerStats()
        {
            this.allSeriesStats = new Dictionary<Guid, PlayerStats>();
        }

        public void AddSeries(Series series, Guid teamId, Player player)
        {
            this.allSeriesStats.Add(series.Id, new PlayerStats(series.Id, teamId, player));
        }

        public void RemoveSeries(Series series)
        {
            this.allSeriesStats.Remove(series.Id);
        }
    }
}