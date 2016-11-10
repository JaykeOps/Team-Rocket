using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainTests.Services
{
    [TestClass()]
    public class SeriesServicesTests
    {
        private SeriesService seriesService;
        private Series testSerieOne;

        public SeriesServicesTests()
        {
            this.seriesService = new SeriesService();

            this.testSerieOne = new Series(new MatchDuration(new TimeSpan(45 * 6000000000 / 10)), 16);
        }

        [TestMethod]
        public void AddSeries()
        {
            seriesService.AddSeries(new Series(new MatchDuration(new TimeSpan(90 * 6000000000 / 10)), 16));
        }

        [TestMethod]
        public void GetAllSerieses()
        {
            var seriesList = seriesService.GetAll();
        }

        [TestMethod]
        public void FindSerieById()
        {
            seriesService.AddSeries(testSerieOne);
            var result = seriesService.FindSeriesById(testSerieOne.Id);
        }
    }
}