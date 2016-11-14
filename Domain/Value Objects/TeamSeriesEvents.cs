using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamSeriesEvents
    {
        private Dictionary<Guid, TeamEvents> seriesEvents;

        public TeamEvents this[Guid seriesId]
        {
            get
            {
                TeamEvents teamEvents;
                seriesEvents.TryGetValue(seriesId, out teamEvents);
                if (teamEvents != null)
                {
                    return teamEvents;
                }
                else
                {
                    throw new SeriesMissingException("A team specific events record for " +
                        $"a series with id '{seriesId}' could not be found!");
                }
            }
        }

        public IEnumerable<TeamEvents> this[params Guid[] seriesIds]
        {
            get
            {
                var seriesEvents = new List<TeamEvents>();
                TeamEvents teamEvent;
                foreach (var seriesId in seriesIds)
                {
                    this.seriesEvents.TryGetValue(seriesId, out teamEvent);
                    if (teamEvent != null)
                    {
                        seriesEvents.Add(teamEvent);
                    }
                }
                return seriesEvents;
            }
        }

        public Dictionary<Guid, TeamEvents> SeriesEvents //Will be internal
        {
            get
            {
                return seriesEvents;
            }
        }

        public TeamSeriesEvents()
        {
            this.seriesEvents = new Dictionary<Guid, TeamEvents>();
        }

        public void AddSeries(Series series, Guid teamId)
        {
            this.seriesEvents.Add(series.Id, new TeamEvents(series.Id, teamId));
        }

        public void RemoveSeries(Series series)
        {
            this.seriesEvents.Remove(series.Id);
        }
    }
}