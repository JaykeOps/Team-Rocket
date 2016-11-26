using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using DomainTests.Test_Dummies;

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
            this.seriesService.Add(this.testSerieOne);
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
            this.seriesService.Add(this.testSerieOne);
            Assert.IsTrue(this.seriesService.FindById(this.testSerieOne.Id) == this.testSerieOne);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SeriesCanOnlyBeAddedToDbIfTeamIdsCountIsEqualToNumberOfTeams()
        {
            this.testSerieOne.TeamIds.Add(Guid.NewGuid());
            this.seriesService.Add(this.testSerieOne);
        }

        [TestMethod]
        public void GetLeagueTablePlacementIsWorking()
        {
            var series = new DummySeries();
            var teamIds = series.SeriesDummy.TeamIds;
            var teamsOfSerie = new List<Team>();
            foreach (var teamId in teamIds)
            {
                teamsOfSerie.Add(DomainService.FindTeamById(teamId));
            }
            var orderedTeamList = teamsOfSerie.OrderByDescending(x => x.PresentableSeriesStats[series.SeriesDummy.Id].Points)
                    .ThenByDescending(x => x.PresentableSeriesStats[series.SeriesDummy.Id].GoalDifference)
                    .ThenByDescending(x => x.PresentableSeriesStats[series.SeriesDummy.Id].GoalsFor);

            var leagueTable = seriesService.GetLeagueTablePlacement(series.SeriesDummy.Id);

            for (int i = 0; i < orderedTeamList.Count(); i++)
            {
                Assert.IsTrue(orderedTeamList.ElementAt(i).Name.Value == leagueTable.ElementAt(i).TeamName);
            }
        }

        [TestMethod]
        public void DeleteSeriesIsWorking()
        {
            var series = new Series(new MatchDuration(new TimeSpan(0,90,0)),new NumberOfTeams(4),"test");
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(0).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(1).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(2).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(3).Id);
            seriesService.Add(series);
            seriesService.ScheduleGenerator(series.Id);
            Assert.IsTrue(seriesService.GetAll().Contains(series));
            seriesService.DeleteSeries(series.Id);
            Assert.IsFalse(seriesService.GetAll().Contains(series));
        }
    }
}