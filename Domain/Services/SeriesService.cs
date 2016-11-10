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

        public IEnumerable<Series> FindSeriesById(Guid id)
        {
            var allSeries = GetAll();
            return allSeries.Where(x => x.Id == id);
        }
    }
}