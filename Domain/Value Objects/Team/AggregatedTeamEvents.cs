using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    [Serializable]
    public class AggregatedTeamEvents
    {
        private Dictionary<Guid, TeamEvents> seriesEvents;
        private Guid teamId;

        public AggregatedTeamEvents()
        {
        }

        public TeamEvents this[Guid seriesId]
        {
            get
            {
                TeamEvents teamEvents;
                if (this.seriesEvents.TryGetValue(seriesId, out teamEvents))
                {
                    teamEvents.UpdateAllEvents();
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
                    if (this.seriesEvents.TryGetValue(seriesId, out teamEvent))
                    {
                        teamEvent.UpdateAllEvents();
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

        public AggregatedTeamEvents(Guid teamId)
        {
            this.teamId = teamId;
            this.seriesEvents = new Dictionary<Guid, TeamEvents>();
        }

        public void AddSeries(Series series)
        {
            this.seriesEvents.Add(series.Id,
                new TeamEvents(series.Id, this.teamId));
        }

        public void RemoveSeries(Series series)
        {
            this.seriesEvents.Remove(series.Id);
        }
    }
}