using Domain.Entities;
using Domain.Repositories;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Repositories
{
    [TestClass]
    public class SeriesRepositoryTests
    {
        [TestMethod]
        public void RepoStateIsTheSame()
        {
            var instance = SeriesRepository.instance;
            var instance2 = SeriesRepository.instance;

            Assert.AreEqual(instance, instance2);
        }

        [TestMethod]
        public void RepoGetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(SeriesRepository.instance.GetAll(), typeof(IEnumerable<Series>));
        }

        [TestMethod]
        public void RepoAddIsWorking()
        {
            var series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");
            var series2 = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");
            SeriesRepository.instance.AddSeries(series);
            var allSeries = SeriesRepository.instance.GetAll();
            Assert.IsTrue(allSeries.Contains(series));
            Assert.IsFalse(allSeries.Contains(series2));
        }

        [TestMethod]
        public void GetAllNotReturningNull()
        {
            Assert.IsNotNull(SeriesRepository.instance.GetAll());
        }

        [TestMethod]
        public void SeriesLoadTest()
        {
            var series = SeriesRepository.instance.GetAll();
            Assert.IsTrue(series.Count() != 0);
        }
    }
}