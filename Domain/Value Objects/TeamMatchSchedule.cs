using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects
{
    public class TeamMatchSchedule
    {
        private Dictionary<Guid, List<Guid>> matchSchedule;

        public IEnumerable<Match> this[Guid seriesId]
        {
            get
            {
                List<Match> schedule = new List<Match>();
                List<Guid> matchIds;
                if (this.matchSchedule.TryGetValue(seriesId, out matchIds))
                {
                    foreach (var id in matchIds)
                    {
                        var match = DomainService.FindMatchById(id);
                        schedule.Add(match);
                    }
                    return schedule;
                }
                else
                {
                    throw new SeriesMissingException($"Could not find any match schedule " +
                        $"with the ID '{seriesId}'");
                }
                
            }
        }

        //public IEnumerable<IEnumerable<Match>> this[params Guid[] seriesIds]
        //{
        //    get
        //    {
        //        List<Guid> matchScheduleIds = new List<Guid>();
        //        List<List<Match>> allschedules = new List<List<Match>>();
        //        foreach (var seriesId in seriesIds)
        //        {
        //            if (this.matchSchedule.TryGetValue(seriesId, out matchScheduleIds))
        //            {
        //                foreach (var matchId in matchScheduleIds)
        //                {
                            
        //                }
        //            }
        //        }
        //    }
        //}

        public TeamMatchSchedule()
        {
            this.matchSchedule = new Dictionary<Guid, List<Guid>>();
        }

        public void AddSeries(Series series)
        {
            this.matchSchedule.Add(series.Id, series.Schedule);
        }

        public void UpdateSchedule(Series series, List<Guid> newSchedule)
        {
            List<Guid> currentSchedule;
            if (this.matchSchedule.TryGetValue(series.Id, out currentSchedule))
            {
                currentSchedule = newSchedule;
            }
            else
            {
                throw new SeriesMissingException($"Could not find any match schedule " +
                        $"with the ID '{series.Id}'");
            }
        }
    }
}