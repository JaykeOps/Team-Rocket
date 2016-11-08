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
            var team = new Team(new TeamName("ifk göteborg"),new ArenaName("ullevi"),new EmailAddress("ifkgoteborg@gmail.com")  );
            var instance = TeamRepository.instance;
            var instance2 = TeamRepository.instance;
            instance.Add(team);
            var teams = instance.GetAll();
            var teams2 = instance2.GetAll();
            Assert.IsTrue(teams.Count()==teams2.Count());
        }
    }
}
