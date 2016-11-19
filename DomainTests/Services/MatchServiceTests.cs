using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Services
{
    [TestClass]
    public class MatchServiceTests
    {
        private MatchService service = new MatchService();
        private Match match = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan"), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));
        private Match match2 = new Match(new ArenaName("Ullevi"), Guid.NewGuid(), Guid.NewGuid(), new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan"), new MatchDateAndTime(new DateTime(2016, 12, 20, 19, 30, 00)));

        [TestMethod]
        public void GetAllIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(service.GetAll(), typeof(IEnumerable<Match>));
        }

        [TestMethod]
        public void GetAllMatchsNotReturningNull()
        {
            Assert.IsNotNull(service.GetAll());
        }

        [TestMethod]
        public void AddMatchIsWorking()
        {
            service.AddMatch(match);
            IEnumerable<Match> matchs = service.GetAll();
            Assert.IsTrue(matchs.Contains(match));
            Assert.IsFalse(matchs.Contains(match2));
        }

        [TestMethod]
        public void ServiceRepoInstanceIsTheSame()
        {
            MatchService service1 = new MatchService();
            MatchService service2 = new MatchService();
            service1.AddMatch(match);
            IEnumerable<Match> matchs = service2.GetAll();
            Assert.IsTrue(matchs.Contains(match));
        }

        [TestMethod]
        public void FindMatchByIdIsWorking()
        {
            Assert.IsFalse(service.FindById(match.Id) == match);
            service.AddMatch(match);
            Assert.IsTrue(service.FindById(match.Id) == match);
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
