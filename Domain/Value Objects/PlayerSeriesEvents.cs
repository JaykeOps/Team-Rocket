using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class PlayerSeriesEvents : IPresentablePlayerSeriesEvents
    {
        private Dictionary<Guid, PlayerEvents> seriesEvents;

        public IPresentablePlayerEvents this[Guid seriesId]
        {
            get { return this.seriesEvents[seriesId]; }
        }

        public IEnumerable<IPresentablePlayerEvents> this[params Guid[] seriesIds]
        {
            get
            {
                var seriesEvents = new List<PlayerEvents>();
                PlayerEvents playerEvents;
                foreach (var seriesId in seriesIds)
                {
                    this.seriesEvents.TryGetValue(seriesId, out playerEvents);
                    if (playerEvents != null)
                    {
                        seriesEvents.Add(playerEvents);
                    }
                }
                return seriesEvents;
            }
        }

        public Dictionary<Guid, PlayerEvents> SeriesEvents //Will be internal
        {
            get
            {
                return this.seriesEvents;
            }
        }
        public PlayerSeriesEvents()
        {
            this.seriesEvents = new Dictionary<Guid, PlayerEvents>();
        }
    }
}