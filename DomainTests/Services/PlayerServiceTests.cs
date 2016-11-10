using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

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
        public void FindPlayerFullName()
        {
            var expectedPlayerId = playerService.FindPlayer("Sergio Ramos", true).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerCaseSensitive()
        {
            var expectedPlayerId = playerService.FindPlayer("SeRGio RaMos", true).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfFirstName()
        {
            var expectedPlayerId = playerService.FindPlayer("ZLat", true).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfLastName()
        {
            var expectedPlayerId = playerService.FindPlayer("Ibra", true).Select(x => x.Id).First();

            var actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerSpecialCharactersNotAllowed()
        {
            var expectedPlayerObj = playerService.FindPlayer("Ibra@%", true).FirstOrDefault();

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