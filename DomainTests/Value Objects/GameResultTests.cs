using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Value_Objects.Tests
{
    [TestClass]
    public class GameResultTests
    {
        [TestMethod]
        public void GameResultIsEqualToValidEntries()
        {
            var gameResult = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 3, 0);
            Assert.AreEqual(gameResult.HomeTeam_Name.Value, "IFK Göteborg");
            Assert.AreEqual(gameResult.HomeTeam_Score, 3);
            Assert.AreEqual(gameResult.AwayTeam_Name.Value, "AIK");
            Assert.AreEqual(gameResult.AwayTeam_Score, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfHomeTeamScoreIsLessThanZero()
        {
            var gameResult = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), -1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfAwayTeamScoreIsLessThanZero()
        {
            var gameResult = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 0, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfHomeTeamScoreIsGreaterThanFifty()
        {
            var gameResult = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 51, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GameResultThrowsArgumentExceptionIfAwayTeamScoreIsGreaterThanFifty()
        {
            var gameResult = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 0, 51);
        }

        [TestMethod]
        public void GameResultIsComparableByValue()
        {
            var gameResultOne = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 3, 0);
            var gameResultTwo = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 3, 0);
            Assert.AreEqual(gameResultOne, gameResultTwo);
            Assert.IsTrue(gameResultOne == gameResultTwo);
        }

        [TestMethod]
        public void GameResultWorksWithHashSet()
        {
            var gameResultOne = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 3, 0);
            var gameResultTwo = new GameResult(new Team(new TeamName("IFK Göteborg"), new ArenaName("Ullevi"), new EmailAddress("ifkgbg@ifk.se")),
                new Team(new TeamName("AIK"), new ArenaName("Friends Arena"), new EmailAddress("aik@aik.se")), 3, 0);
            var gameResultHashSet = new HashSet<GameResult>();
            gameResultHashSet.Add(gameResultOne);
            gameResultHashSet.Add(gameResultTwo);
            Assert.IsTrue(gameResultHashSet.Count == 1);
        }
    }
}