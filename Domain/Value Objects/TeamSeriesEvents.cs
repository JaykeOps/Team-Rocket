using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamSeriesEvents : IPresentableTeamSeriesEvents
    {
        private Dictionary<Guid, TeamEvents> seriesEvents;

        public IPresentableTeamEvents this[Guid seriesId]
        {
            get
            {
                TeamEvents teamEvents;
                if (seriesEvents.TryGetValue(seriesId, out teamEvents))
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

        public IEnumerable<IPresentableTeamEvents> this[params Guid[] seriesIds]
        {
            get
            {
                var seriesEvents = new List<TeamEvents>();
                TeamEvents teamEvent;
                foreach (var seriesId in seriesIds)
                {
                    if (this.seriesEvents.TryGetValue(seriesId, out teamEvent))
                    {
                        seriesEvents.Add(teamEvent);
                    }
                }
                return seriesEvents;
            }
        }

        public Dictionary<Guid, TeamEvents> SeriesEvents
        {
            get
            {
                return this.seriesEvents;
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