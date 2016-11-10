using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Value_Objects;
using Domain.CustomExceptions;

namespace Domain.Entities.Tests
{
    [TestClass]
    public class GameTests
    {

        MatchDuration matchDuration90Minutes = new MatchDuration(new TimeSpan(0, 90, 0));
        Team teamRed = new Team(new TeamName("RedTeam"), new ArenaName("RedArena"), new EmailAddress("red@gmail.se"));
        Team teamGreen = new Team(new TeamName("GreenTeam"), new ArenaName("GreenArena"), new EmailAddress("green@gmail.se"));

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