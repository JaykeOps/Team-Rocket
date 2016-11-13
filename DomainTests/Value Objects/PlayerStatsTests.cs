using Domain.Entities;
using Domain.Services;
using Domain.Value_Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

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

        [TestInitialize]
        public void Init()
        {
            var playerService = new PlayerService();
            this.playerOneId = playerService.GetAll().ToList().First().Id;
            this.playerTwoId = playerService.GetAll().ToList().ElementAt(1).Id;
            this.playerOneTeamId = Guid.NewGuid();
            this.playerTwoTeamId = Guid.NewGuid();
            playerStatsOne = new PlayerStats(this.playerOneId);
            playerStatsOneDuplicate = new PlayerStats(this.playerOneId);
            playerStatsTwo = new PlayerStats(this.playerTwoId);

            this.playerStatsOne.AddGoal(new Goal(new MatchMinute(11), this.playerOneTeamId, this.playerOneId));
            this.playerStatsOne.AddGoal(new Goal(new MatchMinute(44), this.playerOneTeamId, this.playerOneId));
            this.playerStatsOne.AddGoal(new Goal(new MatchMinute(89), this.playerOneTeamId, this.playerOneId));

            this.playerStatsOneDuplicate.AddGoal(new Goal(new MatchMinute(11), this.playerOneTeamId, this.playerOneId));
            this.playerStatsOneDuplicate.AddGoal(new Goal(new MatchMinute(44), this.playerOneTeamId, this.playerOneId));
            this.playerStatsOneDuplicate.AddGoal(new Goal(new MatchMinute(89), this.playerOneTeamId, this.playerOneId));

            this.playerStatsTwo.AddGoal(new Goal(new MatchMinute(3), this.playerTwoTeamId, this.playerTwoId));
            this.playerStatsTwo.AddGoal(new Goal(new MatchMinute(17), this.playerTwoTeamId, this.playerTwoId));
            this.playerStatsTwo.AddGoal(new Goal(new MatchMinute(32), this.playerTwoTeamId, this.playerTwoId));
            this.playerStatsTwo.AddGoal(new Goal(new MatchMinute(34), this.playerTwoTeamId, this.playerTwoId));

            this.playerStatsOne.AddAssist(new Assist(new MatchMinute(33), this.playerOneId));
            this.playerStatsOne.AddAssist(new Assist(new MatchMinute(82), this.playerOneId));

            this.playerStatsOneDuplicate.AddAssist(new Assist(new MatchMinute(33), this.playerOneId));
            this.playerStatsOneDuplicate.AddAssist(new Assist(new MatchMinute(82), this.playerOneId));

            this.playerStatsTwo.AddAssist(new Assist(new MatchMinute(5), this.playerTwoId));
            this.playerStatsTwo.AddAssist(new Assist(new MatchMinute(9), this.playerTwoId));
            this.playerStatsTwo.AddAssist(new Assist(new MatchMinute(67), this.playerTwoId));
            this.playerStatsTwo.AddAssist(new Assist(new MatchMinute(55), this.playerTwoId));
            this.playerStatsTwo.AddAssist(new Assist(new MatchMinute(45), this.playerTwoId));

            this.playerStatsOne.AddCard(new Card(new MatchMinute(85), this.playerOneId, CardType.Red));
            this.playerStatsOne.AddCard(new Card(new MatchMinute(67), this.playerOneId, CardType.Yellow));
            this.playerStatsOne.AddCard(new Card(new MatchMinute(23), this.playerOneId, CardType.Yellow));

            this.playerStatsOneDuplicate.AddCard(new Card(new MatchMinute(85), this.playerOneId, CardType.Red));
            this.playerStatsOneDuplicate.AddCard(new Card(new MatchMinute(67), this.playerOneId, CardType.Yellow));
            this.playerStatsOneDuplicate.AddCard(new Card(new MatchMinute(23), this.playerOneId, CardType.Yellow));

            this.playerStatsTwo.AddCard(new Card(new MatchMinute(90), this.playerTwoId, CardType.Yellow));
            this.playerStatsTwo.AddCard(new Card(new MatchMinute(27), this.playerTwoId, CardType.Red));

            this.playerStatsOne.AddPenalty(new Penalty(new MatchMinute(12), this.playerOneId));
            this.playerStatsOne.AddPenalty(new Penalty(new MatchMinute(22), this.playerOneId));

            this.playerStatsOneDuplicate.AddPenalty(new Penalty(new MatchMinute(12), this.playerOneId));
            this.playerStatsOneDuplicate.AddPenalty(new Penalty(new MatchMinute(22), this.playerOneId));

            this.playerStatsTwo.AddPenalty(new Penalty(new MatchMinute(3), this.playerTwoId));

            for (int i = 0; i < 10; i++)
            {
                var idOne = Guid.NewGuid();
                var idTwo = Guid.NewGuid();
                this.playerStatsOne.AddGameId(idOne);
                this.playerStatsOneDuplicate.AddGameId(idOne);
                this.playerStatsTwo.AddGameId(idTwo);
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
            Assert.AreEqual(this.playerStatsOne.GoalCount, this.playerStatsOne.Goals.Count());
            Assert.IsTrue(this.playerStatsOne.GoalCount == 3);
            this.playerStatsOne.AddGoal(new Goal(new MatchMinute(3), this.playerOneTeamId, this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.GoalCount, this.playerStatsOne.Goals.ToList().Count);
            Assert.IsTrue(this.playerStatsOne.GoalCount == this.playerStatsOne.Goals.ToList().Count
                && this.playerStatsOne.GoalCount == 4);
        }

        [TestMethod]
        public void PlayerStatsAssistCountIsEqualToAssistStats()
        {
            Assert.AreEqual(this.playerStatsOne.AssistCount, this.playerStatsOne.Assists.Count());
            Assert.IsTrue(this.playerStatsOne.AssistCount == this.playerStatsOne.Assists.Count()
                && this.playerStatsOne.AssistCount == 2);
            this.playerStatsOne.AddAssist(new Assist(new MatchMinute(55), this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.AssistCount, this.playerStatsOne.Assists.Count());
            Assert.IsTrue(this.playerStatsOne.AssistCount == this.playerStatsOne.Assists.Count()
                && this.playerStatsOne.AssistCount == 3);
        }

        [TestMethod]
        public void PlayerStatsYellowCardCountIsEqualToYellowCardsInCardStats()
        {
            var yellowCardCount = this.playerStatsOne.Cards.ToList().
                FindAll(x => x.CardType.Equals(CardType.Yellow)).Count;
            Assert.AreEqual(this.playerStatsOne.YellowCardCount, yellowCardCount);
        }

        [TestMethod]
        public void PlayerStatsRedCardCountIsEqualToRedCardsInCardStats()
        {
            var redCardCount = this.playerStatsOne.Cards.ToList().
                FindAll(x => x.CardType.Equals(CardType.Red)).Count;
            Assert.AreEqual(this.playerStatsOne.RedCardCount, redCardCount);
        }

        [TestMethod]
        public void PlayerStatsPenaltyCountIsEqualToPenaltyStats()
        {
            Assert.AreEqual(this.playerStatsOne.PenaltyCount, this.playerStatsOne.Penalties.Count());
            Assert.IsTrue(this.playerStatsOne.PenaltyCount == this.playerStatsOne.Penalties.Count()
                && this.playerStatsOne.PenaltyCount == 2);
            this.playerStatsOne.AddPenalty(new Penalty(new MatchMinute(62), this.playerOneId));
            Assert.AreEqual(this.playerStatsOne.PenaltyCount, this.playerStatsOne.Penalties.Count());
            Assert.IsTrue(this.playerStatsOne.PenaltyCount == this.playerStatsOne.Penalties.Count()
                && this.playerStatsOne.PenaltyCount == 3);
        }

        [TestMethod]
        public void PlayerStatsGamesPlayedCountIsEqualToGamesPlayedIdsCount()
        {
            Assert.AreEqual(this.playerStatsOne.GamesPlayedCount, this.playerStatsOne.Games.Count());
            Assert.IsTrue(this.playerStatsOne.GamesPlayedCount == this.playerStatsOne.Games.Count() &&
                this.playerStatsOne.GamesPlayedCount == 10);
            this.playerStatsOne.AddGameId(Guid.NewGuid());
            Assert.AreEqual(this.playerStatsOne.GamesPlayedCount, this.playerStatsOne.Games.Count());
            Assert.IsTrue(this.playerStatsOne.GamesPlayedCount == this.playerStatsOne.Games.Count() &&
                this.playerStatsOne.GamesPlayedCount == 11);
        }
    }
}