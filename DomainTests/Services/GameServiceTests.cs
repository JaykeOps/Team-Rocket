using Domain.Entities;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
        public void AddGameListTest()
        {
            Match matchOne = new Match(this.teamRed.ArenaName, this.teamRed.Id, this.teamGreen.Id, this.series, this.date);
            var gameIsAdded = false;
            var matchService = new MatchService();
            matchService.Add(matchOne);
            var gameId = this.gameService.Add(matchOne.Id);

            foreach (var gameItem in this.gameService.GetAll())
            {
                if (gameId == gameItem.Id)
                {
                    gameIsAdded = true;
                }
            }

            Assert.IsTrue(gameIsAdded);
        }

        [TestMethod]
        public void AddListOfGames()
        {
            var series = new DummySeries();
            var game1 = new Game(series.SeriesDummy.Schedule[0].ElementAt(0));
            var game2 = new Game(series.SeriesDummy.Schedule[0].ElementAt(1));
            var game3 = new Game(series.SeriesDummy.Schedule[1].ElementAt(0));

            var games = new List<Game>
            {
                game1,
                game2
            };
            gameService.Add(games);
            var allGames = DomainService.GetAllGames();
            Assert.IsTrue(allGames.Contains(game1));
            Assert.IsTrue(allGames.Contains(game2));
            Assert.IsFalse(allGames.Contains(game3));
        }

        [TestMethod]
        public void AddListOfGamesUsingMatchIds()
        {
            var series = new DummySeries();
            var matchs = series.SeriesDummy.Schedule.Values.SelectMany(matchess => matchess).ToList();
            DomainService.AddMatches(matchs);
            var matchIds = (from matches in series.SeriesDummy.Schedule.Values from match in matches select match.Id).ToList();
            var numOfGamesPriorAdd = gameService.GetAll().Count();
            gameService.AddList(matchIds);
            var numOfGamesAfterAdd = gameService.GetAll().Count();
            Assert.IsTrue(matchIds.Count == numOfGamesAfterAdd - numOfGamesPriorAdd);

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
            gameService.AddGoalToGame(game.Id, player.Id, 78);
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
            gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            var gamePenatliesAfterGame = game.Protocol.Penalties.Count;
            var playerPenatliesAfterGame = player.PresentableSeriesStats[series.SeriesDummy.Id].PenaltyCount;
            Assert.IsTrue(gamePenatliesPriorGame == gamePenatliesAfterGame - 1);
            Assert.IsTrue(playerPenatliesPriorGame == playerPenatliesAfterGame - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddGameThrowsExIfMatchIdCantBeFound()
        {
            gameService.Add(new Guid());
        }
        [TestMethod]
        public void RemoveGoalFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameGoalsPriorGame = game.Protocol.Goals.Count;
            var teamGoalsPriorGame = team.PresentableSeriesStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            gameService.AddGoalToGame(game.Id, player.Id, 78);
            gameService.RemoveGoalFromGame(game.Id, player.Id, 78);
            var gameGoalsAfterRemove = game.Protocol.Goals.Count;
            var teamGoalsAfterRemove = team.PresentableSeriesStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsAfterRemove = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsAfterRemove == gameGoalsPriorGame);
            Assert.IsTrue(teamGoalsAfterRemove == teamGoalsPriorGame);
            Assert.IsTrue(playerGoalsAfterRemove == playerGoalsPriorGame);
        }
        [TestMethod]
        public void RemoveAssistFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameAssistsPriorGame = game.Protocol.Assists.Count;
            var playerAssistsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].AssistCount;
            gameService.AddAssistToGame(game.Id, player.Id, 78);
            gameService.RemoveAssistFromGame(game.Id, player.Id, 78);
            var gameAssistsAfterRemove = game.Protocol.Assists.Count;
            var playerAssistsAfterRemove = player.PresentableSeriesStats[series.SeriesDummy.Id].AssistCount;
            Assert.IsTrue(gameAssistsPriorGame == gameAssistsAfterRemove);
            Assert.IsTrue(playerAssistsPriorGame == playerAssistsAfterRemove);
        }
        [TestMethod]
        public void RemoveYellowCardFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameYellowCardsPriorGame = game.Protocol.Cards.Count;
            var playerYellowCardsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].YellowCardCount;
            gameService.AddYellowCardToGame(game.Id, player.Id, 78);
            gameService.RemoveYellowCardFromGame(game.Id, player.Id, 78);
            var gameYellowCardsAfterRemove = game.Protocol.Cards.Count;
            var playerYellowCardsAfterRemove = player.PresentableSeriesStats[series.SeriesDummy.Id].YellowCardCount;
            Assert.IsTrue(gameYellowCardsPriorGame == gameYellowCardsAfterRemove);
            Assert.IsTrue(playerYellowCardsPriorGame == playerYellowCardsAfterRemove);
        }
        [TestMethod]
        public void RemoveRedCardFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameRedCardsPriorGame = game.Protocol.Cards.Count;
            var playerRedCardsPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].RedCardCount;
            gameService.AddRedCardToGame(game.Id, player.Id, 78);
            gameService.RemoveRedCardFromGame(game.Id, player.Id, 78);
            var gameRedCardsAfterRemove = game.Protocol.Cards.Count;
            var playerRedCardsAfterRemove = player.PresentableSeriesStats[series.SeriesDummy.Id].RedCardCount;
            Assert.IsTrue(gameRedCardsPriorGame == gameRedCardsAfterRemove);
            Assert.IsTrue(playerRedCardsPriorGame == playerRedCardsAfterRemove);
        }
        [TestMethod]
        public void RemovePenaltyFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gamePenatliesPriorGame = game.Protocol.Penalties.Count;
            var playerPenatliesPriorGame = player.PresentableSeriesStats[series.SeriesDummy.Id].PenaltyCount;
            gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            gameService.RemovePenaltyFromGame(game.Id, player.Id, 78);
            var gamePenatliesAfterRemove = game.Protocol.Penalties.Count;
            var playerPenatliesAfterRemove = player.PresentableSeriesStats[series.SeriesDummy.Id].PenaltyCount;
            Assert.IsTrue(gamePenatliesPriorGame == gamePenatliesAfterRemove);
            Assert.IsTrue(playerPenatliesPriorGame == playerPenatliesAfterRemove);
        }

        [TestMethod]
        public void PenaltyAddsGoalIfPenaltyIsGoal()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameGoalsPriorPenalty = game.Protocol.Goals.Count;
            var playerGoalsPriorPenalty = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            var gameGoalsAfterPenalty = game.Protocol.Goals.Count;
            var playerGoalsAfterAfterPenalty = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsPriorPenalty == gameGoalsAfterPenalty - 1);
            Assert.IsTrue(playerGoalsPriorPenalty == playerGoalsAfterAfterPenalty - 1);
        }
        [TestMethod]
        public void PenaltyDosnetAddGoalIfPenaltyIsNotGoal()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameGoalsPriorPenalty = game.Protocol.Goals.Count;
            var playerGoalsPriorPenalty = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            gameService.AddPenaltyToGame(game.Id, player.Id, 78, false);
            var gameGoalsAfterPenalty = game.Protocol.Goals.Count;
            var playerGoalsAfterAfterPenalty = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsPriorPenalty == gameGoalsAfterPenalty);
            Assert.IsTrue(playerGoalsPriorPenalty == playerGoalsAfterAfterPenalty);
        }
        [TestMethod]
        public void IfPenaltyThatIsGoalIsRemovedGoalIsAlsoRemoved()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = team.Players.First();
            var gameGoalsPriorRemoveOfPenalty = game.Protocol.Goals.Count;
            var playerGoalsPrioRemoveOfPenalty = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            gameService.RemovePenaltyFromGame(game.Id, player.Id, 78);
            var gameGoalsAfterRemoveOfPenalty = game.Protocol.Goals.Count;
            var playerGoalsAfterAfterRemovOfPenalty = player.PresentableSeriesStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsPriorRemoveOfPenalty == gameGoalsAfterRemoveOfPenalty);
            Assert.IsTrue(playerGoalsPrioRemoveOfPenalty == playerGoalsAfterAfterRemovOfPenalty);
        }
    }
}