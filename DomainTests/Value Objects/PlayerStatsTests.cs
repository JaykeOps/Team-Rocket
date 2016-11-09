using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class PlayerStatsTests
    {
        internal Player playerOne;
        internal Player playerTwo;
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
            playerOne = new Player(nameOne, dateOfBirthOne, playerPositionOne, playerStatusOne,
                shirtNumberOne);
            playerTwo = new Player(nameTwo, dateOfBirthTwo, playerPositionTwo, playerStatusTwo,
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

            playerStatsOne.AssistStats.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(33), playerOne),
                new Assist(new MatchMinute(82), playerOne)
            });

            playerStatsOneDuplicate.AssistStats.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(33), playerOne),
                new Assist(new MatchMinute(82), playerOne)
            });

            playerStatsTwo.AssistStats.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(5), playerTwo),
                new Assist(new MatchMinute(9), playerTwo),
                new Assist(new MatchMinute(67), playerTwo),
                new Assist(new MatchMinute(55), playerTwo),
                new Assist(new MatchMinute(45), playerTwo)
            });

            playerStatsOne.CardStats.AddRange(new List<Card>
            {
                new Card(new MatchMinute(85), playerOne, CardType.Yellow),
                new Card(new MatchMinute(33), playerOne, CardType.Red),
                new Card(new MatchMinute(67), playerOne, CardType.Yellow),
                new Card(new MatchMinute(23), playerOne, CardType.Yellow)
            });

            playerStatsOneDuplicate.CardStats.AddRange(new List<Card>
            {
                new Card(new MatchMinute(85), playerOne, CardType.Yellow),
                new Card(new MatchMinute(33), playerOne, CardType.Red),
                new Card(new MatchMinute(67), playerOne, CardType.Yellow),
                new Card(new MatchMinute(23), playerOne, CardType.Yellow)
            });

            playerStatsTwo.CardStats.AddRange(new List<Card>
            {
                new Card(new MatchMinute(90), playerTwo, CardType.Yellow),
                new Card(new MatchMinute(27), playerTwo, CardType.Red)
            });

            playerStatsOne.PenaltyStats.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(12), playerOne),
                new Penalty(new MatchMinute(22), playerOne)
            });

            playerStatsOneDuplicate.PenaltyStats.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(12), playerOne),
                new Penalty(new MatchMinute(22), playerOne)
            });

            playerStatsTwo.PenaltyStats.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(3), playerTwo)
            });
        }

        [TestMethod]
        public void PlayerStatsIsComparedByValue()
        {
            Assert.AreEqual(playerStatsOne, playerStatsOneDuplicate);
            Assert.IsTrue(playerStatsOne == playerStatsOneDuplicate);
            Assert.AreNotEqual(playerStatsOne, playerStatsTwo);
            Assert.IsFalse(playerStatsOne == playerStatsTwo);
        }

        [TestMethod]
        public void PlayerStatsGoalCountIsEqualToGoalStats()
        {
            Assert.AreEqual(playerStatsOne.GoalCount, playerStatsOne.GoalStats.Count);
            Assert.IsTrue(playerStatsOne.GoalCount == playerStatsOne.GoalStats.Count
                && playerStatsOne.GoalCount == 3);
            playerStatsOne.GoalStats.Add(new Goal(new MatchMinute(3), playerOne));
            Assert.AreEqual(playerStatsOne.GoalCount, playerStatsOne.GoalStats.Count);
            Assert.IsTrue(playerStatsOne.GoalCount == playerStatsOne.GoalStats.Count
                && playerStatsOne.GoalCount == 4);
        }

        [TestMethod]
        public void PlayerStatsAssistCountIsEqualToAssistStats()
        {
            Assert.AreEqual(playerStatsOne.AssistCount, playerStatsOne.AssistStats.Count);
            Assert.IsTrue(playerStatsOne.AssistCount == playerStatsOne.AssistStats.Count
                && playerStatsOne.AssistCount == 2);
            playerStatsOne.AssistStats.Add(new Assist(new MatchMinute(55), playerOne));
            Assert.AreEqual(playerStatsOne.AssistCount, playerStatsOne.AssistStats.Count);
            Assert.IsTrue(playerStatsOne.AssistCount == playerStatsOne.AssistStats.Count
                && playerStatsOne.AssistCount == 3);
        }

        [TestMethod]
        public void PlayerStatsYellowCardCountIsEqualToYellowCardsInCardStats()
        {
            var yellowCardCount = playerStatsOne.CardStats.
                FindAll(x => x.CardType.Equals(CardType.Yellow)).Count;
            Assert.AreEqual(playerStatsOne.YellowCardCount, yellowCardCount);
        }

        [TestMethod]
        public void PlayerStatsRedCardCountIsEqualToRedCardsInCardStats()
        {
            var redCardCount = playerStatsOne.CardStats.
                FindAll(x => x.CardType.Equals(CardType.Red)).Count;
            Assert.AreEqual(playerStatsOne.RedCardCount, redCardCount);
        }

        [TestMethod]
        public void PlayerStatsPenaltyCountIsEqualToPenaltyStats()
        {
            Assert.AreEqual(playerStatsOne.PenaltyCount, playerStatsOne.PenaltyStats.Count);
            Assert.IsTrue(playerStatsOne.PenaltyCount == playerStatsOne.PenaltyStats.Count
                && playerStatsOne.PenaltyCount == 2);
            playerStatsOne.PenaltyStats.Add(new Penalty(new MatchMinute(62), playerOne));
            Assert.AreEqual(playerStatsOne.PenaltyCount, playerStatsOne.PenaltyStats.Count);
            Assert.IsTrue(playerStatsOne.PenaltyCount == playerStatsOne.PenaltyStats.Count
                && playerStatsOne.PenaltyCount == 3);
        }
    }
}