using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repositories.Tests
{
    [TestClass]
    public class GameRepositoryTests
    {
        private Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16), "Allsvenskan");
        private MatchDateAndTime date = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));
        private GameRepository gameRepository = GameRepository.instance;

        private Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        private Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));

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

        //TODO: Write test for GameRepository duplicate validation!
    }
}