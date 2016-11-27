using Domain.Entities;
using Domain.Repositories;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Repositories
{
    [TestClass]
    public class TeamRepositoryTests
    {
        private DummySeries dummySeries;
        private Team dummyTeam;
        private Team dummyTeamDuplicate;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
            this.dummyTeam = this.dummySeries.DummyTeams.DummyTeamOne;
            this.dummyTeamDuplicate = new Team(this.dummyTeam.Name, this.dummyTeam.ArenaName, this.dummyTeam.Email)
            {
                Id = this.dummyTeam.Id
            };
        }

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

        [TestMethod]
        public void TeamLoadTest()
        {
            var teams = TeamRepository.instance.GetAll();
            Assert.IsTrue(teams.Count() != 0);
        }

        [TestMethod]
        public void TryGetTeamWillReplaceRepositoryTeamWithNewTeamIfIdsAreEqual()
        {
            var teamInRepository = TeamRepository.instance.GetAll().First(x => x.Id == this.dummyTeam.Id);
            Assert.AreEqual(this.dummyTeam, teamInRepository);
            Assert.AreNotEqual(this.dummyTeam, this.dummyTeamDuplicate);
            Assert.AreEqual(this.dummyTeam.Id, this.dummyTeamDuplicate.Id);
            this.dummyTeamDuplicate.Name = new TeamName("Smurfs United");
            TeamRepository.instance.Add(this.dummyTeamDuplicate);
            teamInRepository = TeamRepository.instance.GetAll().First(x => x.Id == this.dummyTeam.Id);
            Assert.AreEqual(teamInRepository.Name, this.dummyTeamDuplicate.Name);
            Assert.AreEqual(this.dummyTeamDuplicate, teamInRepository);
        }

        //TODO: Write tests for duplicate validation
    }
}