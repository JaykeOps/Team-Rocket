using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class SeriesService
    {
        private readonly SeriesRepository repository = SeriesRepository.instance;

        public void AddSeries(Series series)
        {
            this.repository.AddSeries(series);
        }

        public IEnumerable<Series> GetAll()
        {
            return this.repository.GetAll();
        }

        public Series FindById(Guid seriesId)
        {
            var allSeries = GetAll();
            return allSeries.ToList().Find(s => s.Id.Equals(seriesId));
        }
    }
}