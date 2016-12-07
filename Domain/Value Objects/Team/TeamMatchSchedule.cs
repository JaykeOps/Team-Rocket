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
        private Dictionary<Guid, List<Match>> allMatches;

        public IEnumerable<Match> this[Guid seriesId]
        {
            get
            {
                List<Match> matches;
                if (this.allMatches.TryGetValue(seriesId, out matches))
                {
                    this.UpdateSchedule(seriesId);
                    return matches;
                }
                else
                {
                    throw new SeriesMissingException($"Series with Id {seriesId} could not be found!");
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
        public void UpdateSchedule(Guid seriesId)
        {
            this.allMatches[seriesId] =
                DomainService.GetAllMatches().Where(x => x.SeriesId == seriesId && x.HomeTeamId
                == this.teamId || x.AwayTeamId
                == this.teamId).ToList();
        }

        public TeamMatchSchedule(Guid teamId)
        {
            this.teamId = teamId;
            this.allMatches = new Dictionary<Guid, List<Match>>();
        }

        public void AddSeries(Series series)
        {
            this.allMatches.Add(series.Id, new List<Match>());
        }
    }
}