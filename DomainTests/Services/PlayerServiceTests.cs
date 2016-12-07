using Domain.Entities;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            this.dummyPlayer = DomainService.FindPlayerById(this.dummyTeam.PlayerIds.ElementAt(0));
            this.duplicatePlayer = new Player(this.dummyPlayer.Name, this.dummyPlayer.DateOfBirth,
                this.dummyPlayer.Position, this.dummyPlayer.Status, this.dummyPlayer.Id);
            this.duplicatePlayer.TeamId = this.dummyPlayer.TeamId;
        }

        [TestMethod]
        public void GetAllPlayersNotNull()
        {
            var getAllPlayers = this.playerService.GetAllExposablePlayers();
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

        [TestMethod]
        public void GetTopScorersTest()
        {
            var dummySeries = new DummySeries();
            var topScorers = this.playerService.GetTopScorersForSeries(dummySeries.SeriesDummy.Id);

            var allTeamsInSeries = dummySeries.SeriesDummy.TeamIds.Select(id => DomainService.FindTeamById(id)).ToList();
            var allPlayerIdsInSereis = allTeamsInSeries.SelectMany(team => team.PlayerIds).ToList();
            var allPlayersInSeries = allPlayerIdsInSereis.Select(x => DomainService.FindPlayerById(x));
            var allPlayerStats =
                allPlayersInSeries.Select(player => player.AggregatedStats[dummySeries.SeriesDummy.Id]).ToList();
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
            var allPlayerIdsInSeries = allTeamsInSeries.SelectMany(team => team.PlayerIds).ToList();
            var allPlayersInSeries = allPlayerIdsInSeries.Select(x => DomainService.FindPlayerById(x));
            var allPlayerStats =
                allPlayersInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
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
            var allPlayerIdsInSeries = allTeamsInSeries.SelectMany(x => x.PlayerIds);
            var allPlayerInSeries = allPlayerIdsInSeries.Select(x => DomainService.FindPlayerById(x));
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
            var allPlayersIdsInSeries = allTeamsInSeries.SelectMany(x => x.PlayerIds).ToList();
            var allPlayerInSeries = allPlayersIdsInSeries.Select(x => DomainService.FindPlayerById(x));
            var allPlayerStats =
                allPlayerInSeries.Select(player => player.AggregatedStats[series.SeriesDummy.Id]).ToList();
            var allPlayerStatsSorted = allPlayerStats.OrderByDescending(ps => ps.YellowCardCount).Take(5);
            for (int i = 0; i < topYellow.Count(); i++)
            {
                Assert.IsTrue(allPlayerStatsSorted.ElementAt(i).YellowCardCount ==
                              topYellow.ElementAt(i).YellowCardCount);
            }
        }

        //[TestMethod]
        //public void PlayerCanBeRenamedThroughDuplicate()
        //{
        //    Assert.AreEqual(this.dummyPlayer.Name, this.duplicatePlayer.Name);
        //    this.playerService.RenamePlayer(this.duplicatePlayer, new Name("Torbjörn", "Nilsson"));
        //    this.dummyPlayer = this.playerService.FindById(this.dummyPlayer.Id);
        //    Assert.AreEqual(this.duplicatePlayer.Name, this.dummyPlayer.Name);
        //}

        //[TestMethod]
        //public void PlayerCanBeRenamedThroughReference()
        //{
        //    Assert.AreNotEqual(this.dummyPlayer.Name, new Name("Deigo", "Maradona"));
        //    this.playerService.RenamePlayer(this.dummyPlayer, new Name("Diego", "Maradona"));
        //    var repositoryPlayer = this.playerService.FindById(this.dummyPlayer.Id);
        //    Assert.AreEqual(this.dummyPlayer.Name, repositoryPlayer.Name);
        //}

        [TestMethod]
        public void PlayerCanBeAssignedNewShirtNumberThroughReference()
        {
            this.playerService.SetShirtNumber(this.dummyPlayer.Id, new ShirtNumber(this.dummyPlayer.TeamId, 9));
            var repositoryPlayer = this.playerService.FindById(this.dummyPlayer.Id);
            Assert.IsNotNull(this.dummyPlayer.ShirtNumber.Value);
            Assert.AreEqual(this.dummyPlayer.ShirtNumber, repositoryPlayer.ShirtNumber);
        }

        //[TestMethod]
        //public void PlayerCanBeAssignedNewPosition()
        //{
        //    Assert.AreNotEqual(this.dummyPlayer.Position, PlayerPosition.GoalKeeper);
        //    this.playerService.SetPlayerPosition(this.dummyPlayer.Id, PlayerPosition.GoalKeeper);
        //    Assert.AreEqual(this.dummyPlayer.Position, PlayerPosition.GoalKeeper);
        //}

        //[TestMethod]
        //public void PlayerCanBeAssignedNewStatus()
        //{
        //    Assert.AreNotEqual(this.dummyPlayer.Status, PlayerStatus.Injured);
        //    this.playerService.SetPlayerStatus(this.dummyPlayer.Id, PlayerStatus.Injured);
        //    Assert.AreEqual(this.dummyPlayer.Status, PlayerStatus.Injured);
        //}

        [TestMethod]
        public void PlayerCanBeAssignedAnEmailAddress()
        {
            var newEmail = new EmailAddress("tester@testmail.com");
            this.dummyPlayer.ContactInformation.Email = newEmail;
            Assert.AreEqual(this.dummyPlayer.ContactInformation.Email, newEmail);
        }

        [TestMethod]
        public void PlayerCanBeAssignedAPhoneNumber()
        {
            var newPhone = new PhoneNumber("0739-887722");
            this.dummyPlayer.ContactInformation.Phone = newPhone;
            Assert.AreEqual(this.dummyPlayer.ContactInformation.Phone, newPhone);
        }

        [TestMethod]
        public void AddListOfPlayerTest()
        {
            var series = new DummySeries();
            var playerOne = new Player(new Name("Kalle", "Kuling"), new DateOfBirth("2012-06-12"), PlayerPosition.Forward, PlayerStatus.Available);
            var playerTwo = new Player(new Name("Kalle", "Kuling"), new DateOfBirth("2012-06-12"), PlayerPosition.Forward, PlayerStatus.Available);
            var playerThree = new Player(new Name("Kalle", "Kuling"), new DateOfBirth("2012-06-12"), PlayerPosition.Forward, PlayerStatus.Available);

            var players = new List<Player>
            {
                playerOne,
                playerTwo
            };
            playerService.Add(players);
            var allPlayers = DomainService.GetAllPlayers();
            Assert.IsTrue(allPlayers.Contains(playerOne));
            Assert.IsTrue(allPlayers.Contains(playerTwo));
            Assert.IsFalse(allPlayers.Contains(playerThree));
        }

        [TestMethod]
        public void PlayerSearchCanReturnPlayersWithSpecifiedName()
        {
            var series = new DummySeries();
            var players = this.playerService.Search("Player One").ToList();
            Assert.IsNotNull(players);
            Assert.AreNotEqual(players.Count, 0);
            foreach (var player in players)
            {
                Assert.AreEqual(player.Name.ToString(), "Player One");
            }
        }

        [TestMethod]
        public void PlayerSearchCanReturnPlayersWithSpecifiedDateOfBirth()
        {
            var players = this.playerService.Search("1995-01-02").ToList();
            Assert.IsNotNull(players);
            Assert.AreNotEqual(players.Count, 0);
            foreach (var player in players)
            {
                Assert.AreEqual(player.Name.ToString(), "Player Three");
                Assert.AreEqual(player.DateOfBirth.ToString(), "1995-01-02");
            }
        }

        [TestMethod]
        public void PlayerSearchCanReturnPlayersBelongingToSpecifiedTeam()
        {
            var players = this.playerService.Search("Dummy TeamOne").ToList();
            Assert.IsNotNull(players);
            Assert.AreNotEqual(players.Count, 0);
            foreach (var player in players)
            {
                Assert.AreEqual(player.AffiliatedTeamName.ToString(), "Dummy TeamOne");
            }
        }

        [TestMethod]
        public void RemovePlayerWorks()
        {
            var series = new DummySeries();
            var playerToRemove = DomainService.FindTeamById(series.SeriesDummy.
                TeamIds.ElementAt(0)).PlayerIds.ElementAt(0);
            playerService.RemovePlayer(playerToRemove);
            Assert.IsTrue(!(playerService.GetAllPlayers().Contains(DomainService.FindPlayerById(playerToRemove))));
        }

        [TestMethod]
        public void AddingTeamIdToPlayerReflectsOnTeam()
        {
            var dummyTeamOne = this.dummySeries.DummyTeams.DummyTeamOne;
            Assert.AreEqual(this.dummyPlayer.TeamId, this.dummyTeam.Id);
            this.playerService.AssignPlayerToTeam(this.dummyPlayer, dummyTeamOne.Id);
            Assert.AreEqual(this.dummyPlayer.TeamId, dummyTeamOne.Id);
            Assert.AreNotEqual(this.dummyPlayer.TeamId, this.dummyTeam.Id);
        }

        [TestMethod]
        public void FreeTextSearchPlayerStats()
        {
            var series = new DummySeries();
            var playerStats = this.playerService.GetPlayerStatsFreeTextSearch("Player One").ToList();
            Assert.IsNotNull(playerStats);
            Assert.AreNotEqual(playerStats.Count, 0);
            foreach (var playerStat in playerStats)
            {
                Assert.AreEqual(playerStat.PlayerName.ToString(), "Player One");
            }
        }
    }
}