using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DomainTests.Repositories
{
    [TestClass]
    public class PlayerRepositoryTests
    {
        private DummySeries dummySeries;
        private Player dummyPlayer;
        private Player dummyPlayerDuplicate;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
            this.dummyPlayer = DomainService.FindPlayerById(this.dummySeries.DummyTeams.DummyTeamOne.PlayerIds.First());
            this.dummyPlayerDuplicate = new Player(this.dummyPlayer.Name, this.dummyPlayer.DateOfBirth,
                this.dummyPlayer.Position, this.dummyPlayer.Status, this.dummyPlayer.Id);
        }

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
            Assert.IsInstanceOfType(PlayerRepository.instance.GetAll(), typeof(IEnumerable<Player>));
        }

        [TestMethod]
        public void AddPlayerWorking()
        {
            var newPlayer = new Player(new Name("Manuel", "Neuer"), new DateOfBirth("1986-03-27"), PlayerPosition.Goalkeeper, PlayerStatus.Available);
            var play = newPlayer;
            PlayerRepository.instance.Add(newPlayer);
            var allPlayers = PlayerRepository.instance.GetAll();
            Assert.IsTrue(allPlayers.Contains(newPlayer));
        }

        [TestMethod]
        public void GetAllNotNull()
        {
            var test = PlayerRepository.instance.GetAll();
            Assert.IsNotNull(PlayerRepository.instance.GetAll());
        }

        [TestMethod]
        public void TryGetPlayerWillReplaceRepositoryPlayerWithNewPlayerIfIdsAreEqual()
        {
            var playerInRepository = PlayerRepository.instance.GetAll().First(x => x.Id == this.dummyPlayer.Id);
            Assert.AreEqual(this.dummyPlayer, playerInRepository);
            Assert.AreNotEqual(this.dummyPlayer, this.dummyPlayerDuplicate);
            Assert.AreEqual(this.dummyPlayer.Id, this.dummyPlayerDuplicate.Id);
            this.dummyPlayerDuplicate.Name = new Name("Michael", "Jordan");
            Assert.AreNotEqual(this.dummyPlayer.Name, this.dummyPlayerDuplicate.Name);
            PlayerRepository.instance.Add(this.dummyPlayerDuplicate);
            playerInRepository = PlayerRepository.instance.GetAll().First(x => x.Id == this.dummyPlayer.Id);
            Assert.AreEqual(this.dummyPlayerDuplicate.Name, playerInRepository.Name);
            Assert.AreEqual(this.dummyPlayerDuplicate, playerInRepository);
        }
    }
}