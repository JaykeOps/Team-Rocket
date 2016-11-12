using Domain.Entities;
using Domain.Repositories;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Repositories
{
    [TestClass]
    public class TeamRepositoryTests
    {


        [TestMethod]
        public void RepoStateIsTheSame()
        {
            var instance = TeamRepository.instance;
            var instance2 = TeamRepository.instance;

            Assert.AreEqual(instance, instance2);
        }

        [TestMethod]
        public void RepoGetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(TeamRepository.instance.GetAll(), typeof(IEnumerable<Team>));
        }

        [TestMethod]
        public void RepoAddIsWorking()
        {
            var team = new Team(new TeamName("ifk göteborg"), new ArenaName("ullevi"), new EmailAddress("ifkgoteborg@gmail.com"));
            var team2 = new Team(new TeamName("GAIS"), new ArenaName("ullevi"), new EmailAddress("GAIS@gmail.com"));
            TeamRepository.instance.Add(team);
            var teams = TeamRepository.instance.GetAll();
            Assert.IsTrue(teams.Contains(team));
            Assert.IsFalse(teams.Contains(team2));
        }

        [TestMethod]
        public void GetAllNotReturningNull()
        {
            Assert.IsNotNull(TeamRepository.instance.GetAll());
        }
    }
}