using Domain.Value_Objects;
using DomainTests.Entities;
using football_series_manager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class PlayerStatsTests
    {
        internal PlayerStats playerStatsOne;
        internal PlayerStats playerStatsOneDuplicate;
        internal PlayerStats playerStatsTwo;

        public PlayerStatsTests()
        {
            var nameOne = new Name("Eric", "Cartman");
            var nameTwo = new Name("Christiano", "Ronaldo");
            var dateOfBirthOne = new DateOfBirth("1990-08-02");
            var dateOfBirthTwo = new DateOfBirth("1980-09-23");
            var playerPositionOne = PlayerPosition.GoalKeeper;
            var playerPositionTwo = PlayerPosition.MidFielder;
            var playerStatusOne = PlayerStatus.Available;
            var playerStatusTwo = PlayerStatus.Injured;
            var shirtNumberOne = new ShirtNumber(25);
            var shirtNumberTwo = new ShirtNumber(28);
            var playerOne = new Player(nameOne, dateOfBirthOne, playerPositionOne, playerStatusOne,
                shirtNumberOne);
            var playerTwo = new Player(nameTwo, dateOfBirthTwo, playerPositionTwo, playerStatusTwo,
                shirtNumberTwo);

            playerStatsOne = new PlayerStats();
            playerStatsOneDuplicate = new PlayerStats();
            playerStatsTwo = new PlayerStats();

            playerStatsOne.GoalStats.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(11), playerOne),
                new Goal(new MatchMinute(44), playerOne),
                new Goal(new MatchMinute(89), playerOne)
            });

            playerStatsOneDuplicate.GoalStats.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(11), playerOne),
                new Goal(new MatchMinute(44), playerOne),
                new Goal(new MatchMinute(89), playerOne)
            });

            playerStatsTwo.GoalStats.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(3), playerTwo),
                new Goal(new MatchMinute(17), playerTwo),
                new Goal(new MatchMinute(32), playerTwo),
                new Goal(new MatchMinute(34), playerTwo)
            });

            playerStatsTwo.AssistStats.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(33), playerOne),
                new Assist(new MatchMinute(82), playerOne)
            });
        }
    }

    [TestMethod]
    public void PlayerStatsTest()
    {
        Assert.Fail();
    }
}
}