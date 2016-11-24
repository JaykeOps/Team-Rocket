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
        public void AddTeamToSeriesIsWorking()
        {
            var series = new DummySeries();
            var idOfTeamToRemove = series.SeriesDummy.TeamIds.ElementAt(0);
            DomainService.FindTeamById(idOfTeamToRemove);
            series.SeriesDummy.TeamIds.Remove(idOfTeamToRemove);

            Assert.IsFalse(series.SeriesDummy.TeamIds.Contains(idOfTeamToRemove));

            var teamToAdd = series.DummyTeams.DummyTeamOne;
            seriesService.AddTeamToSeries(series.SeriesDummy.Id, teamToAdd.Id);

            Assert.IsTrue(series.SeriesDummy.TeamIds.Contains(teamToAdd.Id));
        }
    }
}