using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;
using Domain.Services;

namespace Domain.Repositories
{
    public sealed class SeriesRepository
    {
        private List<Series> series;

        public static readonly SeriesRepository instance = new SeriesRepository();

        private SeriesRepository()
        {


            

            this.series= new List<Series>();
            Load();
        }

        public void Load()
        {
            var series = new Series(new MatchDuration(new TimeSpan(90 * 6000000000 / 10)), new NumberOfTeams(16), "Allsvenskan");
            foreach (var team in DomainService.GetAllTeams())
            {
                series.TeamIds.Add(team.Id);
            }
            series.Schedule= DomainService.ScheduleGenerator(series.Id);
            this.series.Add(series);
        }

        public IEnumerable<Series> GetAll()
        {
            return this.series;
        }

        public void AddSeries(Series newSeries)
        {
            this.series.Add(newSeries);
        }
    }
}