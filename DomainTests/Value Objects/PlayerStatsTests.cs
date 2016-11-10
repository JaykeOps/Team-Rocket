using Domain.Entities;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DomainTests.Value_Objects
{
    [TestClass]
    public class PlayerStatsTests
    {
        internal Guid playerOneId;
        internal Guid playerTwoId;
        internal PlayerStats playerStatsOne;
        internal PlayerStats playerStatsOneDuplicate;
        internal PlayerStats playerStatsTwo;

        public PlayerStatsTests()
        {
            this.playerOneId = Guid.NewGuid();
            this.playerTwoId = Guid.NewGuid();
            playerStatsOne = new PlayerStats();
            playerStatsOneDuplicate = new PlayerStats();
            playerStatsTwo = new PlayerStats();

            playerStatsOne.GoalStats.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(11), this.playerOneId),
                new Goal(new MatchMinute(44), this.playerOneId),
                new Goal(new MatchMinute(89), this.playerOneId)
            });

            playerStatsOneDuplicate.GoalStats.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(11), this.playerOneId),
                new Goal(new MatchMinute(44), this.playerOneId),
                new Goal(new MatchMinute(89), this.playerOneId)
            });

            playerStatsTwo.GoalStats.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(3), this.playerTwoId),
                new Goal(new MatchMinute(17), this.playerTwoId),
                new Goal(new MatchMinute(32), this.playerTwoId),
                new Goal(new MatchMinute(34), this.playerTwoId)
            });

            playerStatsOne.AssistStats.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(33), this.playerOneId),
                new Assist(new MatchMinute(82), this.playerOneId)
            });

            playerStatsOneDuplicate.AssistStats.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(33), this.playerOneId),
                new Assist(new MatchMinute(82), this.playerOneId)
            });

            playerStatsTwo.AssistStats.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(5), this.playerTwoId),
                new Assist(new MatchMinute(9), this.playerTwoId),
                new Assist(new MatchMinute(67), this.playerTwoId),
                new Assist(new MatchMinute(55), this.playerTwoId),
                new Assist(new MatchMinute(45), this.playerTwoId)
            });

            playerStatsOne.CardStats.AddRange(new List<Card>
            {
                new Card(new MatchMinute(85), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(33), this.playerOneId, CardType.Red),
                new Card(new MatchMinute(67), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(23), this.playerOneId, CardType.Yellow)
            });

            playerStatsOneDuplicate.CardStats.AddRange(new List<Card>
            {
                new Card(new MatchMinute(85), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(33), this.playerOneId, CardType.Red),
                new Card(new MatchMinute(67), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(23), this.playerOneId, CardType.Yellow)
            });

            playerStatsTwo.CardStats.AddRange(new List<Card>
            {
                new Card(new MatchMinute(90), this.playerTwoId, CardType.Yellow),
                new Card(new MatchMinute(27), this.playerTwoId, CardType.Red)
            });

            playerStatsOne.PenaltyStats.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(12), this.playerOneId),
                new Penalty(new MatchMinute(22), this.playerOneId)
            });

            playerStatsOneDuplicate.PenaltyStats.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(12), this.playerOneId),
                new Penalty(new MatchMinute(22), this.playerOneId)
            });

            playerStatsTwo.PenaltyStats.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(3), this.playerTwoId)
            });

            for (int i = 0; i < 10; i++)
            {
                var idOne = Guid.NewGuid();
                var idTwo = Guid.NewGuid();
                this.playerStatsOne.GamesPlayedIds.Add(idOne);
                this.playerStatsOneDuplicate.GamesPlayedIds.Add(idOne);
                this.playerStatsTwo.GamesPlayedIds.Add(idTwo);
            }
        }

        [TestMethod]
        public void PlayerStatsIsComparedByValue()
        {
            Assert.AreEqual(this.playerStatsOne, this.playerStatsOneDuplicate);
            Assert.IsTrue(this.playerStatsOne == this.playerStatsOneDuplicate);
            Assert.AreNotEqual(this.playerStatsOne, this.playerStatsTwo);
            Assert.IsFalse(this.playerStatsOne == this.playerStatsTwo);
        }

        [TestMethod]
        public void PlayerStatsGoalCountIsEqualToGoalStats()
        {
            Assert.AreEqual(this.playerStatsOne.GoalCount, this.playerStatsOne.GoalStats.Count);
            Assert.IsTrue(this.playerStatsOne.GoalCount == this.playerStatsOne.GoalStats.Count
                && this.playerStatsOne.GoalCount == 3);
            this.playerStatsOne.GoalStats.Add(new Goal(new MatchMinute(3), this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.GoalCount, this.playerStatsOne.GoalStats.Count);
            Assert.IsTrue(this.playerStatsOne.GoalCount == this.playerStatsOne.GoalStats.Count
                && this.playerStatsOne.GoalCount == 4);
        }

        [TestMethod]
        public void PlayerStatsAssistCountIsEqualToAssistStats()
        {
            Assert.AreEqual(this.playerStatsOne.AssistCount, this.playerStatsOne.AssistStats.Count);
            Assert.IsTrue(this.playerStatsOne.AssistCount == this.playerStatsOne.AssistStats.Count
                && this.playerStatsOne.AssistCount == 2);
            this.playerStatsOne.AssistStats.Add(new Assist(new MatchMinute(55), this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.AssistCount, this.playerStatsOne.AssistStats.Count);
            Assert.IsTrue(this.playerStatsOne.AssistCount == this.playerStatsOne.AssistStats.Count
                && this.playerStatsOne.AssistCount == 3);
        }

        [TestMethod]
        public void PlayerStatsYellowCardCountIsEqualToYellowCardsInCardStats()
        {
            var yellowCardCount = this.playerStatsOne.CardStats.
                FindAll(x => x.CardType.Equals(CardType.Yellow)).Count;
            Assert.AreEqual(this.playerStatsOne.YellowCardCount, yellowCardCount);
        }

        [TestMethod]
        public void PlayerStatsRedCardCountIsEqualToRedCardsInCardStats()
        {
            var redCardCount = this.playerStatsOne.CardStats.
                FindAll(x => x.CardType.Equals(CardType.Red)).Count;
            Assert.AreEqual(this.playerStatsOne.RedCardCount, redCardCount);
        }

        [TestMethod]
        public void PlayerStatsPenaltyCountIsEqualToPenaltyStats()
        {
            Assert.AreEqual(this.playerStatsOne.PenaltyCount, this.playerStatsOne.PenaltyStats.Count);
            Assert.IsTrue(this.playerStatsOne.PenaltyCount == this.playerStatsOne.PenaltyStats.Count
                && this.playerStatsOne.PenaltyCount == 2);
            this.playerStatsOne.PenaltyStats.Add(new Penalty(new MatchMinute(62), this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.PenaltyCount, this.playerStatsOne.PenaltyStats.Count);
            Assert.IsTrue(this.playerStatsOne.PenaltyCount == this.playerStatsOne.PenaltyStats.Count
                && this.playerStatsOne.PenaltyCount == 3);
        }

        [TestMethod]
        public void PlayerStatsGamesPlayedCountIsEqualToGamesPlayedIdsCount()
        {
            Assert.AreEqual(this.playerStatsOne.GamesPlayedCount, this.playerStatsOne.GamesPlayedIds.Count);
            Assert.IsTrue(this.playerStatsOne.GamesPlayedCount == this.playerStatsOne.GamesPlayedIds.Count &&
                this.playerStatsOne.GamesPlayedCount == 10);
            this.playerStatsOne.GamesPlayedIds.Add(Guid.NewGuid());
            Assert.AreEqual(this.playerStatsOne.GamesPlayedCount, this.playerStatsOne.GamesPlayedIds.Count);
            Assert.IsTrue(this.playerStatsOne.GamesPlayedCount == this.playerStatsOne.GamesPlayedIds.Count &&
                this.playerStatsOne.GamesPlayedCount == 11);
        }
    }
}