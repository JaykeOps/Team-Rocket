using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Domain.Entities;
using Domain.Value_Objects;

namespace DomainTests.Entities
{
    [TestClass]
    public class TeamTest
    {
        private Team team;


        public TeamTest()
        {
            var teamName = new TeamName("Ifk Göteborg");
            var arena = new ArenaName("Ullevi");
            var email = new EmailAddress("Ifk@gmail.com");
            this.team = new Team(teamName, arena, email);

        }

        [TestMethod]
        public void TeamCanHoldValidEntries()
        {
            Assert.IsTrue(this.team.Id != Guid.Empty);
            Assert.IsTrue(this.team.Name == new TeamName("Ifk Göteborg"));
            Assert.IsTrue(this.team.Arena == new ArenaName("Ullevi"));
            Assert.IsTrue(this.team.Email == new EmailAddress("Ifk@gmail.com"));
            Assert.IsTrue(this.team.PlayerIds != null);

        }

        [TestMethod]
        public void TeamNameCanChange()
        {
            var teamName = new TeamName("Häcken");
            Assert.IsFalse(this.team.Name == teamName);
            this.team.Name = teamName;
            Assert.IsTrue(this.team.Name == teamName);
        }

        [TestMethod]
        public void TeamArenaCanChange()
        {
            var arena = new ArenaName("Bravida");
            Assert.IsFalse(this.team.Arena == arena);
            this.team.Arena = arena;
            Assert.IsTrue(this.team.Arena == arena);
        }

        [TestMethod]
        public void TeamEmailCanChange()
        {
            var eamil = new EmailAddress("hacken@gmail.com");
            Assert.IsFalse(this.team.Email == eamil);
            this.team.Email = eamil;
            Assert.IsTrue(this.team.Email == eamil);
        }

        [TestMethod]
        public void TeamsHashSetOFPlayersCanAdd()
        {
            this.team.PlayerIds.Add(Guid.NewGuid());
            Assert.IsTrue(team.PlayerIds.Count==1);
        }
    }
}
