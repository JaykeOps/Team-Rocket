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
            testSerieOne.TeamIds.Add(Guid.NewGuid());
            testSerieOne.TeamIds.Add(Guid.NewGuid());
            testSerieOne.TeamIds.Add(Guid.NewGuid());
            testSerieOne.TeamIds.Add(Guid.NewGuid());
        }

        [TestMethod]
        public void AddSeries()
        {
            Assert.IsFalse(seriesService.GetAll().Contains(testSerieOne));
            seriesService.AddSeries(testSerieOne);
            Assert.IsTrue(seriesService.GetAll().Contains(testSerieOne));
        }

        [TestMethod]
        public void GetAllSeriesesIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(seriesService.GetAll(), typeof(IEnumerable<Series>));
        }


        [TestMethod]
        public void FindSeriesByIdIsWorking()
        {
            Assert.IsFalse(seriesService.FindById(testSerieOne.Id) == testSerieOne);
            seriesService.AddSeries(testSerieOne);
            Assert.IsTrue(seriesService.FindById(testSerieOne.Id) == testSerieOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SeriesCanOnlyBeAddedToDbIfTeamIdsCountIsEqualToNumberOfTeams()
        {
            testSerieOne.TeamIds.Add(Guid.NewGuid());
            seriesService.AddSeries(testSerieOne);
        }
    }
}