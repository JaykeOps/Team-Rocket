using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Services;
using Domain.Value_Objects;

namespace DomainTests.Repositories
{
    [TestClass]
    public class PlayerRepositoryTests
    {
        private PlayerService playerService;
        private IEnumerable<Player> allPlayers;

        [TestInitialize]
        public void Init()
        {
            playerService = new PlayerService();
            allPlayers = playerService.GetAll();
        }

        [TestMethod]
        public void RepoGetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(allPlayers, typeof(IEnumerable<Player>));
        }

        [TestMethod]
        public void AddPlayerWorking()
        {
            var newPlayer = new Player(new Name("Manuel", "Neuer"), new DateOfBirth("1986-03-27"), PlayerPosition.GoalKeeper, PlayerStatus.Available);
            playerService.Add(newPlayer);
            Assert.IsTrue(allPlayers.Contains(newPlayer));
        }

        [TestMethod]
        public void GetAllNotNull()
        {
            Assert.IsNotNull(allPlayers);
        }
    }
}
