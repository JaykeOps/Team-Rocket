using Domain.Entities;
using Domain.Repositories;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Repositories
{
    [TestClass]
    public class SeriesRepositoryTests
    {
        private DummySeries dummySeries;
        private Series seriesDummy;
        private Series seriesDummyDuplicate;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
            this.seriesDummy = this.dummySeries.SeriesDummy;
            this.seriesDummyDuplicate = new Series(this.seriesDummy.MatchDuration,
                this.seriesDummy.NumberOfTeams, this.seriesDummy.SeriesName)
            {
                Id = this.seriesDummy.Id
            };
        }

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
            var series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan"));
            var series2 = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan"));
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

        [TestMethod]
        public void TryGetSeriesWillReplaceRepositorySeriesWithNewSeriesIfIdsAreEqual()
        {
            var seriesInRepository = SeriesRepository.instance.GetAll().First(x => x.Id == this.seriesDummy.Id);
            Assert.AreEqual(this.seriesDummy, seriesInRepository);
            Assert.AreNotEqual(this.seriesDummy, this.seriesDummyDuplicate);
            Assert.AreEqual(this.seriesDummy.Id, this.seriesDummyDuplicate.Id);
            this.seriesDummyDuplicate.SeriesName = new SeriesName("Division 7");
            Assert.AreNotEqual(this.seriesDummyDuplicate.SeriesName, this.seriesDummy.SeriesName);
            SeriesRepository.instance.AddSeries(this.seriesDummyDuplicate);
            seriesInRepository = SeriesRepository.instance.GetAll().First(x => x.Id == this.seriesDummy.Id);
            Assert.AreEqual(this.seriesDummyDuplicate.SeriesName, seriesInRepository.SeriesName);
            Assert.AreEqual(this.seriesDummyDuplicate, seriesInRepository);
        }
    }
}