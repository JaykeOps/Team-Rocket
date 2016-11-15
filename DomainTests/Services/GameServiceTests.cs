using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Services.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService gameService = new GameService();
        private MatchDuration matchDuration90Minutes = new MatchDuration(new TimeSpan(0, 90, 0));
        private Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        private Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));

        [TestMethod]
        public void AddGameToListTest()
        {
            //var game = new Game(matchDuration90Minutes, teamRed.Id, teamGreen.Id);
            //var game2 = new Game(matchDuration90Minutes, teamRed.Id, teamGreen.Id);
            var gameIsAdded = false;
            var game2IsAdded = false;

            //gameService.Add(game);

            //foreach (var gameItem in gameService.GetAll())
            //{
            //    if (game.Id == gameItem.Id)
            //    {
            //        gameIsAdded = true;
            //    }
            //    if (game2.Id == gameItem.Id)
            //    {
            //        game2IsAdded = true;
            //    }
            //}

            Assert.IsTrue(gameIsAdded == true);
            Assert.IsTrue(game2IsAdded == false);
        }

        [TestMethod]
        public void ConstructorInitiatesListOfGamesTest()
        {
            Assert.IsNotNull(gameService.GetAll());
        }

        [TestMethod]
        public void GetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(gameService.GetAll(), typeof(IEnumerable<Game>));
        }
    }
}