using Domain.Entities;
using Domain.Value_Objects;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
    internal sealed class SeriesRepository
    {
        private List<Series> series;

        public static readonly SeriesRepository instance = new SeriesRepository();

        private SeriesRepository()
        {
            this.series = new List<Series>()
            {
                new Series(new MatchDuration(new TimeSpan(90 * 6000000000 / 10)), 16)
            };
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