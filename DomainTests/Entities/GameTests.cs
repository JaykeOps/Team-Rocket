using Domain.CustomExceptions;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Entities.Tests
{
    [TestClass]
    public class GameTests
    {
        private MatchDuration matchDuration90Minutes = new MatchDuration(new TimeSpan(0, 90, 0));
        private Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        private Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));

        [TestMethod]
        public void ConstructorInitiatesCorrectlyTest()
        {
            var game = new Game(matchDuration90Minutes, teamRed.Id, teamGreen.Id);

            Assert.AreEqual(matchDuration90Minutes, game.MatchDuration);
            Assert.AreEqual(teamRed.Id, game.HomeTeamId);
            Assert.AreEqual(teamGreen.Id, game.AwayTeamId);
        }

        [TestMethod]
        [ExpectedException(typeof(GameContainsSameTeamTwiceException))]
        public void ConstructorThrowsSameTeamException()
        {
            var game = new Game(matchDuration90Minutes, teamRed.Id, teamRed.Id);

            //try
            //{
            //    var game = new Game(matchDuration90Minutes, teamRed.Id, teamRed.Id);
            //}
            //catch(GameContainsSameTeamTwiceException)
            //{
            //}
        }
    }
}