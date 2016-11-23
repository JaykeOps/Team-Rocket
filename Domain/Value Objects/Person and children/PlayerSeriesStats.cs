using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class PlayerSeriesStats : IPresentablePlayerSeriesStats
    {
        private readonly Dictionary<Guid, PlayerStats> allSeriesStats;

        public PlayerStats this[Guid seriesId]
        {
            get
            {
                PlayerStats playerStats;
                if (this.allSeriesStats.TryGetValue(seriesId, out playerStats))
                {
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
                        seriesStats.Add(playerStats);
                    }
                }
                return seriesStats;
            }
        }

        public PlayerSeriesStats()
        {
            this.allSeriesStats = new Dictionary<Guid, PlayerStats>();
        }

        public void AddSeries(Series series, Guid teamId, Guid playerId)
        {
            this.allSeriesStats.Add(series.Id, new PlayerStats(series.Id, teamId, playerId));
        }

        public void RemoveSeries(Series series)
        {
            this.allSeriesStats.Remove(series.Id);
        }
    }
}