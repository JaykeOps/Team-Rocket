using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
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
            IEnumerable<Team> teams = this.service.GetAll();
            Assert.IsTrue(teams.Contains(this.team));
            Assert.IsFalse(teams.Contains(this.team2));
        }

        [TestMethod]
        public void ServiceRepoInstanceIsTheSame()
        {
            TeamService service1 = new TeamService();
            TeamService service2 = new TeamService();
            service1.AddTeam(this.team);
            IEnumerable<Team> teams = service2.GetAll();
            Assert.IsTrue(teams.Contains(this.team));
        }

        [TestMethod]
        public void FindTeamByIdIsWorking()
        {
            Assert.IsFalse(this.service.FindById(this.team.Id) == this.team);
            this.service.AddTeam(this.team);
            Assert.IsTrue(this.service.FindById(this.team.Id) == this.team);
        }
    }
}