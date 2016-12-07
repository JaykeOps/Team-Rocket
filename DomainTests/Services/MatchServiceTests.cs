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
    public class MatchServiceTests
    {
        private MatchService service = new MatchService();
        private Match match = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan")), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
        private Match match2 = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan")), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
        private DummySeries dummySeries;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
        }

        [TestMethod]
        public void GetAllIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(this.service.GetAll(), typeof(IEnumerable<Match>));
        }

        [TestMethod]
        public void GetAllMatchsNotReturningNull()
        {
            Assert.IsNotNull(this.service.GetAll());
        }

        [TestMethod]
        public void AddMatchIsWorking()
        {
            this.service.Add(this.match);
            var matchs = this.service.GetAll();
            Assert.IsTrue(matchs.Contains(this.match));
            Assert.IsFalse(matchs.Contains(this.match2));
        }

        [TestMethod]
        public void ServiceRepoInstanceIsTheSame()
        {
            var service1 = new MatchService();
            var service2 = new MatchService();
            service1.Add(this.match);
            var matchs = service2.GetAll();
            Assert.IsTrue(matchs.Contains(this.match));
        }

        [TestMethod]
        public void FindMatchByIdIsWorking()
        {
            Assert.IsFalse(this.service.FindById(this.match.Id) == this.match);
            this.service.Add(this.match);
            Assert.IsTrue(this.service.FindById(this.match.Id) == this.match);
        }

        [TestMethod]
        public void EditMatchTimeIsWorking()
        {
            var matchToEdit = this.service.GetAll().ElementAt(0);
            var unEditedMatch = new Match(matchToEdit.Location, matchToEdit.HomeTeamId,
                matchToEdit.AwayTeamId, DomainService.FindSeriesById(matchToEdit.SeriesId),
                matchToEdit.MatchDate);

            this.service.EditMatchTime(new DateTime(2016, 12, 20, 10, 30, 00), matchToEdit.Id);
            Assert.IsTrue(matchToEdit.MatchDate != unEditedMatch.MatchDate);
        }

        [TestMethod]
        public void EditMatchLocationIsWorking()
        {
            var matchToEdit = this.service.GetAll().ElementAt(0);

            var unEditedMatch = new Match(matchToEdit.Location, matchToEdit.HomeTeamId,
                matchToEdit.AwayTeamId, DomainService.FindSeriesById(matchToEdit.SeriesId),
                matchToEdit.MatchDate);

            this.service.EditMatchLocation("Svennes arena", matchToEdit.Id);
            Assert.IsTrue(matchToEdit.Location != unEditedMatch.Location);
        }

        [TestMethod]
        public void AddListOfMatchesTest()
        {
            var series = new DummySeries();
            var matchOne = new Match(new ArenaName("ullevi"), Guid.NewGuid(), Guid.NewGuid(), series.SeriesDummy);
            var matchTwo = new Match(new ArenaName("ullevi"), Guid.NewGuid(), Guid.NewGuid(), series.SeriesDummy);
            var matchThree = new Match(new ArenaName("ullevi"), Guid.NewGuid(), Guid.NewGuid(), series.SeriesDummy);

            var matches = new List<Match>
            {
                matchOne,
                matchTwo
            };
            service.Add(matches);
            var allMatches = DomainService.GetAllMatches();
            Assert.IsTrue(allMatches.Contains(matchOne));
            Assert.IsTrue(allMatches.Contains(matchTwo));
            Assert.IsFalse(allMatches.Contains(matchThree));
        }

        [TestMethod]
        public void MatchSearchCanReturnMatchesBelongingToSpecifiedSeries()
        {
            var series = new DummySeries();
            var matches = this.service.Search("The Dummy Series").ToList();
            Assert.IsNotNull(matches);
            Assert.AreNotEqual(matches.Count, 0);
            foreach (var match in matches)
            {
                Assert.AreEqual(DomainService.FindSeriesById(match.SeriesId).SeriesName.ToString(),
                    "The Dummy Series");
            }
        }

        [TestMethod]
        public void MatchSearchCanReturnMatchesContainingSpecifiedTeam()
        {
            var series = new DummySeries();
            var matches = this.service.Search("Dummy TeamThree").ToList();
            Assert.IsNotNull(matches);
            Assert.AreNotEqual(matches, 0);
            foreach (var match in matches)
            {
                Assert.IsTrue(DomainService.FindTeamById(match.HomeTeamId).Name.ToString()
                              == "Dummy TeamThree"
                              || DomainService.FindTeamById(match.AwayTeamId).Name.ToString()
                              == "Dummy TeamThree");
            }
        }

        [TestMethod]
        public void MatchSearchCanReturnMatchesContainingSpecifiedArena()
        {
            //TODO: Gets screwed since matches has no dates!
            var series = new DummySeries();
            var matches = this.service.Search("Dummy ArenaTwo").ToList();
            Assert.IsNotNull(matches);
            Assert.AreNotEqual(matches.Count, 0);
            foreach (var match in matches)
            {
                Assert.AreEqual(match.Location.ToString(), "Dummy ArenaTwo");
            }
        }

        [TestMethod]
        public void MatchSearchCanReturnMatchesScheduledAtSpecifiedDate()
        {
            //TODO: Write when matches has dates!
        }

        //Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");
        //MatchDateAndTime date = new MatchDateAndTime(new DateTime(2016, 12, 24));
        //Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));
        //Team teamBlue = new Team(new TeamName("BlueTeam"), new ArenaName("BlueArena"), new EmailAddress("blue@gmail.se"));

        //[TestMethod]
        //public void SearchMatchByLocation()
        //{
        //    Match searchMatch = new Match(teamGreen.ArenaName, teamGreen.Id, teamBlue.Id, series, date);
        //    service.AddMatch(searchMatch);

        //    IEnumerable<Match> matches = service.SearchMatch("GreenArena", StringComparison.CurrentCultureIgnoreCase);

        //    Assert.IsNotNull(matches);
        //    Assert.AreEqual(searchMatch.Id, matches.First().Id);
        //}

        //[TestMethod]
        //public void SearchMatchByMatchDate()
        //{
        //    Match searchMatch = new Match(teamGreen.ArenaName, teamGreen.Id, teamBlue.Id, series, date);
        //    service.AddMatch(searchMatch);

        //    IEnumerable<Match> matches = service.SearchMatch("2016-12-24", StringComparison.CurrentCultureIgnoreCase);

        //    Assert.IsNotNull(matches);
        //    Assert.AreEqual(searchMatch.Id, matches.First().Id);
        //}

        //[TestMethod]
        //public void SearchMatchByHomeTeam()
        //{
        //    Match searchMatch = new Match(teamGreen.ArenaName, teamGreen.Id, teamBlue.Id, series, date);
        //    service.AddMatch(searchMatch);

        //    IEnumerable<Match> matches = service.SearchMatch("GreenTeam", StringComparison.CurrentCultureIgnoreCase);

        //    Assert.IsNotNull(matches);
        //    Assert.AreEqual(searchMatch.Id, matches.First().Id);
        //}

        //[TestMethod]
        //public void SearchMatchByAwayTeam()
        //{
        //    Match searchMatch = new Match(teamGreen.ArenaName, teamGreen.Id, teamBlue.Id, series, date);
        //    service.AddMatch(searchMatch);

        //    IEnumerable<Match> matches = service.SearchMatch("BlueTeam", StringComparison.CurrentCultureIgnoreCase);

        //    Assert.IsNotNull(matches);
        //    Assert.AreEqual(searchMatch.Id, matches.First().Id);
        //}
    }
}