using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Services
{
    [TestClass]
    public class TeamServiceTests
    {
        private TeamService service = new TeamService();
        private Team team = new Team(new TeamName("ifk göteborg"), new ArenaName("ullevi"), new EmailAddress("ifkgoteborg@gmail.com"));
        private Team team2 = new Team(new TeamName("GAIS"), new ArenaName("ullevi"), new EmailAddress("gais@gmail.com"));
        private DummySeries dummySeries;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
        }

        [TestMethod]
        public void GetAllIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(this.service.GetAllTeams(), typeof(IEnumerable<Team>));
        }

        [TestMethod]
        public void GetAllTeamsNotReturningNull()
        {
            Assert.IsNotNull(this.service.GetAllTeams());
        }

        [TestMethod]
        public void AddTeamIsWorking()
        {
            this.service.Add(this.team);
            var teams = this.service.GetAllTeams();
            Assert.IsTrue(teams.Contains(this.team));
            Assert.IsFalse(teams.Contains(this.team2));
        }

        [TestMethod]
        public void ServiceRepoInstanceIsTheSame()
        {
            var service1 = new TeamService();
            var service2 = new TeamService();
            service1.Add(this.team);
            var teams = service2.GetAllTeams();
            Assert.IsTrue(teams.Contains(this.team));
        }

        [TestMethod]
        public void FindTeamByIdIsWorking()
        {
            Assert.IsFalse(this.service.FindTeamById(this.team.Id) == this.team);
            this.service.Add(this.team);
            Assert.IsTrue(this.service.FindTeamById(this.team.Id) == this.team);
        }

        [TestMethod]
        public void GetTeamStatsInSeriesIsWorking()
        {
            var series = new DummySeries();
            var team = series.DummyTeams.DummyTeamOne;
            var teamStats = service.GetTeamStatsInSeries(series.SeriesDummy.Id, team.Id);
            var games = DomainService.GetAllGames();
            var teamGoals = games.Where(game => game.SeriesId == series.SeriesDummy.Id).SelectMany(game => game.Protocol.Goals).Count(goal => goal.TeamId == team.Id);
            Assert.IsTrue(teamGoals == teamStats.GoalsFor);
        }

        [TestMethod]
        public void AddListOfPlayerTest()
        {
            var teamOne = new Team(new TeamName("Grunden Bois"), new ArenaName("Ullevi"), new EmailAddress("test@gamil.com"));
            var teamTwo = new Team(new TeamName("Grunden Bois"), new ArenaName("Ullevi"), new EmailAddress("test@gamil.com"));
            var teamThree = new Team(new TeamName("Grunden Bois"), new ArenaName("Ullevi"), new EmailAddress("test@gamil.com"));

            var teams = new List<Team>
            {
                teamOne,
                teamTwo
            };
            service.Add(teams);
            var allTeams = DomainService.GetAllTeams();
            Assert.IsTrue(allTeams.Contains(teamOne));
            Assert.IsTrue(allTeams.Contains(teamTwo));
            Assert.IsFalse(allTeams.Contains(teamThree));
        }

        [TestMethod]
        public void GetAllTeamsOfSeriesIsWorking()
        {
            var series = new DummySeries();
            var teamsOfSerie = service.GetTeamsOfSerie(series.SeriesDummy.Id);
            Assert.IsTrue(teamsOfSerie != null
                && teamsOfSerie.Contains(series.DummyTeams.DummyTeamOne)
                && teamsOfSerie.Contains(series.DummyTeams.DummyTeamTwo)
                && teamsOfSerie.Contains(series.DummyTeams.DummyTeamThree)
                && teamsOfSerie.Contains(series.DummyTeams.DummyTeamFour));
        }

        [TestMethod]
        public void TeamSearchCanReturnTeamsWithMatchingName()
        {
            var teams = this.service.Search
                (this.dummySeries.DummyTeams.DummyTeamOne.Name.ToString()).ToList();
            Assert.IsNotNull(teams);
            Assert.AreNotEqual(teams.Count, 0);
            foreach (var team in teams)
            {
                Assert.AreEqual(team.Name.ToString(), "Dummy TeamOne");
            }
        }

        [TestMethod]
        public void TeamSearchCanReturnTeamsWithMatchingArenaName()
        {
            var teams = this.service.Search(
                this.dummySeries.DummyTeams.DummyTeamTwo.ArenaName.ToString()).ToList();
            Assert.IsNotNull(teams);
            Assert.AreNotEqual(teams.Count, 0);
            foreach (var team in teams)
            {
                Assert.AreEqual(team.ArenaName.ToString(), "Dummy ArenaTwo");
            }
        }

        [TestMethod]
        public void TeamSearchCanReturnTeamsWithMatchingEmailAddress()
        {
            var teamFour = this.dummySeries.DummyTeams.DummyTeamFour;
            teamFour.Email = new EmailAddress("cardio@workout.com");
            var teams = this.service.Search("cardio@workout.com").ToList();
            Assert.IsNotNull(teams);
            Assert.AreNotEqual(teams.Count, 0);
            foreach (var team in teams)
            {
                Assert.AreEqual(teamFour.Email, team.Email);
            }
        }

        [TestMethod]
        public void TeamSearchCanReturnTeamContainingPlayerIdWithMatchingName()
        {
            var playerOne = DomainService.FindPlayerById(
                this.dummySeries.DummyTeams.DummyTeamOne.PlayerIds.First());
            playerOne.Name = new Name("James", "Bond");
            var teams = this.service.Search("James Bond").ToList();
            Assert.IsNotNull(teams);
            Assert.AreNotEqual(teams.Count, 0);
            foreach (var team in teams)
            {
                Assert.IsTrue(team.PlayerIds.Any(x =>
                DomainService.FindPlayerById(x).Name.ToString() == "James Bond"));
            }
        }

        [TestMethod]
        public void RemovePlayerWorks()
        {
            var series = new DummySeries();
            var teamToRemove = DomainService.FindTeamById(series.SeriesDummy.
                TeamIds.ElementAt(0));
            service.RemoveTeam(teamToRemove.Id);
            Assert.IsFalse(service.GetAllTeams().ToList().Contains(teamToRemove));
        }
    }
}