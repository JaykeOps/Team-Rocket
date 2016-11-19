using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;

namespace Domain.Services.Tests
{
    [TestClass]
    public class PlayerServiceTests
    {
        private PlayerService playerService;
        private IEnumerable<Player> allPlayers;
        private Guid zlatanPlayerId;

        [TestInitialize]
        public void Init()
        {
            playerService = new PlayerService();
            allPlayers = playerService.GetAll();
            zlatanPlayerId = allPlayers.ElementAt(0).Id;
        }

        [TestMethod]
        public void GetAllPlayersNotNull()
        {
            IEnumerable<Player> getAllPlayers = playerService.GetAll();

            Assert.IsNotNull(getAllPlayers);
        }

        [TestMethod]
        public void FindPlayerByIdIsWorking()
        {
            Player player = new Player(new Name("John", "Doe"), new DateOfBirth("1985-05-20"), PlayerPosition.Forward, PlayerStatus.Absent);
            Assert.IsFalse(playerService.FindById(player.Id) == player);
            playerService.Add(player);
            Assert.IsTrue(playerService.FindById(player.Id) == player);
        }

        #region PlayerService, FindPlayer metod tests
        [TestMethod]
        public void FindPlayerFullName()
        {
            Guid expectedPlayerId = playerService.FindPlayer("Sergio Ramos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerCaseSensitive()
        {
            Guid expectedPlayerId = playerService.FindPlayer("SeRGio RaMos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfFirstName()
        {
            Guid expectedPlayerId = playerService.FindPlayer("ZLat", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfLastName()
        {
            Guid expectedPlayerId = playerService.FindPlayer("Ibra", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerSpecialCharactersNotAllowed()
        {
            Player expectedPlayerObj = playerService.FindPlayer("Ibra@%", StringComparison.InvariantCultureIgnoreCase).FirstOrDefault();

            Assert.IsNull(expectedPlayerObj);
        }
        #endregion

        [TestMethod]
        public void GetPlayerNameNotNull()
        {
            string expectedPlayerName = playerService.GetPlayerName(zlatanPlayerId);

            Assert.IsNotNull(expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerNameNotEmpty()
        {
            string expectedPlayerName = playerService.GetPlayerName(zlatanPlayerId);

            Assert.AreNotEqual("", expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerTeamIdNotNull()
        {
            Guid expectedTeamId = playerService.GetPlayerTeamId(zlatanPlayerId);

            Assert.IsNotNull(expectedTeamId);
        }

        
    }
}