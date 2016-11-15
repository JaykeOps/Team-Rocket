using Domain.CustomExceptions;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Domain.Entities.Tests
{
    [TestClass]
    public class GameTests
    {
        private Series series = new Series(new MatchDuration(new TimeSpan(0, 90, 0)), new NumberOfTeams(16));
        private MatchDateAndTime date = new MatchDateAndTime(DateTime.Now + TimeSpan.FromDays(1));
        private MatchDuration matchDuration90Minutes = new MatchDuration(new TimeSpan(0, 90, 0));
        private Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        private Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));

        [TestMethod]
        public void ConstructorInitiatesCorrectlyTest()
        {
            Match matchOne = new Match(teamRed.ArenaName, teamRed.Id, teamGreen.Id, series, date);

            var game = new Game(matchOne);

            Assert.AreEqual(matchDuration90Minutes, game.MatchDuration);
            Assert.AreEqual(teamRed.Id, game.HomeTeamId);
            Assert.AreEqual(teamGreen.Id, game.AwayTeamId);
        }

        [TestMethod]
        [ExpectedException(typeof(GameContainsSameTeamTwiceException))]
        public void ConstructorThrowsSameTeamException()
        {
            Match matchOne = new Match(teamRed.ArenaName, teamRed.Id, teamRed.Id, series, date);
            var game = new Game(matchOne);
        }
    }
}