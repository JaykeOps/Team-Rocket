using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using DomainTests.Test_Dummies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories.Tests
{
    [TestClass]
    public class GameRepositoryTests
    {
        private Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), new SeriesName("Allsvenskan"));
        private MatchDateAndTime date = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));
        private GameRepository gameRepository = GameRepository.instance;
        private Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        private Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));
        private DummySeries dummySeries;
        private Game dummyGame;
        private Game dummyGameDuplicate;

        [TestInitialize]
        public void Initialize()
        {
            this.dummySeries = new DummySeries();
            this.dummyGame = this.dummySeries.DummyGames.GameOne;
            this.dummyGameDuplicate = new Game(this.dummySeries.SeriesDummy.Schedule.ElementAt(0))
            {
                Id = this.dummyGame.Id,
            };
        }

        [TestMethod]
        public void RepoInstancesAreTheSameObject()
        {
            var repoInstance1 = GameRepository.instance;
            var repoInstance2 = GameRepository.instance;

            Assert.AreSame(repoInstance1, repoInstance2);
        }

        [TestMethod]
        public void ConstructorInitiatesListOfGamesTest()
        {
            Assert.IsNotNull(this.gameRepository.GetAll());
        }

        [TestMethod]
        public void AddGameToListTest()
        {
            Match matchOne = new Match(this.teamRed.ArenaName, this.teamRed.Id, this.teamGreen.Id, this.series, this.date);

            var game = new Game(matchOne);
            var game2 = new Game(matchOne);
            var gameIsAdded = false;
            var game2IsAdded = false;

            this.gameRepository.Add(game);

            foreach (var gameItem in this.gameRepository.GetAll())
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
        public void GetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(this.gameRepository.GetAll(), typeof(IEnumerable<Game>));
        }

        [TestMethod]
        public void GameLoadTest()
        {
            var games = GameRepository.instance.GetAll();
            Assert.IsTrue(games.Count() != 0);
        }

        [TestMethod]
        public void TryGetGameWillReplaceRepositoryGameWithNewGameIfIdsAreEqual()
        {
            var gameInRepositroy = DomainService.FindGameById(this.dummyGame.Id);
            Assert.AreEqual(gameInRepositroy, this.dummyGame);
            this.dummyGame.Protocol.Goals.Clear();
            Assert.AreEqual(this.dummyGame.Id, this.dummyGameDuplicate.Id);
            this.dummyGameDuplicate.Protocol.Goals.Add(new Goal(new MatchMinute(5), new Guid(), new Guid()));
            Assert.AreNotEqual(this.dummyGame.Protocol.Goals.Count, this.dummyGameDuplicate.Protocol.Goals.Count);
            this.gameRepository.Add(this.dummyGameDuplicate);
            gameInRepositroy = this.gameRepository.GetAll().First(x => x.Id == this.dummyGame.Id);
            Assert.AreEqual(gameInRepositroy.Protocol.Goals.Count, this.dummyGameDuplicate.Protocol.Goals.Count);
            Assert.AreEqual(gameInRepositroy, this.dummyGameDuplicate);
        }

        /*[TestMethod]
        public void RepositoriesCanGenerateBinFiles()
        {
            new DummySeries();
            GameRepository.instance.SaveData();
            MatchRepository.instance.SaveData();
            PlayerRepository.instance.SaveData();
            SeriesRepository.instance.SaveData();
            TeamRepository.instance.SaveData();
        }

        [TestMethod]
        public void RepositoriesCanLoadBinFiles()
        {
            GameRepository.instance.LoadData();
            MatchRepository.instance.LoadData();
            PlayerRepository.instance.LoadData();
            SeriesRepository.instance.LoadData();
            TeamRepository.instance.LoadData();
        }*/
    }
}