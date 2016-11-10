using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Services
{
    [TestClass]
    public class TeamServiceTest
    {
        private TeamService service = new TeamService();
        private Team team = new Team(new TeamName("ifk göteborg"), new ArenaName("ullevi"), new EmailAddress("ifkgoteborg@gmail.com"));
        private Team team2 = new Team(new TeamName("GAIS"), new ArenaName("ullevi"), new EmailAddress("GAIS@gmail.com"));

        [TestMethod]
        public void GetAllIsReturningIEnumerable()
        {
            Assert.IsInstanceOfType(service.GetAll(), typeof(IEnumerable<Team>));
        }

        [TestMethod]
        public void GetAllTeamsNotReturningNull()
        {
            Assert.IsNotNull(service.GetAll());
        }

        [TestMethod]
        public void AddTeamIsWorking()
        {
            service.AddTeam(team);
            var teams = service.GetAll();
            Assert.IsTrue(teams.Contains(team));
            Assert.IsFalse(teams.Contains(team2));
        }

        [TestMethod]
        public void ServiceRepoInstanceIsTheSame()
        {
            var service1 = new TeamService();
            var service2 = new TeamService();
            service1.AddTeam(team);
            var teams = service2.GetAll();
            Assert.IsTrue(teams.Contains(team));
        }

        [TestMethod]
        public void FindTeamByIdIsWorking()
        {
            Assert.IsFalse(service.FindById(team.Id) == team);
            service.AddTeam(team);
            Assert.IsTrue(service.FindById(team.Id) == team);
        }
    }
}