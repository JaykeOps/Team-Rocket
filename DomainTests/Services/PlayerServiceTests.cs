using Domain.Entities;
using Domain.Interfaces;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Domain.Services.Tests
{
    [TestClass]
    public class PlayerServiceTests
    {
        private PlayerService playerService;
        private DummySeries dummySeries;
        private Team dummyTeam;
        private Player dummyPlayer;

        [TestInitialize]
        public void Init()
        {
            this.playerService = new PlayerService();
            this.dummySeries = new DummySeries();
            this.dummyTeam = this.dummySeries.DummyTeams.DummyTeamTwo;
            this.dummyPlayer = this.dummyTeam.Players.ElementAt(0);
        }

        [TestMethod]
        public void GetAllPlayersNotNull()
        {
            var getAllPlayers = this.playerService.GetAllPresentablePlayers();
            Assert.IsNotNull(getAllPlayers);
        }

        [TestMethod]
        public void FindPlayerByIdIsWorking()
        {
            var player = new Player(new Name("John", "Doe"), new DateOfBirth("1985-05-20"), PlayerPosition.Forward,
                PlayerStatus.Absent);
            Assert.IsFalse(this.playerService.FindById(player.Id) == player);
            this.playerService.Add(player);
            Assert.IsTrue(this.playerService.FindById(player.Id) == player);
        }

        #region PlayerService, FindPlayer metod tests

        //[TestMethod]
        //public void FindPlayerFullName()
        //{
        //    var expectedName = this.dummyPlayer.Name;
        //    var nameFound = this.playerService.FindPlayer(expectedName.FirstName + expectedName.LastName,
        //        StringComparison.InvariantCultureIgnoreCase);
        //}

        //[TestMethod]
        //public void FindPlayerCaseSensitive()
        //{
        //    var expectedPlayer =
        //        (Player)playerService.FindPlayer("SeRGio RaMos", StringComparison.InvariantCultureIgnoreCase).First();

        //    var actualPlayerId = allPlayers.First(x => x.Name.ToString() == "Sergio Ramos").Id;

        //    Assert.AreEqual(expectedPlayer.Id, actualPlayerId);
        //}

        //[TestMethod]
        //public void FindPlayerPartOfFirstName()
        //{
        //    var expectedPlayer =
        //        (Player)playerService.FindPlayer("ZLat", StringComparison.InvariantCultureIgnoreCase).First();

        //    var actualPlayerId = allPlayers.First(x => x.Name.ToString() == "Zlatan Ibrahimovic").Id;

        //    Assert.AreEqual(expectedPlayer.Id, actualPlayerId);
        //}

        //[TestMethod]
        //public void FindPlayerPartOfLastName()
        //{
        //    var expectedPlayer =
        //        (Player)playerService.FindPlayer("Ibra", StringComparison.InvariantCultureIgnoreCase).First();

        //    var actualPlayerId = allPlayers.First(x => x.Name.ToString() == "Zlatan Ibrahimovic").Id;

        //    Assert.AreEqual(expectedPlayer.Id, actualPlayerId);
        //}

        [TestMethod]
        public void FindPlayerSpecialCharactersNotAllowed()
        {
            IPresentablePlayer expectedPlayerObj =
                this.playerService.FindPlayer("Ibra@%", StringComparison.InvariantCultureIgnoreCase).FirstOrDefault();

            Assert.IsNull(expectedPlayerObj);
        }

        #endregion PlayerService, FindPlayer metod tests

        //[TestMethod]
        //public void GetPlayerNameNotNull()
        //{
        //    string expectedPlayerName = this.playerService.GetPlayerName(this.zlatanPlayerId);

        //    Assert.IsNotNull(expectedPlayerName);
        //}

        //[TestMethod]
        //public void GetPlayerNameNotEmpty()
        //{
        //    string expectedPlayerName = this.playerService.GetPlayerName(this.zlatanPlayerId);

        //    Assert.AreNotEqual("", expectedPlayerName);
        //}

        //[TestMethod]
        //public void GetPlayerTeamIdNotNull()
        //{
        //    Guid expectedTeamId = this.playerService.GetPlayerTeamId(this.zlatanPlayerId);

        //    Assert.IsNotNull(expectedTeamId);
        //}

        [TestMethod]
        public void GetTopScorersTest()
        {
            var dummySeries = new DummySeries();
            var topScorers = this.playerService.GetTopScorersForSeries(dummySeries.SeriesDummy.Id);

            var allTeamsInSeries = dummySeries.SeriesDummy.TeamIds.Select(id => DomainService.FindTeamById(id)).ToList();
            var allPlayerInSeries = allTeamsInSeries.SelectMany(team => team.Players).ToList();
            var allPlayerStats = allPlayerInSeries.Select(player => player.AggregatedStats[dummySeries.SeriesDummy.Id]).ToList();
            var allPlayerStatsSorted = allPlayerStats.OrderByDescending(ps => ps.GoalCount).Take(15);
            for (int i = 0; i < topScorers.Count(); i++)
            {
                Assert.IsTrue(allPlayerStatsSorted.ElementAt(i).GoalCount == topScorers.ElementAt(i).GoalCount);
            }
        }

        [TestMethod]
        public void GetTopAssistTest()
        {
            var series = new DummySeries();
            var topAssists = this.playerService.GetTopAssistsForSeries(series.SeriesDummy.Id);

            var allTeamsInSeries = series.SeriesDummy.TeamIds.Select(id => DomainService.FindTeamById(id)).ToList();
            var allPlayerInSeries = allTeamsInSeries.SelectMany(team => team.Players).ToList();
            var allPlayerStats = allPlayerInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
            var allPlayerStatsSorted = allPlayerStats.OrderByDescending(ps => ps.AssistCount).Take(15);
            for (int i = 0; i < topAssists.Count(); i++)
            {
                Assert.IsTrue(allPlayerStatsSorted.ElementAt(i).AssistCount == topAssists.ElementAt(i).AssistCount);
            }
        }

        [TestMethod]
        public void GetTopRedCardsTest()
        {
            var series = new DummySeries();
            var topReds = this.playerService.GetTopRedCardsForSeries(series.SeriesDummy.Id);

            var allTeamsInSeries = series.SeriesDummy.TeamIds.Select(id => DomainService.FindTeamById(id)).ToList();
            var allPlayerInSeries = allTeamsInSeries.SelectMany(team => team.Players).ToList();
            var allPlayerStats = allPlayerInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
            var allPlayerStatsSorted = allPlayerStats.OrderByDescending(ps => ps.RedCardCount).Take(5);
            for (int i = 0; i < topReds.Count(); i++)
            {
                Assert.IsTrue(allPlayerStatsSorted.ElementAt(i).RedCardCount == topReds.ElementAt(i).RedCardCount);
            }
        }

        [TestMethod]
        public void GetTopYellowCardsTest()
        {
            var series = new DummySeries();
            var topYellow = this.playerService.GetTopYellowCardsForSeries(series.SeriesDummy.Id);

            var allTeamsInSeries = series.SeriesDummy.TeamIds.Select(id => DomainService.FindTeamById(id)).ToList();
            var allPlayerInSeries = allTeamsInSeries.SelectMany(team => team.Players).ToList();
            var allPlayerStats = allPlayerInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
            var allPlayerStatsSorted = allPlayerStats.OrderByDescending(ps => ps.YellowCardCount).Take(5);
            for (int i = 0; i < topYellow.Count(); i++)
            {
                Assert.IsTrue(allPlayerStatsSorted.ElementAt(i).YellowCardCount == topYellow.ElementAt(i).YellowCardCount);
            }
        }

        [TestMethod]
        public void PlayerCanBeRenamedThroughDuplicate()
        {
            var playerService = new PlayerService();
            var duplicatePlayer = new Player(this.dummyPlayer.Name, this.dummyPlayer.DateOfBirth,
                this.dummyPlayer.Position, this.dummyPlayer.Status, this.dummyPlayer.Id);
            Assert.AreEqual(this.dummyPlayer.Name, duplicatePlayer.Name);
            this.playerService.RenamePlayer(duplicatePlayer, new Name("Torbjörn", "Nilsson"));
            this.dummyPlayer = playerService.FindById(this.dummyPlayer.Id);
            Assert.AreEqual(duplicatePlayer.Name, this.dummyPlayer.Name);

        }
    }
}