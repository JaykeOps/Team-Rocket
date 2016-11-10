using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests.Repositories
{
    [TestClass]
    public class TeamRepositoryTest
    {
        [TestMethod]
        public void RepoStateIsTheSame()
        {
            
            var instance = TeamRepository.Instance;
            var instance2 = TeamRepository.Instance;
            
            Assert.AreEqual(instance,instance2);
        }

        [TestMethod]
        public void RepoGetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(TeamRepository.Instance.GetAll(),typeof(IEnumerable<Team>));
        }

        [TestMethod]
        public void RepoAddIsWorking()
        {
            var team = new Team(new TeamName("ifk göteborg"), new ArenaName("ullevi"), new EmailAddress("ifkgoteborg@gmail.com"));
            var team2 = new Team(new TeamName("GAIS"), new ArenaName("ullevi"), new EmailAddress("GAIS@gmail.com"));
            TeamRepository.Instance.Add(team);
            var teams = TeamRepository.Instance.GetAll();
            Assert.IsTrue(teams.Contains(team));
            Assert.IsFalse(teams.Contains(team2));
        }

        [TestMethod]
        public void GetAllNotReturningNull()
        {
            Assert.IsNotNull(TeamRepository.Instance.GetAll());
        }
       
    }
}
