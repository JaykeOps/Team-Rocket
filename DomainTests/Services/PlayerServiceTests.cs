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
            this.playerService = new PlayerService();
            this.allPlayers = this.playerService.GetAll();
            this.zlatanPlayerId = this.allPlayers.ElementAt(0).Id;
        }

        [TestMethod]
        public void GetAllPlayersNotNull()
        {
            IEnumerable<Player> getAllPlayers = this.playerService.GetAll();

            Assert.IsNotNull(getAllPlayers);
        }

        [TestMethod]
        public void FindPlayerByIdIsWorking()
        {
            Player player = new Player(new Name("John", "Doe"), new DateOfBirth("1985-05-20"), PlayerPosition.Forward, PlayerStatus.Absent);
            Assert.IsFalse(this.playerService.FindById(player.Id) == player);
            this.playerService.Add(player);
            Assert.IsTrue(this.playerService.FindById(player.Id) == player);
        }

        #region PlayerService, FindPlayer metod tests
        [TestMethod]
        public void FindPlayerFullName()
        {
            Guid expectedPlayerId = this.playerService.FindPlayer("Sergio Ramos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerCaseSensitive()
        {
            Guid expectedPlayerId = this.playerService.FindPlayer("SeRGio RaMos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfFirstName()
        {
            Guid expectedPlayerId = this.playerService.FindPlayer("ZLat", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfLastName()
        {
            Guid expectedPlayerId = this.playerService.FindPlayer("Ibra", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            Guid actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerSpecialCharactersNotAllowed()
        {
            Player expectedPlayerObj = this.playerService.FindPlayer("Ibra@%", StringComparison.InvariantCultureIgnoreCase).FirstOrDefault();

            Assert.IsNull(expectedPlayerObj);
        }
        #endregion

        [TestMethod]
        public void GetPlayerNameNotNull()
        {
            string expectedPlayerName = this.playerService.GetPlayerName(this.zlatanPlayerId);

            Assert.IsNotNull(expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerNameNotEmpty()
        {
            string expectedPlayerName = this.playerService.GetPlayerName(this.zlatanPlayerId);

            Assert.AreNotEqual("", expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerTeamIdNotNull()
        {
            Guid expectedTeamId = this.playerService.GetPlayerTeamId(this.zlatanPlayerId);

            Assert.IsNotNull(expectedTeamId);
        }

        
    }
}