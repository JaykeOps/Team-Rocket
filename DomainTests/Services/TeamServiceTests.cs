using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DomainTests.Test_Dummies;

namespace DomainTests.Services
{
    [TestClass]
    public class TeamServiceTests
    {
        private TeamService service = new TeamService();
        private Team team = new Team(new TeamName("ifk göteborg"), new ArenaName("ullevi"), new EmailAddress("ifkgoteborg@gmail.com"));
        private Team team2 = new Team(new TeamName("GAIS"), new ArenaName("ullevi"), new EmailAddress("GAIS@gmail.com"));

        [TestMethod]
        public void GetAllIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(this.service.GetAll(), typeof(IEnumerable<Team>));
        }

        [TestMethod]
        public void GetAllTeamsNotReturningNull()
        {
            Assert.IsNotNull(this.service.GetAll());
        }

        [TestMethod]
        public void AddTeamIsWorking()
        {
            this.service.AddTeam(this.team);
            var teams = this.service.GetAll();
            Assert.IsTrue(teams.Contains(this.team));
            Assert.IsFalse(teams.Contains(this.team2));
        }

        [TestMethod]
        public void ServiceRepoInstanceIsTheSame()
        {
            var service1 = new TeamService();
            var service2 = new TeamService();
            service1.AddTeam(this.team);
            var teams = service2.GetAll();
            Assert.IsTrue(teams.Contains(this.team));
        }

        [TestMethod]
        public void FindTeamByIdIsWorking()
        {
            Assert.IsFalse(this.service.FindById(this.team.Id) == this.team);
            this.service.AddTeam(this.team);
            Assert.IsTrue(this.service.FindById(this.team.Id) == this.team);
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
    }
}