using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
            this.testSerieOne = new Series(new MatchDuration(new TimeSpan(45 * 6000000000 / 10)), new NumberOfTeams(4), "Allsvenskan");
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
        }

        [TestMethod]
        public void AddSeries()
        {
            Assert.IsFalse(this.seriesService.GetAll().Contains(this.testSerieOne));
            this.seriesService.AddSeries(this.testSerieOne);
            Assert.IsTrue(this.seriesService.GetAll().Contains(this.testSerieOne));
        }

        [TestMethod]
        public void GetAllSeriesesIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(this.seriesService.GetAll(), typeof(IEnumerable<Series>));
        }

        [TestMethod]
        public void FindSeriesByIdIsWorking()
        {
            Assert.IsFalse(this.seriesService.FindById(this.testSerieOne.Id) == this.testSerieOne);
            this.seriesService.AddSeries(this.testSerieOne);
            Assert.IsTrue(this.seriesService.FindById(this.testSerieOne.Id) == this.testSerieOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SeriesCanOnlyBeAddedToDbIfTeamIdsCountIsEqualToNumberOfTeams()
        {
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.seriesService.AddSeries(this.testSerieOne);
        }

        [TestMethod]
        public void GetLeagueTablePlacementIsWorking()
        {
            var seriesId = seriesService.GetAll().ElementAt(0).Id;
            var leagueTable = seriesService.GetLeagueTablePlacement(seriesId);
        }
    }
}