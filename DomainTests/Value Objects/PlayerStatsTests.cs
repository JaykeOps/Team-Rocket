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
        internal Guid playerOneTeamId;
        internal Guid playerTwoTeamId;
        internal PlayerStats playerStatsOne;
        internal PlayerStats playerStatsOneDuplicate;
        internal PlayerStats playerStatsTwo;

        public PlayerStatsTests()
        {
            this.playerOneId = Guid.NewGuid();
            this.playerTwoId = Guid.NewGuid();
            this.playerOneTeamId = Guid.NewGuid();
            this.playerTwoTeamId = Guid.NewGuid();
            playerStatsOne = new PlayerStats();
            playerStatsOneDuplicate = new PlayerStats();
            playerStatsTwo = new PlayerStats();

            playerStatsOne.Goals.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(11), this.playerOneTeamId, this.playerOneId),
                new Goal(new MatchMinute(44), this.playerOneTeamId, this.playerOneId),
                new Goal(new MatchMinute(89), this.playerOneTeamId, this.playerOneId)
            });

            playerStatsOneDuplicate.Goals.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(11), this.playerOneTeamId, this.playerOneId),
                new Goal(new MatchMinute(44), this.playerOneTeamId, this.playerOneId),
                new Goal(new MatchMinute(89), this.playerOneTeamId, this.playerOneId)
            });

            playerStatsTwo.Goals.AddRange(new List<Goal>
            {
                new Goal(new MatchMinute(3), this.playerTwoTeamId, this.playerTwoId),
                new Goal(new MatchMinute(17), this.playerTwoTeamId, this.playerTwoId),
                new Goal(new MatchMinute(32), this.playerTwoTeamId, this.playerTwoId),
                new Goal(new MatchMinute(34), this.playerTwoTeamId, this.playerTwoId)
            });

            playerStatsOne.Assists.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(33), this.playerOneId),
                new Assist(new MatchMinute(82), this.playerOneId)
            });

            playerStatsOneDuplicate.Assists.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(33), this.playerOneId),
                new Assist(new MatchMinute(82), this.playerOneId)
            });

            playerStatsTwo.Assists.AddRange(new List<Assist>
            {
                new Assist(new MatchMinute(5), this.playerTwoId),
                new Assist(new MatchMinute(9), this.playerTwoId),
                new Assist(new MatchMinute(67), this.playerTwoId),
                new Assist(new MatchMinute(55), this.playerTwoId),
                new Assist(new MatchMinute(45), this.playerTwoId)
            });

            playerStatsOne.Cards.AddRange(new List<Card>
            {
                new Card(new MatchMinute(85), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(33), this.playerOneId, CardType.Red),
                new Card(new MatchMinute(67), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(23), this.playerOneId, CardType.Yellow)
            });

            playerStatsOneDuplicate.Cards.AddRange(new List<Card>
            {
                new Card(new MatchMinute(85), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(33), this.playerOneId, CardType.Red),
                new Card(new MatchMinute(67), this.playerOneId, CardType.Yellow),
                new Card(new MatchMinute(23), this.playerOneId, CardType.Yellow)
            });

            playerStatsTwo.Cards.AddRange(new List<Card>
            {
                new Card(new MatchMinute(90), this.playerTwoId, CardType.Yellow),
                new Card(new MatchMinute(27), this.playerTwoId, CardType.Red)
            });

            playerStatsOne.Penalties.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(12), this.playerOneId),
                new Penalty(new MatchMinute(22), this.playerOneId)
            });

            playerStatsOneDuplicate.Penalties.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(12), this.playerOneId),
                new Penalty(new MatchMinute(22), this.playerOneId)
            });

            playerStatsTwo.Penalties.AddRange(new List<Penalty>
            {
                new Penalty(new MatchMinute(3), this.playerTwoId)
            });

            for (int i = 0; i < 10; i++)
            {
                var idOne = Guid.NewGuid();
                var idTwo = Guid.NewGuid();
                this.playerStatsOne.Games.Add(idOne);
                this.playerStatsOneDuplicate.Games.Add(idOne);
                this.playerStatsTwo.Games.Add(idTwo);
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
            Assert.AreEqual(this.playerStatsOne.GoalCount, this.playerStatsOne.Goals.Count);
            Assert.IsTrue(this.playerStatsOne.GoalCount == this.playerStatsOne.Goals.Count
                && this.playerStatsOne.GoalCount == 3);
            this.playerStatsOne.Goals.Add(new Goal(new MatchMinute(3), this.playerOneTeamId, this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.GoalCount, this.playerStatsOne.Goals.Count);
            Assert.IsTrue(this.playerStatsOne.GoalCount == this.playerStatsOne.Goals.Count
                && this.playerStatsOne.GoalCount == 4);
        }

        [TestMethod]
        public void PlayerStatsAssistCountIsEqualToAssistStats()
        {
            Assert.AreEqual(this.playerStatsOne.AssistCount, this.playerStatsOne.Assists.Count);
            Assert.IsTrue(this.playerStatsOne.AssistCount == this.playerStatsOne.Assists.Count
                && this.playerStatsOne.AssistCount == 2);
            this.playerStatsOne.Assists.Add(new Assist(new MatchMinute(55), this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.AssistCount, this.playerStatsOne.Assists.Count);
            Assert.IsTrue(this.playerStatsOne.AssistCount == this.playerStatsOne.Assists.Count
                && this.playerStatsOne.AssistCount == 3);
        }

        [TestMethod]
        public void PlayerStatsYellowCardCountIsEqualToYellowCardsInCardStats()
        {
            var yellowCardCount = this.playerStatsOne.Cards.
                FindAll(x => x.CardType.Equals(CardType.Yellow)).Count;
            Assert.AreEqual(this.playerStatsOne.YellowCardCount, yellowCardCount);
        }

        [TestMethod]
        public void PlayerStatsRedCardCountIsEqualToRedCardsInCardStats()
        {
            var redCardCount = this.playerStatsOne.Cards.
                FindAll(x => x.CardType.Equals(CardType.Red)).Count;
            Assert.AreEqual(this.playerStatsOne.RedCardCount, redCardCount);
        }

        [TestMethod]
        public void PlayerStatsPenaltyCountIsEqualToPenaltyStats()
        {
            Assert.AreEqual(this.playerStatsOne.PenaltyCount, this.playerStatsOne.Penalties.Count);
            Assert.IsTrue(this.playerStatsOne.PenaltyCount == this.playerStatsOne.Penalties.Count
                && this.playerStatsOne.PenaltyCount == 2);
            this.playerStatsOne.Penalties.Add(new Penalty(new MatchMinute(62), this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.PenaltyCount, this.playerStatsOne.Penalties.Count);
            Assert.IsTrue(this.playerStatsOne.PenaltyCount == this.playerStatsOne.Penalties.Count
                && this.playerStatsOne.PenaltyCount == 3);
        }

        [TestMethod]
        public void PlayerStatsGamesPlayedCountIsEqualToGamesPlayedIdsCount()
        {
            Assert.AreEqual(this.playerStatsOne.GamesPlayedCount, this.playerStatsOne.Games.Count);
            Assert.IsTrue(this.playerStatsOne.GamesPlayedCount == this.playerStatsOne.Games.Count &&
                this.playerStatsOne.GamesPlayedCount == 10);
            this.playerStatsOne.Games.Add(Guid.NewGuid());
            Assert.AreEqual(this.playerStatsOne.GamesPlayedCount, this.playerStatsOne.Games.Count);
            Assert.IsTrue(this.playerStatsOne.GamesPlayedCount == this.playerStatsOne.Games.Count &&
                this.playerStatsOne.GamesPlayedCount == 11);
        }
    }
}