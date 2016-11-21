using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using DomainTests.Test_Dummies;

namespace Domain.Services.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService gameService = new GameService();
        private Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");
        private MatchDateAndTime date = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));

        private Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        private Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));

        [TestMethod]
        public void AddGameToListTest()
        {
            Match matchOne = new Match(this.teamRed.ArenaName, this.teamRed.Id, this.teamGreen.Id, this.series, this.date);
            var game = new Game(matchOne);
            var game2 = new Game(matchOne);
            var gameIsAdded = false;
            var game2IsAdded = false;
            var matchId = DomainService.GetAllMatches().First().Id;
            this.gameService.Add(matchId);

            foreach (var gameItem in this.gameService.GetAll())
            {
                if (game.Id == gameItem.Id)
                {
                    gameIsAdded = true;
                }
                if (game2.Id == gameItem.Id)
                {
                    game2IsAdded = true;
                }
            }

            Assert.IsTrue(gameIsAdded == true);
            Assert.IsTrue(game2IsAdded == false);
        }

        [TestMethod]
        public void ConstructorInitiatesListOfGamesTest()
        {
            Assert.IsNotNull(this.gameService.GetAll());
        }

        [TestMethod]
        public void GetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(this.gameService.GetAll(), typeof(IEnumerable<Game>));
        }

        [TestMethod]
        public void AddGoalToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameGoalsPriorGame = game.Protocol.Goals.Count;
            var teamGoalsPriorGame = team.PresentableSeriesStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            gameService.AddGoalToGame(game.Id, game.HomeTeamId, player.Id, 78);
            var gameGoalsAfterGame = game.Protocol.Goals.Count;
            var teamGoalsAfterGame = team.PresentableSeriesStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsAfterGame = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsPriorGame == gameGoalsAfterGame - 1);
            Assert.IsTrue(teamGoalsPriorGame == teamGoalsAfterGame - 1);
            Assert.IsTrue(playerGoalsPriorGame == playerGoalsAfterGame - 1);
        }

        [TestMethod]
        public void AddAssistToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameAssistsPriorGame = game.Protocol.Assists.Count;
            var playerAssistsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].AssistCount;
            gameService.AddAssistToGame(game.Id, player.Id, 78);
            var gameAssistsAfterGame = game.Protocol.Assists.Count;
            var playerAssistsAfterGame = player.PresentableSeriesStats[series.SeriesDummy.Id].AssistCount;
            Assert.IsTrue(gameAssistsPriorGame == gameAssistsAfterGame - 1);
            Assert.IsTrue(playerAssistsPriorGame == playerAssistsAfterGame - 1);
        }
        [TestMethod]
        public void AddYellowCardToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameYellowCardsPriorGame = game.Protocol.Cards.Count;
            var playerYellowCardsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].YellowCardCount;
            gameService.AddYellowCardToGame(game.Id, player.Id, 78);
            var gameYellowCardsAfterGame = game.Protocol.Cards.Count;
            var playerYellowCardsAfterGame = player.PresentableSeriesStats[series.SeriesDummy.Id].YellowCardCount;
            Assert.IsTrue(gameYellowCardsPriorGame == gameYellowCardsAfterGame - 1);
            Assert.IsTrue(playerYellowCardsPriorGame == playerYellowCardsAfterGame - 1);
        }
        [TestMethod]
        public void AddRedCardToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameRedCardsPriorGame = game.Protocol.Cards.Count;
            var playerRedCardsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].RedCardCount;
            gameService.AddRedCardToGame(game.Id, player.Id, 78);
            var gameRedCardsAfterGame = game.Protocol.Cards.Count;
            var playerRedCardsAfterGame = player.PresentableSeriesStats[series.SeriesDummy.Id].RedCardCount;
            Assert.IsTrue(gameRedCardsPriorGame == gameRedCardsAfterGame - 1);
            Assert.IsTrue(playerRedCardsPriorGame == playerRedCardsAfterGame - 1);
        }
        [TestMethod]
        public void AddPenaltyToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gamePenatliesPriorGame = game.Protocol.Penalties.Count;
            var playerPenatliesPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].PenaltyCount;
            gameService.AddPenaltyToGame(game.Id, player.Id, 78);
            var gamePenatliesAfterGame = game.Protocol.Penalties.Count;
            var playerPenatliesAfterGame = player.PresentableSeriesStats[series.SeriesDummy.Id].PenaltyCount;
            Assert.IsTrue(gamePenatliesPriorGame == gamePenatliesAfterGame - 1);
            Assert.IsTrue(playerPenatliesPriorGame == playerPenatliesAfterGame - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddGameThrowsEx()
        {
            gameService.Add(new Guid());
        }

    }
}