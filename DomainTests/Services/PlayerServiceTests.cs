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
        private Player duplicatePlayer;

        [TestInitialize]
        public void Init()
        {
            this.playerService = new PlayerService();
            this.dummySeries = new DummySeries();
            this.dummyTeam = this.dummySeries.DummyTeams.DummyTeamTwo;
            this.dummyPlayer = this.dummyTeam.Players.ElementAt(0);
            this.duplicatePlayer = new Player(this.dummyPlayer.Name, this.dummyPlayer.DateOfBirth,
                this.dummyPlayer.Position, this.dummyPlayer.Status, this.dummyPlayer.Id);
            this.duplicatePlayer.TeamId = this.dummyPlayer.TeamId;

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

        #region PlayerService, FreeTextSearchForPlayers metod tests

        //[TestMethod]
        //public void FindPlayerFullName()
        //{
        //    var expectedName = this.dummyPlayer.Name;
        //    var nameFound = this.playerService.FreeTextSearchForPlayers(expectedName.FirstName + expectedName.LastName,
        //        StringComparison.InvariantCultureIgnoreCase);
        //}

        //[TestMethod]
        //public void FindPlayerCaseSensitive()
        //{
        //    var expectedPlayer =
        //        (Player)playerService.FreeTextSearchForPlayers("SeRGio RaMos", StringComparison.InvariantCultureIgnoreCase).First();

        //    var actualPlayerId = allPlayers.First(x => x.Name.ToString() == "Sergio Ramos").Id;

        //    Assert.AreEqual(expectedPlayer.Id, actualPlayerId);
        //}

        //[TestMethod]
        //public void FindPlayerPartOfFirstName()
        //{
        //    var expectedPlayer =
        //        (Player)playerService.FreeTextSearchForPlayers("ZLat", StringComparison.InvariantCultureIgnoreCase).First();

        //    var actualPlayerId = allPlayers.First(x => x.Name.ToString() == "Zlatan Ibrahimovic").Id;

        //    Assert.AreEqual(expectedPlayer.Id, actualPlayerId);
        //}

        //[TestMethod]
        //public void FindPlayerPartOfLastName()
        //{
        //    var expectedPlayer =
        //        (Player)playerService.FreeTextSearchForPlayers("Ibra", StringComparison.InvariantCultureIgnoreCase).First();

        //    var actualPlayerId = allPlayers.First(x => x.Name.ToString() == "Zlatan Ibrahimovic").Id;

        //    Assert.AreEqual(expectedPlayer.Id, actualPlayerId);
        //}

        [TestMethod]
        public void FindPlayerSpecialCharactersNotAllowed()
        {
            IPresentablePlayer expectedPlayerObj =
                this.playerService.FreeTextSearchForPlayers("Ibra@%", StringComparison.InvariantCultureIgnoreCase).FirstOrDefault();

            Assert.IsNull(expectedPlayerObj);
        }

        #endregion PlayerService, FreeTextSearchForPlayers metod tests

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
            var allPlayerStats =
                allPlayerInSeries.Select(player => player.AggregatedStats[dummySeries.SeriesDummy.Id]).ToList();
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
            var allPlayerStats =
                allPlayerInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
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
            var allPlayerStats =
                allPlayerInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
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
            var allPlayerStats =
                allPlayerInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
            var allPlayerStatsSorted = allPlayerStats.OrderByDescending(ps => ps.YellowCardCount).Take(5);
            for (int i = 0; i < topYellow.Count(); i++)
            {
                Assert.IsTrue(allPlayerStatsSorted.ElementAt(i).YellowCardCount ==
                              topYellow.ElementAt(i).YellowCardCount);
            }
        }

        [TestMethod]
        public void PlayerCanBeRenamedThroughDuplicate()
        {
            Assert.AreEqual(this.dummyPlayer.Name, this.duplicatePlayer.Name);
            this.playerService.RenamePlayer(this.duplicatePlayer, new Name("Torbjörn", "Nilsson"));
            this.dummyPlayer = this.playerService.FindById(this.dummyPlayer.Id);
            Assert.AreEqual(this.duplicatePlayer.Name, this.dummyPlayer.Name);
        }

        [TestMethod]
        public void PlayerCanBeRenamedThroughReference()
        {
            Assert.AreNotEqual(this.dummyPlayer.Name, new Name("Deigo", "Maradona"));
            this.playerService.RenamePlayer(this.dummyPlayer, new Name("Diego", "Maradona"));
            var repositoryPlayer = this.playerService.FindById(this.dummyPlayer.Id);
            Assert.AreEqual(this.dummyPlayer.Name, repositoryPlayer.Name);
        }

        [TestMethod]
        public void PlayerCanBeAssignedNewShirtNumberThroughDuplicate()
        {
            Assert.AreEqual(this.dummyPlayer.Id, this.duplicatePlayer.Id);
            Assert.IsNull(this.dummyPlayer.ShirtNumber.Value);
            Assert.IsNull(this.duplicatePlayer.ShirtNumber.Value);
            this.playerService.SetShirtNumber(this.duplicatePlayer, new ShirtNumber(7));
            var repositoryPlayer = this.playerService.FindById(this.dummyPlayer.Id);
            Assert.AreEqual(repositoryPlayer.ShirtNumber, this.duplicatePlayer.ShirtNumber);
        }

        [TestMethod]
        public void PlayerCanBeAssignedNewShirtNumberThroughReference()
        {
            Assert.IsNull(this.dummyPlayer.ShirtNumber.Value);
            this.playerService.SetShirtNumber(this.dummyPlayer.Id, new ShirtNumber(9));
            var repositoryPlayer = this.playerService.FindById(this.dummyPlayer.Id);
            Assert.IsNotNull(this.dummyPlayer.ShirtNumber.Value);
            Assert.AreEqual(this.dummyPlayer.ShirtNumber, repositoryPlayer.ShirtNumber);
        }

        [TestMethod]
        public void PlayerCanBeAssignedNewPosition()
        {
            Assert.AreNotEqual(this.dummyPlayer.Position, PlayerPosition.GoalKeeper);
            this.playerService.SetPlayerPosition(this.dummyPlayer.Id, PlayerPosition.GoalKeeper);
            Assert.AreEqual(this.dummyPlayer.Position, PlayerPosition.GoalKeeper);
        }

        [TestMethod]
        public void PlayerCanBeAssignedNewStatus()
        {
            Assert.AreNotEqual(this.dummyPlayer.Status, PlayerStatus.Injured);
            this.playerService.SetPlayerStatus(this.dummyPlayer.Id, PlayerStatus.Injured);
            Assert.AreEqual(this.dummyPlayer.Status, PlayerStatus.Injured);
        }
    }
}