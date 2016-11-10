using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Value_Objects;

namespace Domain.Services.Tests
{
    [TestClass]
    public class PlayerServiceTests
    {
        private PlayerService playerService;
        private IEnumerable<Player> allPlayers;
        private Guid playerId;

        [TestInitialize]
        public void Init()
        {
            playerService = new PlayerService();
            allPlayers = playerService.GetAll();
            playerId = allPlayers.ElementAt(0).Id;
        }

        [TestMethod]
        public void GetAllPlayersNotNull()
        {
            var getAllPlayers = playerService.GetAll();

            Assert.IsNotNull(getAllPlayers);
        }

        [TestMethod]
        public void FindPlayerByIdIsWorking()
        {
            var player = new Player(new Name("John","Doe"),new DateOfBirth("1985-05-20"),PlayerPosition.Forward,PlayerStatus.Absent,new ShirtNumber(88));
            Assert.IsFalse(playerService.FindById(player.Id)==player);
            playerService.Add(player);
            Assert.IsTrue(playerService.FindById(player.Id)==player);
        }

        [TestMethod]
        public void FindPlayerFullName()
        {
            var expectedPlayerId = playerService.FindPlayer("Sergio Ramos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerCaseSensitive()
        {
            var expectedPlayerId = playerService.FindPlayer("SeRGio RaMos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfFirstName()
        {
            var expectedPlayerId = playerService.FindPlayer("ZLat", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfLastName()
        {
            var expectedPlayerId = playerService.FindPlayer("Ibra", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerSpecialCharactersNotAllowed()
        {
            var expectedPlayerObj = playerService.FindPlayer("Ibra@%", StringComparison.InvariantCultureIgnoreCase).FirstOrDefault();

            Assert.IsNull(expectedPlayerObj);
        }

        [TestMethod]
        public void GetPlayerNameNotNull()
        {
            var expectedPlayerName = playerService.GetPlayerName(playerId);

            Assert.IsNotNull(expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerNameNotEmpty()
        {
            var expectedPlayerName = playerService.GetPlayerName(playerId);

            Assert.AreNotEqual("", expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerTeamIdNotNull()
        {
            var expectedTeamId = playerService.GetPlayerTeamId(playerId);

            Assert.IsNotNull(expectedTeamId);
        }
    }
}