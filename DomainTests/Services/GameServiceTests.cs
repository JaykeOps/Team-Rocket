using Domain.Entities;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService gameService = new GameService();
        private Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan"));
        private MatchDateAndTime date = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));

        private Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        private Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));
        private DummySeries dummySeries;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
        }

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
            var game1 = new Game(series.SeriesDummy.Schedule.ElementAt(0));
            var game2 = new Game(series.SeriesDummy.Schedule.ElementAt(1));
            var game3 = new Game(series.SeriesDummy.Schedule.ElementAt(2));

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

        /*   [TestMethod]
           public void AddListOfGamesUsingMatchIds()
           {
               var series = new DummySeries();
               var matchs = series.SeriesDummy.Schedule.Values.SelectMany(matchess => matchess).ToList();
               DomainService.AddMatches(matchs);
               var matchIds = (from matches in series.SeriesDummy.Schedule from match in matches select match.Id).ToList();
               var numOfGamesPriorAdd = gameService.GetAll().Count();
               gameService.AddList(matchIds);
               var numOfGamesAfterAdd = gameService.GetAll().Count();
               Assert.IsTrue(matchIds.Count == numOfGamesAfterAdd - numOfGamesPriorAdd);
           }*/

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
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameGoalsPriorGame = game.Protocol.Goals.Count;
            var teamGoalsPriorGame = team.AggregatedStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            this.gameService.AddGoalToGame(game.Id, player.Id, 78);
            var gameGoalsAfterGame = game.Protocol.Goals.Count;
            var teamGoalsAfterGame = team.AggregatedStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsAfterGame = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
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
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameAssistsPriorGame = game.Protocol.Assists.Count;
            var playerAssistsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].AssistCount;
            this.gameService.AddAssistToGame(game.Id, player.Id, 78);
            var gameAssistsAfterGame = game.Protocol.Assists.Count;
            var playerAssistsAfterGame = player.AggregatedStats[series.SeriesDummy.Id].AssistCount;
            Assert.IsTrue(gameAssistsPriorGame == gameAssistsAfterGame - 1);
            Assert.IsTrue(playerAssistsPriorGame == playerAssistsAfterGame - 1);
        }

        [TestMethod]
        public void AddYellowCardToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameYellowCardsPriorGame = game.Protocol.Cards.Count;
            var playerYellowCardsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].YellowCardCount;
            this.gameService.AddYellowCardToGame(game.Id, player.Id, 78);
            var gameYellowCardsAfterGame = game.Protocol.Cards.Count;
            var playerYellowCardsAfterGame = player.AggregatedStats[series.SeriesDummy.Id].YellowCardCount;
            Assert.IsTrue(gameYellowCardsPriorGame == gameYellowCardsAfterGame - 1);
            Assert.IsTrue(playerYellowCardsPriorGame == playerYellowCardsAfterGame - 1);
        }

        [TestMethod]
        public void AddRedCardToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameRedCardsPriorGame = game.Protocol.Cards.Count;
            var playerRedCardsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].RedCardCount;
            this.gameService.AddRedCardToGame(game.Id, player.Id, 78);
            var gameRedCardsAfterGame = game.Protocol.Cards.Count;
            var playerRedCardsAfterGame = player.AggregatedStats[series.SeriesDummy.Id].RedCardCount;
            Assert.IsTrue(gameRedCardsPriorGame == gameRedCardsAfterGame - 1);
            Assert.IsTrue(playerRedCardsPriorGame == playerRedCardsAfterGame - 1);
        }

        [TestMethod]
        public void AddPenaltyToGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gamePenatliesPriorGame = game.Protocol.Penalties.Count;
            var playerPenatliesPriorGame = player.AggregatedStats[series.SeriesDummy.Id].PenaltyCount;
            this.gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            var gamePenatliesAfterGame = game.Protocol.Penalties.Count;
            var playerPenatliesAfterGame = player.AggregatedStats[series.SeriesDummy.Id].PenaltyCount;
            Assert.IsTrue(gamePenatliesPriorGame == gamePenatliesAfterGame - 1);
            Assert.IsTrue(playerPenatliesPriorGame == playerPenatliesAfterGame - 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddGameThrowsExIfMatchIdCantBeFound()
        {
            this.gameService.Add(new Guid());
        }

        [TestMethod]
        public void RemoveGoalFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameGoalsPriorGame = game.Protocol.Goals.Count;
            var teamGoalsPriorGame = team.AggregatedStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            this.gameService.AddGoalToGame(game.Id, player.Id, 78);
            this.gameService.RemoveGoalFromGame(game.Id, player.Id, 78);
            var gameGoalsAfterRemove = game.Protocol.Goals.Count;
            var teamGoalsAfterRemove = team.AggregatedStats[series.SeriesDummy.Id].GoalDifference;
            var playerGoalsAfterRemove = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
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
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameAssistsPriorGame = game.Protocol.Assists.Count;
            var playerAssistsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].AssistCount;
            this.gameService.AddAssistToGame(game.Id, player.Id, 78);
            this.gameService.RemoveAssistFromGame(game.Id, player.Id, 78);
            var gameAssistsAfterRemove = game.Protocol.Assists.Count;
            var playerAssistsAfterRemove = player.AggregatedStats[series.SeriesDummy.Id].AssistCount;
            Assert.IsTrue(gameAssistsPriorGame == gameAssistsAfterRemove);
            Assert.IsTrue(playerAssistsPriorGame == playerAssistsAfterRemove);
        }

        [TestMethod]
        public void RemoveYellowCardFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameYellowCardsPriorGame = game.Protocol.Cards.Count;
            var playerYellowCardsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].YellowCardCount;
            this.gameService.AddYellowCardToGame(game.Id, player.Id, 78);
            this.gameService.RemoveYellowCardFromGame(game.Id, player.Id, 78);
            var gameYellowCardsAfterRemove = game.Protocol.Cards.Count;
            var playerYellowCardsAfterRemove = player.AggregatedStats[series.SeriesDummy.Id].YellowCardCount;
            Assert.IsTrue(gameYellowCardsPriorGame == gameYellowCardsAfterRemove);
            Assert.IsTrue(playerYellowCardsPriorGame == playerYellowCardsAfterRemove);
        }

        [TestMethod]
        public void RemoveRedCardFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameRedCardsPriorGame = game.Protocol.Cards.Count;
            var playerRedCardsPriorGame = player.AggregatedStats[series.SeriesDummy.Id].RedCardCount;
            this.gameService.AddRedCardToGame(game.Id, player.Id, 78);
            this.gameService.RemoveRedCardFromGame(game.Id, player.Id, 78);
            var gameRedCardsAfterRemove = game.Protocol.Cards.Count;
            var playerRedCardsAfterRemove = player.AggregatedStats[series.SeriesDummy.Id].RedCardCount;
            Assert.IsTrue(gameRedCardsPriorGame == gameRedCardsAfterRemove);
            Assert.IsTrue(playerRedCardsPriorGame == playerRedCardsAfterRemove);
        }

        [TestMethod]
        public void RemovePenaltyFromGame()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gamePenatliesPriorGame = game.Protocol.Penalties.Count;
            var playerPenatliesPriorGame = player.AggregatedStats[series.SeriesDummy.Id].PenaltyCount;
            this.gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            this.gameService.RemovePenaltyFromGame(game.Id, player.Id, 78);
            var gamePenatliesAfterRemove = game.Protocol.Penalties.Count;
            var playerPenatliesAfterRemove = player.AggregatedStats[series.SeriesDummy.Id].PenaltyCount;
            Assert.IsTrue(gamePenatliesPriorGame == gamePenatliesAfterRemove);
            Assert.IsTrue(playerPenatliesPriorGame == playerPenatliesAfterRemove);
        }

        [TestMethod]
        public void PenaltyAddsGoalIfPenaltyIsGoal()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameGoalsPriorPenalty = game.Protocol.Goals.Count;
            var playerGoalsPriorPenalty = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            this.gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            var gameGoalsAfterPenalty = game.Protocol.Goals.Count;
            var playerGoalsAfterAfterPenalty = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsPriorPenalty == gameGoalsAfterPenalty - 1);
            Assert.IsTrue(playerGoalsPriorPenalty == playerGoalsAfterAfterPenalty - 1);
        }

        [TestMethod]
        public void PenaltyDosnetAddGoalIfPenaltyIsNotGoal()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameGoalsPriorPenalty = game.Protocol.Goals.Count;
            var playerGoalsPriorPenalty = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            this.gameService.AddPenaltyToGame(game.Id, player.Id, 78, false);
            var gameGoalsAfterPenalty = game.Protocol.Goals.Count;
            var playerGoalsAfterAfterPenalty = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsPriorPenalty == gameGoalsAfterPenalty);
            Assert.IsTrue(playerGoalsPriorPenalty == playerGoalsAfterAfterPenalty);
        }

        [TestMethod]
        public void IfPenaltyThatIsGoalIsRemovedGoalIsAlsoRemoved()
        {
            var series = new DummySeries();
            var game = series.DummyGames.GameTwo;
            var team = DomainService.FindTeamById(game.HomeTeamId);
            var player = DomainService.FindPlayerById(team.PlayerIds.First());
            var gameGoalsPriorRemoveOfPenalty = game.Protocol.Goals.Count;
            var playerGoalsPrioRemoveOfPenalty = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            this.gameService.AddPenaltyToGame(game.Id, player.Id, 78, true);
            this.gameService.RemovePenaltyFromGame(game.Id, player.Id, 78);
            var gameGoalsAfterRemoveOfPenalty = game.Protocol.Goals.Count;
            var playerGoalsAfterAfterRemovOfPenalty = player.AggregatedStats[series.SeriesDummy.Id].GoalCount;
            Assert.IsTrue(gameGoalsPriorRemoveOfPenalty == gameGoalsAfterRemoveOfPenalty);
            Assert.IsTrue(playerGoalsPrioRemoveOfPenalty == playerGoalsAfterAfterRemovOfPenalty);
        }

        [TestMethod] //Works if you run it solo!
        public void GameSearchCanReturnGamesContainingSpecifiedArenaName()
        {
            var series = new DummySeries();
            var games = this.gameService.Search("Dummy ArenaOne");
            foreach (var game in games)
            {
                Assert.AreEqual(game.Location.ToString(), "Dummy ArenaOne");
            }
        }

        [TestMethod]
        public void GameSearchCanReturnGamesContainingSpecifiedDate()
        {
            var series = new DummySeries();
            var games = this.gameService.Search("2017-08-22").ToList();
            foreach (var game in games.ToList())
            {
                Assert.AreEqual(game.MatchDate.ToString(), "2017-08-22 10:10");
            }
        }

        [TestMethod]
        public void GameSearchCanReturnGamesInSeries()
        {
            //TODO: Update when more series are available!
            var series = new DummySeries();
            var games = this.gameService.Search("The Dummy Series").ToList();
            Assert.IsNotNull(games);
            Assert.AreNotEqual(games.Count, 0);
            foreach (var game in games)
            {
                Assert.AreEqual(DomainService.FindSeriesById(game.SeriesId).SeriesName.Value, "The Dummy Series");
            }
        }

        [TestMethod]
        public void GameSearchCanReturnGamesContainingActivePlayer()
        {
            //TODO: Cannot be tried until games are populated with game squads!
            var series = new DummySeries();
            var games = this.gameService.Search("Player One").ToList();
            Assert.IsNotNull(games);
            Assert.AreNotEqual(games.Count, 0);
            foreach (var game in games)
            {
                Assert.IsTrue(
                    game.Protocol.HomeTeamActivePlayers.Any
                    (x => DomainService.FindPlayerById(x).Name.ToString() == "Player One")
                    ||
                    game.Protocol.AwayTeamActivePlayers.Any
                    (x => DomainService.FindPlayerById(x).Name.ToString() == "Player One"));
            }
            Assert.Fail();
        }

        [TestMethod]
        public void GameSearchCanReturnGamesContainingTeam()
        {
            var games = this.gameService.Search("Dummy TeamTh").ToList();
            Assert.IsNotNull(games);
            Assert.AreNotEqual(games.Count, 0);
            foreach (var game in games)
            {
                Assert.IsTrue(DomainService.FindTeamById(game.HomeTeamId).Name.ToString() == "Dummy TeamThree"
                    || DomainService.FindTeamById(game.AwayTeamId).Name.ToString() == "Dummy TeamThree");
            }
        }

        [TestMethod]
        public void GetGameFromMatchTest()
        {
            var service = new GameService();
            var matchService = new MatchService();
            var match = new Match(new ArenaName("test"), Guid.NewGuid(), Guid.NewGuid(), dummySeries.SeriesDummy);
            var game = service.GetGameFromMatch(match);
            Assert.IsTrue(game == null);
            matchService.Add(match);
            service.Add(match.Id);
            game = service.GetGameFromMatch(match);
            Assert.IsTrue(game != null);
        }

        [TestMethod]
        public void GetAllEventsFromGame()
        {
            var game = dummySeries.DummyGames.GameOne;
            var events = gameService.GetAllEventsFromGame(game);
            Assert.IsTrue(events != null);
        }

        [TestMethod]
        public void RemoveEventWorks()
        {
            var game = dummySeries.DummyGames.GameFive;
            var events = gameService.GetAllEventsFromGame(game);
            foreach (var _event in events)
            {
                var eventCountPriorRemove = gameService.GetAllEventsFromGame(game).Count();
                gameService.RemoveEvent(_event, game.Id);
                var eventsAfterRemove = gameService.GetAllEventsFromGame(game);
                Assert.IsTrue(eventCountPriorRemove == eventsAfterRemove.Count() + 1);
                if (events.Count() == 1)
                {
                    break;
                }
            }
        }
    }
}