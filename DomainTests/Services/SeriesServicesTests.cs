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
    [TestClass()]
    public class SeriesServicesTests
    {
        private SeriesService seriesService;
        private Series testSerieOne;
        private DummySeries dummySeries;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
        }

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
        public void AddListOfSeriesTest()
        {
            var seriesOne = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(4), "Körv");
            var seriesTwo = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(4), "Körv");
            var seriesThree = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(4), "Körv");
            seriesOne.TeamIds.Add(Guid.NewGuid());
            seriesOne.TeamIds.Add(Guid.NewGuid());
            seriesOne.TeamIds.Add(Guid.NewGuid());
            seriesOne.TeamIds.Add(Guid.NewGuid());

            seriesTwo.TeamIds.Add(Guid.NewGuid());
            seriesTwo.TeamIds.Add(Guid.NewGuid());
            seriesTwo.TeamIds.Add(Guid.NewGuid());
            seriesTwo.TeamIds.Add(Guid.NewGuid());

            seriesThree.TeamIds.Add(Guid.NewGuid());
            seriesThree.TeamIds.Add(Guid.NewGuid());
            seriesThree.TeamIds.Add(Guid.NewGuid());
            seriesThree.TeamIds.Add(Guid.NewGuid());

            var series = new List<Series>
            {
                seriesOne,
                seriesTwo
            };
            seriesService.Add(series);
            var allSeries = DomainService.GetAllSeries();
            Assert.IsTrue(allSeries.Contains(seriesOne));
            Assert.IsTrue(allSeries.Contains(seriesTwo));
            Assert.IsFalse(allSeries.Contains(seriesThree));
        }

        [TestMethod]
        public void SeriesSearchCanReturnSeriesContainingSpecifiedSeriesName()
        {
            var matchingSeries = this.seriesService.Search("The Dummy Series").ToList();
            Assert.IsNotNull(matchingSeries);
            Assert.AreNotEqual(matchingSeries.Count, 0);
            foreach (var series in matchingSeries)
            {
                Assert.AreEqual(series.SeriesName, "The Dummy Series");
            }
        }

        [TestMethod]
        public void SeriesSearchCanReturnSeriesContainingSpecifiedTeamName()
        {
            var matchingSeries = this.seriesService.Search("Dummy TeamFour").ToList();
            Assert.IsNotNull(matchingSeries);
            Assert.AreNotEqual(matchingSeries.Count, 0);
            foreach (var series in matchingSeries)
            {
                Assert.IsTrue(series.TeamIds.Any(x => DomainService.FindTeamById(x).Name.ToString()
                == "Dummy TeamFour"));
            }
        }

        [TestMethod]
        public void SeriesSearchCanReturnSeriesContainingSpecifiedArenaName()
        {
            var matchingSeries = this.seriesService.Search("Dummy ArenaOne").ToList();
            Assert.IsNotNull(matchingSeries);
            Assert.AreNotEqual(matchingSeries.Count, 0);
            foreach (var series in matchingSeries)
            {
                Assert.IsTrue(series.Schedule.Values.Any(x => x.Any(y => y.Location.ToString() 
                == "Dummy ArenaOne")));
            }
        }

        [TestMethod]
        public void SeriesSearchCanReturnSeriesContainingSpecifiedMatchDate()
        {
            //TODO: Update when matches has dates!
            var matchingSeries = this.seriesService.Search("2017-08-22 10:10").ToList();
            Assert.IsNotNull(matchingSeries);
            Assert.AreNotEqual(matchingSeries.Count, 0);
            foreach (var series in matchingSeries)
            {
                Assert.IsTrue(series.Schedule.Values.Any(x => x.Any(y => y.MatchDate.ToString() 
                == "2017-08-22 10:10")));
            }
        }
    }
}