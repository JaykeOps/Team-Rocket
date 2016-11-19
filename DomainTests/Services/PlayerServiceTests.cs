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
            var getAllPlayers = this.playerService.GetAll();

            Assert.IsNotNull(getAllPlayers);
        }

        [TestMethod]
        public void FindPlayerByIdIsWorking()
        {
            var player = new Player(new Name("John","Doe"),new DateOfBirth("1985-05-20"),PlayerPosition.Forward,PlayerStatus.Absent);
            Assert.IsFalse(this.playerService.FindById(player.Id)==player);
            this.playerService.Add(player);
            Assert.IsTrue(this.playerService.FindById(player.Id)==player);
        }

        #region PlayerService, FindPlayer metod tests
        [TestMethod]
        public void FindPlayerFullName()
        {
            var expectedPlayerId = this.playerService.FindPlayer("Sergio Ramos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerCaseSensitive()
        {
            var expectedPlayerId = this.playerService.FindPlayer("SeRGio RaMos", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Sergio Ramos").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfFirstName()
        {
            var expectedPlayerId = this.playerService.FindPlayer("ZLat", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerPartOfLastName()
        {
            var expectedPlayerId = this.playerService.FindPlayer("Ibra", StringComparison.InvariantCultureIgnoreCase).Select(x => x.Id).First();

            var actualPlayerId = this.allPlayers.Where(x => x.Name.ToString() == "Zlatan Ibrahimovic").First().Id;

            Assert.AreEqual(expectedPlayerId, actualPlayerId);
        }

        [TestMethod]
        public void FindPlayerSpecialCharactersNotAllowed()
        {
            var expectedPlayerObj = this.playerService.FindPlayer("Ibra@%", StringComparison.InvariantCultureIgnoreCase).FirstOrDefault();

            Assert.IsNull(expectedPlayerObj);
        }
        #endregion

        [TestMethod]
        public void GetPlayerNameNotNull()
        {
            var expectedPlayerName = this.playerService.GetPlayerName(this.zlatanPlayerId);

            Assert.IsNotNull(expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerNameNotEmpty()
        {
            var expectedPlayerName = this.playerService.GetPlayerName(this.zlatanPlayerId);

            Assert.AreNotEqual("", expectedPlayerName);
        }

        [TestMethod]
        public void GetPlayerTeamIdNotNull()
        {
            var expectedTeamId = this.playerService.GetPlayerTeamId(this.zlatanPlayerId);

            Assert.IsNotNull(expectedTeamId);
        }

        #region PlayerService, Different Stats on Player 
        //[TestMethod]
        //public void GamesPlayedIdsNotNull()
        //{
        //    var expectedGamesPlayedIds = playerService.GetPlayerGamesPlayedIds(zlatanPlayerId);

        //    Assert.IsNotNull(expectedGamesPlayedIds);
        //}

        //[TestMethod]
        //public void TotalYellowCardsNotNull()
        //{
        //    var expectedTotalYellowCards = playerService.GetPlayerTotalYellowCards(zlatanPlayerId);

        //    Assert.IsNotNull(expectedTotalYellowCards);
        //}

        //[TestMethod]
        //public void TotalRedCardsNotNull()
        //{
        //    var expectedTotalRedCards = playerService.GetPlayerTotalRedCards(zlatanPlayerId);

        //    Assert.IsNotNull(expectedTotalRedCards);
        //}

        //[TestMethod]
        //public void TotalGoalsNotNull()
        //{
        //    var expectedTotalGoals = playerService.GetPlayerTotalGoals(zlatanPlayerId);

        //    Assert.IsNotNull(expectedTotalGoals);
        //}

        //[TestMethod]
        //public void TotalAssistsNotNull()
        //{
        //    var expectedTotalAssists = playerService.GetPlayerTotalAssists(zlatanPlayerId);

        //    Assert.IsNotNull(expectedTotalAssists);
        //}

        //[TestMethod]
        //public void TotalPenaltiesNotNull()
        //{
        //    var expectedTotalPenalties = playerService.GetPlayerTotalPenalties(zlatanPlayerId);

        //    Assert.IsNotNull(expectedTotalPenalties);
        //}
        #endregion
    }
}