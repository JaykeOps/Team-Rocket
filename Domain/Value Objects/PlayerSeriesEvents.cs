using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class PlayerSeriesEvents : IPresentablePlayerSeriesEvents
    {
        private Dictionary<Guid, PlayerEvents> seriesEvents;

        public PlayerEvents this[Guid seriesId]
        {
            get
            {
                PlayerEvents playerEvents;
                if (this.seriesEvents.TryGetValue(seriesId, out playerEvents))
                {
                    return playerEvents;
                }
                else
                {
                    throw new SeriesMissingException("A player specific events record for " +
                        $"a series with id '{seriesId}' could not be found!");
                }
            }
        }

        public IEnumerable<PlayerEvents> this[params Guid[] seriesIds]
        {
            get
            {
                var seriesEvents = new List<PlayerEvents>();
                PlayerEvents playerEvents;
                foreach (var seriesId in seriesIds)
                {
                    if (this.seriesEvents.TryGetValue(seriesId, out playerEvents))
                    {
                        seriesEvents.Add(playerEvents);
                    }
                }
                return seriesEvents;
            }
        }

        public PlayerSeriesEvents()
        {
            this.seriesEvents = new Dictionary<Guid, PlayerEvents>();
        }

        public void AddSeries(Series series, Guid teamId, Guid playerId) //Will be internal
        {
            this.seriesEvents.Add(series.Id, new PlayerEvents(series.Id, teamId, playerId));
        }

        public void RemoveSeries(Series series) //Will be internal
        {
            this.seriesEvents.Remove(series.Id);
        }
    }
}