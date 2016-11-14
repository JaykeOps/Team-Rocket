using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class PlayerSeriesStats
    {
        private Dictionary<Guid, PlayerStats> seriesStats;

        public PlayerStats this[Guid seriesId]
        {
            get
            {
                return this.seriesStats[seriesId];
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
                    this.seriesStats.TryGetValue(seriesId, out playerStats);
                    if (playerStats != null)
                    {
                        seriesStats.Add(playerStats);
                    }
                }
                return seriesStats;
            }
        }

        public Dictionary<Guid, PlayerStats> SeriesStats //Will be internal
        {
            get
            {
                return seriesStats;
            }
        }
    }
}