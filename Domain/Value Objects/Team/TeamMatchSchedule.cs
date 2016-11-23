using Domain.Entities;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Value_Objects
{
    [Serializable]
    public class TeamMatchSchedule
    {
        private Guid teamId;

        public IEnumerable<Match> this[Guid seriesId]
        {
            get
            {
                var seriesSchedule = DomainService.GetTeamScheduleForSeries(seriesId, this.teamId).ToList();
                var matches = new List<Match>();

                foreach (var matchId in seriesSchedule)
                {
                    matches.Add(DomainService.FindMatchById(matchId));
                }
                return matches;
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

        public TeamMatchSchedule(Guid teamId)
        {
            this.teamId = teamId;
            //this.teamsSeries = new List<Guid>();
        }

        //public void AddSeries(Series series)
        //{
        //    this.teamsSeries.Add(series.Id);
        //}

        private IEnumerable<Match> GetMatchScheduleForSeries(Guid seriesId)
        {
            var series = DomainService.FindSeriesById(seriesId);
            var allSeriesMatches = new List<Match>();
            foreach (var pair in series.Schedule)
            {
                allSeriesMatches.AddRange(pair.Value);
            }

            return allSeriesMatches.Where(x => x.HomeTeamId == this.teamId || x.AwayTeamId == this.teamId);
        }
    }
}