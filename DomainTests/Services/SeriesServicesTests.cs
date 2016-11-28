using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Services
{
    [TestClass]
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
        public void AddTeamToSeriesIsWorking()
        {
            var series = seriesService.GetAll().ElementAt(0);

            var teamOne = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(0);
            var teamTwo = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(1);
            var teamThree = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(2);
            var teamFour = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(3);

            for (int i = series.TeamIds.Count-1; i >= 0; i--)
            {
                series.TeamIds.Remove(series.TeamIds.ElementAt(i));
            }

            seriesService.AddTeamToSeries(series.Id, teamOne);
            seriesService.AddTeamToSeries(series.Id, teamTwo);
            seriesService.AddTeamToSeries(series.Id, teamThree);
            seriesService.AddTeamToSeries(series.Id, teamFour);

            Assert.IsTrue(series.TeamIds.Contains(teamOne)
                && series.TeamIds.Contains(teamTwo)
                && series.TeamIds.Contains(teamThree)
                && series.TeamIds.Contains(teamFour));


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddTeamToSeriesCanNotAddSameTeamMultipleTimes()
        {
            var series = seriesService.GetAll().ElementAt(0);

            var teamOne = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(0);
            var teamTwo = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(1);
            var teamThree = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(2);
            var teamFour = seriesService.GetAll().ElementAt(0).TeamIds.ElementAt(3);

            for (int i = series.TeamIds.Count - 1; i >= 0; i--)
            {
                series.TeamIds.Remove(series.TeamIds.ElementAt(i));
            }

            seriesService.AddTeamToSeries(series.Id, teamOne);
            seriesService.AddTeamToSeries(series.Id, teamTwo);
            seriesService.AddTeamToSeries(series.Id, teamThree);
            seriesService.AddTeamToSeries(series.Id, teamFour);
            seriesService.AddTeamToSeries(series.Id, teamOne);
            seriesService.AddTeamToSeries(series.Id, teamTwo);
            seriesService.AddTeamToSeries(series.Id, teamThree);
            seriesService.AddTeamToSeries(series.Id, teamFour);

            Assert.IsTrue(series.TeamIds.Contains(teamOne)
                && series.TeamIds.Contains(teamTwo)
                && series.TeamIds.Contains(teamThree)
                && series.TeamIds.Contains(teamFour));


        }

        [TestMethod]
        public void RemoveTeamFromSeriesIsWorking()
        {
            var seriesToEdit = new DummySeries();
            var uneditedSeries = new DummySeries();
            var teamToRemove = seriesToEdit.SeriesDummy.TeamIds.ElementAt(0);
            seriesService.RemoveTeamFromSeries(seriesToEdit.SeriesDummy.Id, teamToRemove);
            Assert.IsTrue(seriesToEdit.SeriesDummy.TeamIds.Count != uneditedSeries.SeriesDummy.TeamIds.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveTeamFromSeriesCanNotRemoveTeamThatIsNotInSeries()
        {
            var series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(4), "test");
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(0).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(1).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(2).Id);
            series.TeamIds.Add(DomainService.GetAllTeams().ElementAt(3).Id);
            seriesService.Add(series);
            seriesService.ScheduleGenerator(series.Id);
            Assert.IsTrue(seriesService.GetAll().Contains(series));
            seriesService.DeleteSeries(series.Id);
            Assert.IsFalse(seriesService.GetAll().Contains(series));
            var seriesToEdit = new DummySeries();
            var uneditedSeries = new DummySeries();
            var teamToRemove = seriesToEdit.SeriesDummy.TeamIds.ElementAt(0);
            seriesService.RemoveTeamFromSeries(seriesToEdit.SeriesDummy.Id, teamToRemove);
            seriesService.RemoveTeamFromSeries(seriesToEdit.SeriesDummy.Id, teamToRemove);
        }
    }
}