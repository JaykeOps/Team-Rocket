using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.CustomExceptions;
using Domain.Value_Objects;
using Domain.Entities;

namespace Domain.Repositories.Tests
{
    [TestClass]
    public class GameRepositoryTests
    {
        GameRepository gameRepository = GameRepository.instance;
        MatchDuration matchDuration90Minutes = new MatchDuration(new TimeSpan(0, 90, 0));
        Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));

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
            Assert.IsNotNull(gameRepository.GetAll());
        }

        [TestMethod]
        public void AddGameToListTest()
        {    
            var game = new Game(matchDuration90Minutes, teamRed.Id, teamGreen.Id);
            var game2 = new Game(matchDuration90Minutes, teamRed.Id, teamGreen.Id);
            var gameIsAdded = false;
            var game2IsAdded = false;

            gameRepository.Add(game);

            foreach (var gameItem in gameRepository.GetAll())
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
        [ExpectedException(typeof(GameAlreadyAddedException))]
        public void AddThrowsGameAlreadyAddedException()
        {
            var game = new Game(matchDuration90Minutes, teamRed.Id, teamGreen.Id);

            gameRepository.Add(game);
            gameRepository.Add(game);
        }

        [TestMethod]
        public void GetAllReturnsIEnumerable()
        {
            Assert.IsInstanceOfType(gameRepository.GetAll(), typeof(IEnumerable<Game>));
        }
    }
}
